using System.Collections.Generic;
using System.Reflection;
using Core.Components;
using Core.Generated;
using Core.Lib;
using Core.Services;
using Core.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using Reflex;

namespace Core
{
    public class EcsEngine : MonoConstruct, IEcsEngine
    {
        private EcsWorld _world;
        private WorldMessages _worldMessages;

        private EcsSystems _updateSystems;
        private EcsSystems _fixedUpdateSystems;
        private List<IEcsSystem> _globalUiSystems = new();

        public WorldMessages WorldMessages => _worldMessages;
        public EcsWorld World => _world;

        private Context _context;

        protected override void Construct(Context context) => _context = context;

        private void Awake()
        {
            _world = _context.Resolve<EcsWorld>();

            _updateSystems = new EcsSystems(_world);
            _fixedUpdateSystems = new EcsSystems(_world);
            _worldMessages = new WorldMessages(_world, this);
            enabled = false;
        }

        public void SetupSystems()
        {
            _updateSystems
                .Add(new EventTimeDilationSystem(_context))
                .Add(new EventCameraShakeSystem(_context));

            _fixedUpdateSystems
#if UNITY_EDITOR
                .Add(new DebugEnemySystem())
#endif
                //============================================
                //Создание сущьностей из предыдущего кадра
                .Add(new BuildEntitySystem(_context))
                //--Часть логики текущей игры, вставка значений родителя ()
                .Add(new EventSetupParentValueSystem<DamageValueComponent>())
                .Add(new EventSetupParentComponentSystem<ThroughProjectileSlotTag>())
                //--
                //Удаление сущностей
                .Add(new WriteDefaultsBeforeRemoveEntitySystem())
                .Add(new RemoveModulesBeforeRemoveEntitySystem())
                .Add(new ActionCancelBeforeRemoveEntitySystem())
                .Add(new CascadeCleanBeforeRemoveEntitySystem())
                // .Add(new RemoveWithParentSystem())
                .Add(new EventRemoveEntitySystem())

                //Ядро
                //Добавление значений из базовых
                .Add(new InitValuesFromBaseValuesSystem())
                //системы слотов значений
                .AddMany(new SlotSetupSystemsNode().BuildSystems(_context))
                //Начало перерасчёта значений
                .AddMany(new RecalculateValueSystemsNode().BuildSystems())
                //Суммирование со слотами
                .Add(new EventRefreshValueBySlotSystem(_world))
                //=============================================================================================

                //Логика для текущей игры. (расчёт ограничений, множителей, добавление обязательных компонентов)
                //Умножение базовых значений на скейл от сложности
                .AddMany(_context.Resolve<DifficultScaleService>().InitializeAndBuildSystems())
                //Добавление обязательных компонентов
                .Add(new InitSubComponentSystem())
                //Вставка value если есть maxValue
                .AddMany(new InitValueByMaxValueSystemsNode().BuildSystems())
                //Приравнивание значений к макс значениям при инициализации (хп к макс хп и т.д.)
                .AddMany(new InitEquateValuesSystemsNode().BuildSystems())
                //
                .Add(new InitEventSetupVelocitySystem())
                //Удаление ивента иницыализации
                .Add(new DelHere<EventInit>())
                //--------

                //Приминение бонусов, процент к урону/хп и т.д.
                .AddMany(new MathValueSystemsNode().BuildSystems())
                .Add(new RestoreByCheckpointSystem())
                //
                //Установка цели для виртуальной камеры
                .Add(new VirtualCameraEventsSystem(_context))
                //Накладывание импактов (без урона)
                .Add(new ImpactAreaSystem())
                //Накладывание урона
                .Add(new DamageAreaSystem(_context))

                //EventStartTimerResetReceivers
                .Add(new AreaResetReceiversSystem())

                //Обработка коллектебелс
                .Add(new PickCollectableSystem())
                .Add(new ReceiverSystem())

                //Притягивание колектаблов
                .Add(new MagnetSystem<MagnetAreaComponent>())
                .Add(new MagnetSystem<ReceiverMagnetAreaComponent>())

                // .Add(new ScanHierarchyForNestedEntitiesSystem(_context))
                //Вызов верхнеуровнего поведения
                .Add(new BehaviorTreeSystem(_context))

                //Управление игрока
                .Add(new PlayerControllerSystem(_context))
                .Add(new PlayerButtonsSystem(_context))
                //Авто активация
                // .Add(new EventStartActionJumpCrushWallSystem())
                // .Add(new EventStartActionClimbingSystem())
                // .Add(new EventStartActionHitImpactSystem(_context))
                //Установка модулей
                .Add(new EventSetupModuleSystem<ShotTriggerComponent>())
                //События для модулей
                .Add(new ModuleTriggerSystem<ShotTriggerComponent>())
                //Системы способностей
                .AddMany(new ActionSystemsNode().BuildSystems(_context))

                //Спавны
                .Add(new ShockWaveMissileSystem(_context))
                .Add(new DamageOrbSpawnSystem(_context))
                .Add(new DamageOrbSystem())
                .Add(new ThroughProjectileSystem())
                //
                .Add(new DropOnDeathSystem(_context))

                //Поворот модели сущности (Top Down)
                .Add(new RotationTransformSystem())
                //
                .Add(new SetupForwardVelocitySystem())
                //
                .Add(new EventEnableOutlinableSystem())

                //Удаление нанесенного урона
                .Add(new DelHere<EventCausedDamage>())

                //Удаление события получения урона
                .Add(new DelHere<EventDamageTaken>())

                //Применение защиты
                .Add(new ReduceDamageByDefenceSystem())

                //Нанесение урона
                .Add(new EventApplyDamageSystem<EventIncomingDamage>(_context))

                //запуск регенерации если значение было изменено
                .AddMany(new RecoverySystemsNode().BuildSystems())

                //запуск перезарядки экшена
                .AddMany(new RecoveryActionSystemsNode().BuildSystems())

                //EventRemoveEntity сущностей с истекшим сроком жизни
                .Add(new LifetimeSystem())

                //EventRemoveEntity при столкновении с окружением
                .Add(new DeathOnTouchWallSystem())
                .Add(new RemoveOnTouchWallSystem())

                //EventRemoveEntity сущностей которые нанесли урон
                .Add(new DeathOnDealDamageSystem())
                .Add(new RemoveOnDealDamageSystem())

                //Удаление евентов которые должны быть удалены в конце
                .Add(new DelHere<EventTouchWall>())

                //=============================================================================================
                //Обновление UI
                .AddMany(_globalUiSystems)
                .AddMany(new LocalUiValueSystemsNode().BuildSystems())
#if UNITY_EDITOR
                // Регистрируем отладочные системы по контролю за состоянием каждого отдельного мира:
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(bakeComponentsInName: false));
#endif
            ;

            _updateSystems.Inject();
            _fixedUpdateSystems.Inject();

            var componentPools = _context.Resolve<ComponentPools>();
            InjectGeneratedPoolsAndFilters(_updateSystems, componentPools);
            InjectGeneratedPoolsAndFilters(_fixedUpdateSystems, componentPools);

            _updateSystems.Init();
            _fixedUpdateSystems.Init();
            enabled = true;
        }

        private void Update() => _updateSystems.Run();

        private void FixedUpdate() => _fixedUpdateSystems.Run();

        public void MakeTicksToUpdateSlotValues()
        {
            _fixedUpdateSystems?.Run();
            _fixedUpdateSystems?.Run();
        }

        private void OnDestroy()
        {
            _updateSystems.Destroy();
            _fixedUpdateSystems.Destroy();
        }

        public void AddUiSystem(IEcsSystem system) => _globalUiSystems.Add(system);

        private void InjectGeneratedPoolsAndFilters(
            IEcsSystems systems,
            ComponentPools pools)
        {
            foreach (var system in systems.GetAllSystems())
            foreach (var f in system.GetType().GetFields(
                         BindingFlags.Public |
                         BindingFlags.NonPublic |
                         BindingFlags.Instance))
            {
                if (f.IsStatic)
                    continue;

                if (typeof(ComponentPools).IsAssignableFrom(f.FieldType))
                    f.SetValue(system, pools);
            }
        }
    }
}
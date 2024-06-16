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
            _fixedUpdateSystems
                //Часть моего фреймворка, не стал удалять т.к. в любом проекте этот кусок будет присутствовать. 
                //============================================
                //Создание сущьностей из предыдущего кадра
                .Add(new BuildEntitySystem(_context))
                //--Часть логики текущей игры, вставка значений родителя ()
                // .Add(new EventSetupParentValueSystem<DamageValueComponent>())
                // .Add(new EventSetupParentComponentSystem<ThroughProjectileSlotTag>())
                //--
                //Удаление сущностей
                .Add(new WriteDefaultsBeforeRemoveEntitySystem())
                .Add(new ActionCancelBeforeRemoveEntitySystem())
                .Add(new CascadeCleanBeforeRemoveEntitySystem())
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

                //Добавление обязательных компонентов
                .Add(new InitSubComponentSystem())
                //Удаление ивента инициализации
                .Add(new DelHere<EventInit>())
                //--------

                //Установка цели для виртуальной камеры
                .Add(new EventVirtualCameraFollowSetupSystem(_context))

                //Обработка коллектебелс
                .Add(new PickCollectableSystem())
                .Add(new StackSystem())
                .Add(new ReceiverSystem(_context))

                //Притягивание колектаблов
                .Add(new MagnetSystem<MagnetAreaComponent>())
                .Add(new MagnetSystem<ReceiverMagnetAreaComponent>())

                //Управление игрока
                .Add(new PlayerControllerSystem(_context))
                .Add(new PlayerButtonsSystem(_context))
                
                //Системы способностей
                .AddMany(new ActionSystemsNode().BuildSystems(_context))

                //Поворот модели сущности (Top Down)
                .Add(new RotationTransformSystem())

                //=============================================================================================
                //Обновление UI
                .AddMany(_globalUiSystems)
#if UNITY_EDITOR
                // Регистрируем отладочные системы по контролю за состоянием каждого отдельного мира:
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(bakeComponentsInName: false));
#endif
            ;

            _updateSystems.Inject();
            _fixedUpdateSystems.Inject();

            var componentPools = _context.Resolve<ComponentPools>();
            InjectGeneratedPools(_updateSystems, componentPools);
            InjectGeneratedPools(_fixedUpdateSystems, componentPools);

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

        private void InjectGeneratedPools(
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
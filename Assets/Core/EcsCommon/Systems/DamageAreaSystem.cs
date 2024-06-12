using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using Reflex;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Systems
{
    public class DamageAreaSystem : IEcsRunSystem
    {
        private EcsWorldInject _world;

        private readonly EcsFilterInject<
            Inc<
                ActiveArea<DamageAreaComponent>,
                DamageAreaComponent,
                DamageScaleComponent,
                DamageValueComponent,
                HitImpactEventsComponent,
                TransformComponent
            >,
            Exc<
                CriticalChanceValueComponent
            >> _filter;

        private readonly EcsFilterInject<
            Inc<
                ActiveArea<DamageAreaComponent>,
                DamageAreaComponent,
                DamageValueComponent,
                DamageScaleComponent,
                CriticalChanceValueComponent,
                CriticalDamageValueComponent,
                HitImpactEventsComponent,
                TransformComponent
            >> _filterWithCritical;

        private readonly EcsFilterInject<Inc<EventIncomingDamage>> _eventApplyDamageFilter;
        private readonly EcsFilterInject<Inc<EventImpactVelocity>> _eventHitVelocityFilter;

        private readonly EffectWithText _damageTextEffectPrefab;
        private readonly EffectWithText _criticalDamageTextEffectPrefab;
        private readonly ComponentPools _pools;

        private int _entity;
        private float _damage;
        private Vector3 _position;
        private AbstractArea _area;
        private EffectWithText _effectPrefab;
        private AbstractEntityLogic _targetEvents;

        public DamageAreaSystem(Context context)
        {
            var effectsTemplate = context.Resolve<CommonEffectsTemplate>();
            _damageTextEffectPrefab = effectsTemplate.DamageTextEffectPrefab;
            _criticalDamageTextEffectPrefab = effectsTemplate.CriticalDamageTextEffectPrefab;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _eventApplyDamageFilter.Value)
                _pools.EventIncomingDamage.Del(i);

            foreach (var i in _filter.Value)
                UpdateEntity(i, _pools.DamageArea.Get(i).area, _pools.DamageValue.Get(i).value,
                    _damageTextEffectPrefab);

            foreach (var i in _filterWithCritical.Value)
            {
                var damage = _pools.DamageValue.Get(i).value;
                var effect = _damageTextEffectPrefab;

                if (_pools.CriticalChanceValue.Get(i).value >= Random.value)
                {
                    effect = _criticalDamageTextEffectPrefab;
                    damage += damage * _pools.CriticalDamageValue.Get(i).value;
                }

                UpdateEntity(i, _pools.DamageArea.Get(i).area, damage, effect);
            }
        }

        private void UpdateEntity(int entity, AbstractArea area, float damage, EffectWithText effectPrefab)
        {
            //Костыль запрещающий активацию DamageAreaSystem, изза того что аниматор включает объекты между анимациями -
            //если в анимации такой объект был включен и не был выключен изза прерывания анимации.
            //Решение видится в переходе на Animancer
            // if (_pools.ActionCurrent.Has(entity) &&
            //     _pools.ActionCurrent.Get(entity).currentAction?.ActionEnum switch
            //     {
            //         ActionEnum.ActionDashSystem => true,
            //         ActionEnum.ActionFlySystem => true,
            //         ActionEnum.ActionHitImpactSystem => true,
            //         ActionEnum.ActionClimbingSystem => true,
            //         ActionEnum.ActionIdleSystem => true,
            //         ActionEnum.ActionJump2System => true,
            //         ActionEnum.ActionJump3System => true,
            //         ActionEnum.ActionJumpCrushWallSystem => true,
            //         ActionEnum.ActionJumpFromWallSystem => true,
            //         ActionEnum.ActionJumpSystem => true,
            //         ActionEnum.ActionMoveSystem => true,
            //         ActionEnum.ActionReviveSystem => true,
            //         ActionEnum.ActionSitSystem => true,
            //         _ => false
            //     })
            //     return;

            var hitImpactsNode = _pools.HitImpactEvents.Get(entity);
            var position = _pools.Transform.Get(entity).transform.position;

            float baseDamageScale = _pools.DamageScale.Get(entity).value;
            hitImpactsNode.selfEvents?.Run(entity);
            ref var damageHitScale = ref _pools.DamageScale.Get(entity);
            damage *= damageHitScale.value;
            damage += Random.value * damage * .2f;
            
            _damage = damage;
            _area = area;
            _position = position;
            _entity = entity;
            _effectPrefab = effectPrefab;
            _targetEvents = hitImpactsNode.targetEvents;

            area.ForEachEntity(ForEachEntity);

            damageHitScale.value = baseDamageScale;
        }

        private void ForEachEntity(int targetEntity)
        {
            var eventIncomingDamage = _pools.EventIncomingDamage.GetOrInitialize(targetEntity);

            eventIncomingDamage.data.Add(
                _damage,
                _area.triggerPoint,
                _position,
                _entity,
                _effectPrefab
            );

            _targetEvents?.Run(targetEntity);
        }
    }
}
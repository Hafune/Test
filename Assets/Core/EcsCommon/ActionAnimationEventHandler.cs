using Core;
using Core.Components;
using Core.Generated;
using Core.Lib;
using Core.Systems;
using Leopotam.EcsLite;
using Lib;
using Reflex;
using UnityEngine;
using Voody.UniLeo.Lite;

[DisallowMultipleComponent, RequireComponent(typeof(Animator))]
public class ActionAnimationEventHandler : MonoConstruct
{
    public enum Parameters
    {
        CanBeCanceled,
        CannotBeCanceled,
        Completed,
        ResetTargets,
    }

    [SerializeField] private ConvertToEntity _convertToEntity;
    [SerializeField] private DamageArea _damageArea;
    private EcsPool<ActionCurrentComponent> _currentActionPool;
    private EcsPool<EventTimelineAction> _eventTimelineActionPool;
    private Glossary<AbstractEffect> _pathHash = new();
    private Glossary<AbstractEntityLogic> _timelineHash = new();
    private Context _context;

    private void OnValidate()
    {
        _convertToEntity = _convertToEntity ? _convertToEntity : GetComponentInParent<ConvertToEntity>();
        _damageArea = _damageArea ? _damageArea : GetComponentInChildren<DamageArea>(true);
    }

    protected override void Construct(Context context) => _context = context;

    private void Awake()
    {
        var pools = _context.Resolve<ComponentPools>();
        _currentActionPool = pools.ActionCurrent;
        _eventTimelineActionPool = pools.EventTimelineAction;
    }

    private void PutAnimationEventReferencePath(ReferencePath pathRef)
    {
        if (_pathHash.TryGetValue(pathRef.GetInstanceID(), out var target))
        {
            target.Execute();
        }
        else
        {
            target = pathRef.Find(transform)!.GetComponent<AbstractEffect>();
            _pathHash.Add(pathRef.GetInstanceID(), target);

            target.Execute();
        }
    }

    private void PutAnimationEvent(ActionAnimationEventData data)
    {
        int entity = _convertToEntity.RawEntity;

        switch (data.Parameter)
        {
            case Parameters.CanBeCanceled:
                _currentActionPool.Get(entity).canBeCanceled = true;
                break;
            case Parameters.CannotBeCanceled:
                _currentActionPool.Get(entity).canBeCanceled = false;
                break;
            case Parameters.Completed:
                ref var action = ref _currentActionPool.Get(entity);
                action.isCompleted = true;
                action.BTreeOnActionCompleted?.Invoke();
                break;
            case Parameters.ResetTargets:
                _damageArea.ResetReceivers();
                break;
            default:
                Debug.LogError("Неизвестный параметер " + data.Parameter);
                break;
        }
    }

    private void PutTimelineAction(ReferencePath pathRef)
    {
        int entity = _convertToEntity.RawEntity;

        if (!_timelineHash.TryGetValue(pathRef.GetInstanceID(), out var target))
        {
            target = pathRef.Find(transform)!.GetComponent<AbstractEntityLogic>();
            _timelineHash.Add(pathRef.GetInstanceID(), target);
        }

        _eventTimelineActionPool.GetOrInitialize(entity).logic += target.Run;
    }
}
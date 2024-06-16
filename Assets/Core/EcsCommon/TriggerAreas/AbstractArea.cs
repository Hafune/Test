using System;
using System.Collections.Generic;
using System.Linq;
using Core.Components;
using Leopotam.EcsLite;
using Lib;
using Reflex;
using UnityEngine;
using UnityEngine.Assertions;
using Voody.UniLeo.Lite;

public abstract class AbstractArea : MonoConstruct, ITriggerDispatcherTarget
{
    [SerializeField] private ConvertToEntity _entityRef;
    [SerializeField] private bool _useReceiversList;
    private readonly HashSet<int> _hitTaken = new();
    public Vector2 triggerPoint { get; private set; }

    private readonly Dictionary<int, Collider> _entityContacts = new();

    private Context _context;
    private Action Activate;
    private Action DeActivate;
    private bool _areaIsActive;

    private void OnValidate() => _entityRef = _entityRef ? _entityRef : GetComponentInParent<ConvertToEntity>();

    protected override void Construct(Context context) => _context = context;

    protected void Init<T>() where T : struct, IAreaComponent
    {
        Assert.IsNotNull(_entityRef);

        var world = _context.Resolve<EcsWorld>();
        var activeAreaPool = world.GetPool<ActiveArea<T>>();
        var triggerPool = world.GetPool<EventTriggerEnterArea<T>>();

        Activate += () =>
        {
            if (!_areaIsActive)
            {
                _areaIsActive = true;

                if (_entityRef.RawEntity != -1)
                    activeAreaPool.Add(_entityRef.RawEntity);
            }

            if (_entityRef.RawEntity != -1)
                triggerPool.AddIfNotExist(_entityRef.RawEntity);
        };

        DeActivate += () =>
        {
            if (_areaIsActive && _entityRef.RawEntity != -1)
                activeAreaPool.Del(_entityRef.RawEntity);

            _areaIsActive = false;
        };
    }

    private void OnDisable()
    {
        _entityContacts.Clear();
        _hitTaken.Clear();
        ClearContactIfNeed();
    }

    public void ResetReceivers()
    {
        _hitTaken.Clear();

        if (_useReceiversList && _entityContacts.Count != 0)
            Activate();
    }

    public void OnTriggerEnter(Collider other)
    {
        var entityRef = other.GetComponentInParent<ConvertToEntity>();

        if (entityRef is null)
            return;

        if (entityRef.RawEntity < 0 || !_entityContacts.TryAdd(entityRef.RawEntity, other))
            return;

        entityRef.OnEntityWasDeleted += EntityRefDisabled;

        if (_useReceiversList && !_hitTaken.Contains(entityRef.RawEntity))
            Activate();
        else
            Activate();
    }

    public void OnTriggerExit(Collider other)
    {
        var entityRef = other.GetComponentInParent<ConvertToEntity>();

        if (entityRef is null)
            return;

        if (!_entityContacts.Remove(entityRef.RawEntity))
            return;

        entityRef.OnEntityWasDeleted -= EntityRefDisabled;
        ClearContactIfNeed();
    }

    public void ForEachEntity(Action<int> callback)
    {
        if (_useReceiversList)
        {
            foreach (var pair in _entityContacts)
            {
                var (entity, col) = pair;

                if (_hitTaken.Contains(entity))
                    continue;

                _hitTaken.Add(entity);
                triggerPoint = col.transform.position;
                callback.Invoke(entity);
            }

            DeActivate();
        }
        else
        {
            foreach (var pair in _entityContacts)
            {
                var (entity, col) = pair;

                triggerPoint = col.transform.position;

                callback.Invoke(entity);
            }
        }
    }

    public int GetFirst() => _entityContacts.FirstOrDefault().Key;

    private void EntityRefDisabled(ConvertToEntity entityRef)
    {
        _entityContacts.Remove(entityRef.RawEntity);
        _hitTaken.Remove(entityRef.RawEntity);

        entityRef.OnEntityWasDeleted -= EntityRefDisabled;
        ClearContactIfNeed();
    }

    private void ClearContactIfNeed()
    {
        if (_entityContacts.Count != 0 || !_areaIsActive)
            return;

        DeActivate();
    }
}
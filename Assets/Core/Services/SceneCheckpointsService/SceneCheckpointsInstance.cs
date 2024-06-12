using System;
using Core;
using Lib;
using Reflex;
using UnityEngine;
using VInspector;

public class SceneCheckpointsInstance : MonoConstruct
{
    [SerializeField] private string _instanceUuid;
    [SerializeField] private Vector2 _positionAfterLoading;
    [SerializeField] private Animator _animator;
    private Context _context;
    private SceneCheckpointsService _service;
    private static readonly int StepNum = Animator.StringToHash("StepNum");

    [Button]
    public void RegenerateInstanceUuid() => _instanceUuid = Guid.NewGuid().ToString();

    protected override void Construct(Context context) => _context = context;

    private void Awake() => _service = _context.Resolve<SceneCheckpointsService>();

    private void Start()
    {
        if (_service.HasCheckpoint(_instanceUuid))
            Play();
    }

    private void OnTriggerEnter2D(Collider2D _)
    {
        _service.SaveCheckpoint(_instanceUuid, transform.TransformPoint(_positionAfterLoading));
        Play();
    }

    private void Play() => _animator.SetInteger(StepNum,0);
}
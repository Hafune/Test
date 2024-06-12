using System;
using Lib;
using Reflex;
using UnityEngine;
using VInspector;

namespace Core
{
    public class GemInstance : MonoConstruct
    {
        [field: SerializeField, ReadOnly] public string InstanceUuid { get; protected set; }
        private Context _context;
        private GemService _service;

        protected override void Construct(Context context) => _context = context;

        private void Awake() => _service = _context.Resolve<GemService>();

        private void OnEnable()
        {
            _service.OnDataChange += ReloadData;
            ReloadData();
        }

        private void OnDisable()
        {
            _service.OnDataChange -= ReloadData;
            InstanceUuid = string.Empty;
        }

        [Button]
        public void RegenerateInstanceUuid() => InstanceUuid = Guid.NewGuid().ToString();

        private void ReloadData()
        {
            if (_service.TryGetInstanceState(InstanceUuid))
                gameObject.SetActive(false);
        }
    }
}
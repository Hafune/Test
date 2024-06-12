using System;
using Core.Services;
using Lib;
using Reflex;
using UnityEngine;
using VInspector;

namespace Core
{
    public class KeySilverInstance : MonoConstruct
    {
        [field: SerializeField, ReadOnly] public string InstanceUuid { get; protected set; }
        private Context _context;
        private KeySilverService _service;

        protected override void Construct(Context context) => _context = context;

        private void Awake() => _service = _context.Resolve<KeySilverService>();
        
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

        [Button,ButtonSize(22)]
        public void RegenerateInstanceUuid() => InstanceUuid = Guid.NewGuid().ToString();

        private void ReloadData()
        {
            if (_service.TryGetInstanceState(InstanceUuid))
                gameObject.SetActive(false);
        }
    }
}
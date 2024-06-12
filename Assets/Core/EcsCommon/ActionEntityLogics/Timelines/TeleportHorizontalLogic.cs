using Core.Systems;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class TeleportHorizontalLogic : AbstractEntityLogic
    {
        [SerializeField] private Transform _root;
        private Transform _target;

        private void OnValidate() => _root = _root ? _root : transform.root;

        private void OnTriggerEnter2D(Collider2D col) => _target = col.transform;

        public override void Run(int entity) => _root.position = _root.position.Copy(x: _target.position.x);
    }
}
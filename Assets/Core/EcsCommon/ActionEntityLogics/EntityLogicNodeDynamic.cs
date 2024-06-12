using System.Linq;
using Core.Lib;
using Lib;
using UnityEngine;

namespace Core.Systems
{
    public class EntityLogicNodeDynamic : AbstractEntityLogic
    {
        private bool _isValidState;
        private AbstractEntityLogic[] _allLogics;
        private GameObject[] _objects;
        private readonly MyList<AbstractEntityLogic> _activeLogics = new();

        private void Awake()
        {
            _allLogics = transform.GetSelfChildrenComponents<AbstractEntityLogic>(true);
            _objects = _allLogics.Select(i => i.gameObject).ToArray();

            foreach (var logic in _allLogics)
            {
                var dispatcher = logic.gameObject.AddComponent<EnableDispatcher>();
                dispatcher.OnEnabled += MakeStateDirty;
                dispatcher.OnDisabled += MakeStateDirty;
            }

            ReSearchActiveLogics();
        }

        public override void Run(int entity)
        {
            if (!_isValidState)
                ReSearchActiveLogics();

            foreach (var logic in _activeLogics)
                logic.Run(entity);
        }

        private void MakeStateDirty() => _isValidState = false;

        private void ReSearchActiveLogics()
        {
            _activeLogics.Clear();

            for (int i = 0, iMax = _objects.Length; i < iMax; i++)
                if (_objects[i].activeSelf)
                    _activeLogics.Add(_allLogics[i]);

            _isValidState = true;
        }
    }
}
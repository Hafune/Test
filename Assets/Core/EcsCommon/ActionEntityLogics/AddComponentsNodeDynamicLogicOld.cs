using System;
using System.Linq;
using Core.Systems;
using UnityEngine;

namespace Core.Lib
{
    [Obsolete("Использовать " + nameof(EntityLogicNodeDynamic))]
    public class AddComponentsNodeDynamicLogicOld : AbstractEntityLogic
    {
        private bool _isValidState;
        private readonly MyList<AddComponentsLogic> _activeImpacts = new();
        private AddComponentsLogic[] _allImpacts;
        private GameObject[] _impactObjects;

        private void Awake()
        {
            _allImpacts = GetComponentsInChildren<AddComponentsLogic>(true);
            _impactObjects = _allImpacts.Select(i => i.gameObject).ToArray();

            foreach (var logic in _allImpacts)
            {
                if (logic.TryGetComponent<EnableDispatcher>(out _))
                    continue;

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

            for (int i = 0, iMax = _activeImpacts.Count; i < iMax; i++)
                _activeImpacts.Items[i].Run(entity);
        }

        private void MakeStateDirty() => _isValidState = false;

        private void ReSearchActiveLogics()
        {
            _activeImpacts.Clear();

            for (int i = 0, iMax = _impactObjects.Length; i < iMax; i++)
                if (_impactObjects[i].activeSelf)
                    _activeImpacts.Add(_allImpacts[i]);

            _isValidState = true;
        }
    }
}
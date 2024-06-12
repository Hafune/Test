using System;
using Core.Generated;
using Core.Systems;
using JetBrains.Annotations;
using Lib;
using UnityEngine;

namespace Core.BehaviorTree.Scripts
{
    public class BTreeNpcAction : MonoBehaviour
    {
        [field: SerializeField] public ActionEnum ActionEnum { get; private set; }
        [SerializeField, CanBeNull] private TriggerCounter2D _activateArea;

        private void OnValidate() => name = "BTree_" + Enum.GetName(typeof(ActionEnum), ActionEnum)?
            .FormatAddCharBeforeCapitalLetters(true, '_');

        public bool TriggerIsActive() => !_activateArea || _activateArea.Count > 0;
    }
}
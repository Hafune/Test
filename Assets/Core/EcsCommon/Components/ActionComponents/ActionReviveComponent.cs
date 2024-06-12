using System;
using Core.Generated;
using Core.Systems;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct ActionReviveComponent : IActionComponent
    {
        private static ActionEnum? _actionEnum;
        public ActionEnum actionEnum => _actionEnum ??= ActionSystemsSchema.Get(this);
        [field: SerializeField] public AbstractActionEntityLogic logic { get; set; }
    }
}
using System;
using Core.Generated;
using Core.Systems;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct NpcActionComponent : IActionComponent
    {
        private static ActionEnum? _actionEnum;
        public ActionEnum actionEnum => _actionEnum ??= ActionSystemsSchema.Get(this);
        [field: SerializeField] public AbstractActionEntityLogic logic { get; set; }
        [field: SerializeField] public AbstractActionEntityLogic nextLogic { get; set; }
    }
}
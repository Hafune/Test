using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct NameValueComponent : IStringValue
    {
        [field: SerializeField] public string value { get; set; }
    }
}
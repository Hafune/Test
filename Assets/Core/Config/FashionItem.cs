using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    [CreateAssetMenu(menuName = "Game Config/" + nameof(FashionItem))]
    public class FashionItem : ScriptableObject
    {
        [field: SerializeField] public ReferencePath PathReference { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public int Cost { get; private set; }
    }
}
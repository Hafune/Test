using UnityEngine;

namespace Core.Services.SpriteForInputService
{
    [CreateAssetMenu(menuName = "Game Config/" + nameof(EditorDefaultInputSpritesData))]
    public class EditorDefaultInputSpritesData : ScriptableObject
    {
        [field: SerializeField] public InputSpritesData Data { get; private set; }
    }
}
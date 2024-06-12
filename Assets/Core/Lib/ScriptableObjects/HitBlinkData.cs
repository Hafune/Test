using UnityEngine;

[CreateAssetMenu(menuName = "HitBlinkData/" + nameof(HitBlinkData))]
public class HitBlinkData : ScriptableObject
{
    [SerializeField] public Material material;
    [SerializeField] public float time;
}
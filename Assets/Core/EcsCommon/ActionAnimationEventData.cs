using System;
using UnityEngine;

[Serializable, CreateAssetMenu(menuName = "Game Config/" + nameof(ActionAnimationEventData))]
public class ActionAnimationEventData : ScriptableObject
{
    [field: SerializeField] public ActionAnimationEventHandler.Parameters Parameter { get; private set; }
}
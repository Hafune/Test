using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    [CreateAssetMenu(menuName = "Game Config/" + nameof(UiSfxTemplate))]
    public class UiSfxTemplate : ScriptableObject
    {
        [field: SerializeField] public GameObject SelectSourceContainer { get; private set; }
        [field: SerializeField] public GameObject SubmitSourceContainer { get; private set; }
        [field: SerializeField] public GameObject CancelSourceContainer { get; private set; }
        [field: SerializeField] public GameObject TriggerSourceContainer { get; private set; }

        private void OnValidate()
        {
            if (!IsValidateObject(SelectSourceContainer))
                SelectSourceContainer = null;
            if (!IsValidateObject(SubmitSourceContainer))
                SubmitSourceContainer = null;
            if (!IsValidateObject(CancelSourceContainer))
                CancelSourceContainer = null;
            if (!IsValidateObject(TriggerSourceContainer))
                TriggerSourceContainer = null;
        }

        private bool IsValidateObject(GameObject go) => go && go.GetComponent<AudioSource>() != null;
    }
}
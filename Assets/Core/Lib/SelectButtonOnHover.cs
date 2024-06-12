using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SelectButtonOnHover : MonoBehaviour, IPointerEnterHandler
{
    public event Action OnSelect;
    
    [SerializeField] private Button _button;

    private void OnValidate() => _button = _button ? _button : GetComponent<Button>();

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_button.isActiveAndEnabled || !_button.interactable)
            return;

        _button.Select();
        OnSelect?.Invoke();
    }
}
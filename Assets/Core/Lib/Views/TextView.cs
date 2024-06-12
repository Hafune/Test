using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _pattern;

    public void OnValidate() => _text = _text ? _text : GetComponent<TextMeshProUGUI>();

    public void SetText(string value) => _text.text = value;
    public void SetText(int value) => _text.text = value.ToString();
    public void SetText(float value) => _text.text = value.ToString();

    public void FormatText<T>(params T[] args) =>
        _text.text = string.Format(_pattern, args.Select(i => i.ToString()).ToArray());

    public void FormatText(int arg0) => _text.text = string.Format(_pattern, arg0);
    public void FormatText(float arg0) => _text.text = string.Format(_pattern, arg0);

    public void FormatText(string arg0) => _text.text = string.Format(_pattern, arg0);
}
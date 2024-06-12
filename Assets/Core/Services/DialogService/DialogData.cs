using System;
using System.Collections.Generic;
using System.Text;
using Lib;
using LurkingNinja.MyGame.Internationalization;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

[Serializable]
[CreateAssetMenu(menuName = "Game Config/" + nameof(DialogData))]
public class DialogData : ScriptableObject
{
    [SerializeField] private LocalizedString localizedDialog;
    [NonSerialized] private List<DialogMessage> _messages;
    private static readonly List<DialogMessage> _emptyMessages = new();
    private const string dir = "Icons/charface";

    // Словарь для кеширования спрайтов
    private static readonly Dictionary<string, Sprite> _spriteCache = new();

    public IReadOnlyList<DialogMessage> Messages => _messages ??= ParseLocalizedDialog();

    private void OnEnable()
    {
        LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;

        if (_spriteCache.Count == 0)
            foreach (var sprite in Resources.LoadAll<Sprite>(dir))
                _spriteCache[sprite.name] = sprite;
    }

    private void OnDisable() => LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;

    // сброс кеша сообщений при смене локализации
    private void OnLocaleChanged(Locale obj) => _messages = null;

#if UNITY_EDITOR
    [SerializeField] private List<DialogMessage> testMessages;

    [VInspector.Button]
    private void Test() => testMessages = ParseLocalizedDialog();
#endif
    private List<DialogMessage> ParseLocalizedDialog()
    {
        if (localizedDialog == null || localizedDialog.IsEmpty)
        {
            Debug.LogError($"{nameof(localizedDialog)} is empty");
            return _emptyMessages;
        }

        var messages = new List<DialogMessage>();
        var rawText = localizedDialog.GetLocalizedString();
        var lines = rawText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

        var currentMessage = new DialogMessage();
        var messageBuilder = new StringBuilder();

        foreach (var line in lines)
        {
            if (line.StartsWith("--"))
            {
                currentMessage.Message = messageBuilder.ToString().Trim();
                messages.Add(currentMessage);

                currentMessage = new DialogMessage();
                messageBuilder.Clear();
                continue;
            }

            if (line.StartsWith("icon="))
            {
                var iconName = ExtractValue(line, "icon");
                currentMessage.icon = GetCachedSprite(iconName);
                continue;
            }

            if (line.StartsWith("title="))
            {
                var key = ExtractValue(line, "title");
                currentMessage.Title = I18N.DialogTitles.Table.GetLocalizedString(key);
                continue;
            }

            if (!line.StartsWith("icon=") && !line.StartsWith("title="))
            {
                messageBuilder.AppendLine(line);
            }
        }

        currentMessage.Message = messageBuilder.ToString().Trim();
        messages.Add(currentMessage);

        return messages;
    }

    private Sprite GetCachedSprite(string iconName) => _spriteCache[iconName];

    private string ExtractValue(string input, string key)
    {
        var startIndex = input.IndexOf(key + "=") + key.Length + 1;
        return input.Substring(startIndex).Trim();
    }

    [Serializable]
    public class DialogMessage
    {
        [ShowAssetPreview] public Sprite icon;
        [SerializeField] public string Title;
        [SerializeField, TextArea] public string Message;
    }
}
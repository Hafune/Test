using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Lib.Views
{
    [RequireComponent(typeof(VerticalLayoutGroup))]
    public class QualitySettingsButtons : MonoBehaviour
    {
        [SerializeField] private Button _buttonPrefab;

        private void Awake()
        {
            string[] names = QualitySettings.names;
            for (int i = 0; i < names.Length; i++)
            {
                var btn = Instantiate(_buttonPrefab, transform);
                btn.GetComponentInChildren<TextMeshProUGUI>().text = names[i];

                var index = i;
                btn.onClick.AddListener(() => QualitySettings.SetQualityLevel(index, true));
            }

            Destroy(_buttonPrefab.gameObject);
        }
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Lib.Views
{
    [RequireComponent(typeof(VerticalLayoutGroup))]
    public class ResolutionSettingsButtons : MonoBehaviour
    {
        [SerializeField] private Button _buttonPrefab;

        private void Awake()
        {
            Resolution[] models = Screen.resolutions;
            for (int i = 0; i < models.Length; i++)
            {
                var btn = Instantiate(_buttonPrefab, transform);
                btn.GetComponentInChildren<TextMeshProUGUI>().text = models[i].ToString();

                var model = models[i];
                btn.onClick.AddListener(() => Screen.SetResolution(model.width, model.height, false));
            }

            Destroy(_buttonPrefab.gameObject);
        }
    }
}
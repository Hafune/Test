using System.Linq;
using UnityEngine;

namespace Core.Lib
{
    public class SyncActiveState : MonoBehaviour
    {
        [SerializeField] private GameObject[] _controlledObjects;

        private void OnValidate() => _controlledObjects = _controlledObjects.Distinct().ToArray();

        private void OnEnable()
        {
            for (int i = 0, iMax = _controlledObjects.Length; i < iMax; i++)
                _controlledObjects[i].SetActive(true);
        }

        private void OnDisable()
        {
            for (int i = 0, iMax = _controlledObjects.Length; i < iMax; i++)
                _controlledObjects[i].SetActive(false);
        }

        public void Activate() => gameObject.SetActive(true);
        public void DeActivate() => gameObject.SetActive(false);
    }
}
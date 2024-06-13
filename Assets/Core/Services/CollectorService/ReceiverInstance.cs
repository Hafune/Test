using Lib;
using UnityEngine;

namespace Core
{
    public class ReceiverInstance : MonoBehaviour
    {
        [SerializeField] private Collider _receiveArea;
        [SerializeField, Layer] private int _workingLayer;
        private int _defaultLayer;

        private void Awake() => _defaultLayer = _receiveArea.gameObject.layer;

        public void Activate() => _receiveArea.gameObject.layer = _workingLayer;
        public void Deactivate() => _receiveArea.gameObject.layer = _defaultLayer;
    }
}
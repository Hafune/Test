using System;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace Core.Systems
{
    public class ActionClimbingCollision : MonoBehaviour, ICollision2D
    {
        [SerializeField] private ConvertToEntity _convertToEntity;
        public Action<int> OnContact;

        public void Activate() => gameObject.SetActive(true);
        public void Deactivate() => gameObject.SetActive(false);

        public void OnCollisionEnter2D(Collision2D col)
        {
            if (_convertToEntity.RawEntity != -1)
                OnContact?.Invoke(_convertToEntity.RawEntity);
        }

        public void OnCollisionExit2D(Collision2D col)
        {
        }
    }
}
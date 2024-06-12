using UnityEngine;

namespace Core.Lib
{
    public class DisableOverTime : MonoBehaviour
    {
        [SerializeField] private float _time = .5f;
        private float _currentTime;

        private void OnEnable() => _currentTime = 0;

        private void FixedUpdate()
        {
            if ((_currentTime += Time.deltaTime) >= _time)
                gameObject.SetActive(false);
        }
    }
}
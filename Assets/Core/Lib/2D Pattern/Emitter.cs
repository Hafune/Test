using UnityEngine;

namespace Core.Lib
{
    public class Emitter : MonoBehaviour
    {
        [field: SerializeField] public Transform Point { get; private set; }

        private void OnValidate()
        {
            var e = transform.GetEnumerator();

            if (e.MoveNext())
                Point = (Transform)e.Current;
        }
    }
}
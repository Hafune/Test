using UnityEngine;

namespace Core.Lib
{
    public class SpawnParticleRuntime : MonoBehaviour
    {
        [SerializeField] public Transform prefab;

        public void Awake()
        {
            var prefab = Instantiate(this.prefab, transform.position, transform.rotation, transform.parent);
            prefab.localScale = transform.localScale;
            prefab.name = name;
            Destroy(gameObject);
        }
    }
}
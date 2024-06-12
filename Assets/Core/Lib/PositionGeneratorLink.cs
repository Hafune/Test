using UnityEngine;

namespace Core.Lib
{
    public class PositionGeneratorLink : MonoBehaviour, IPositionGenerator
    {
        [SerializeField] private MonoBehaviour iPositionGenerator;

        private void OnValidate()
        {
            if (iPositionGenerator is not IPositionGenerator)
                iPositionGenerator = null;

            iPositionGenerator = iPositionGenerator
                ? iPositionGenerator
                : GetComponentsInChildren<IPositionGenerator>()[0] as MonoBehaviour;

            if (iPositionGenerator == this)
                iPositionGenerator = null;
        }

        public Vector3 GeneratePositionXZ() => ((IPositionGenerator)iPositionGenerator).GeneratePositionXZ();
    }
}
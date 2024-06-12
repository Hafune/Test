using UnityEngine;
using UnityEngine.UI;

namespace Core.Views
{
    [RequireComponent(typeof(Image))]
    public class ImageTextureMover : MonoBehaviour
    {
        [SerializeField] private Vector2 _speed;
        [SerializeField] private Renderer _renderer;
        private Image _image;

        // private void Update() => _renderer.material.uv += _speed * Time.unscaledDeltaTime;
    }
}
using System.Collections;
using UnityEngine;

public class HitBlinkSprite : MonoBehaviour
{
    private static readonly int HitEffectBlend = Shader.PropertyToID("_HitEffectBlend");

    [SerializeField] private SpriteRenderer _image;
    private MyCoroutine _coroutine;
    private WaitForSeconds _wait;

    private void OnValidate() => _image = _image ? _image : GetComponentInChildren<SpriteRenderer>();

    private void Awake()
    {
        _wait = new WaitForSeconds(.1f);
        _coroutine = new MyCoroutine(this, BeginBlink);
    }

    private void OnDisable()
    {
        _coroutine.StopCoroutine();
        Clear();
    }

    public void Activate() => _coroutine.StartCoroutine(false);

    private IEnumerator BeginBlink()
    {
        _image.material.SetFloat(HitEffectBlend, .1f);

        yield return _wait;

        Clear();
    }

    private void Clear() => _image.material.SetFloat(HitEffectBlend, 0);
}
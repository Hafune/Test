using System.Collections;
using UnityEngine;

public class FracturedObject : MonoBehaviour
{
    private FracturedChunk[] ListFracturedChunks;
    private Coroutine _coroutine;
    
    private void Awake() => ListFracturedChunks = GetComponentsInChildren<FracturedChunk>();

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_coroutine is not null)
            return;

        Explode(col.transform.position, 150f);
    }

    // Token: 0x060001F1 RID: 497 RVA: 0x0000FBD0 File Offset: 0x0000DDD0
    private void Explode(Vector3 v3ExplosionPosition, float fExplosionForce)
    {
        _coroutine = StartCoroutine(RunAnimation(8));

        foreach (var chunk in ListFracturedChunks)
        {
            chunk.EnablePhysics();
            chunk.GetComponent<Rigidbody>()
                .AddExplosionForce(fExplosionForce, v3ExplosionPosition, 0f, 0f);
        }
    }

    private IEnumerator RunAnimation(float disableTime)
    {
        yield return new WaitForSeconds(disableTime);
        gameObject.SetActive(false);
        _coroutine = null;
    }

    private void OnDisable()
    {
        foreach (var fracturedChunk in ListFracturedChunks)
            fracturedChunk.ResetChunk();

        if (_coroutine is not null)
            StopCoroutine(_coroutine);

        _coroutine = null;
    }
}
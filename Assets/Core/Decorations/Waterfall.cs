using UnityEngine;

public class Waterfall : MonoBehaviour
{
    [SerializeField] private float speed;
    private float height;
    private Vector3 startPosition;
    private bool _isRootScript = true;

    private void Start()
    {
        var bounds = GetComponent<MeshRenderer>().bounds;
        height = bounds.size.y * 2;
        transform.position += new Vector3(0, bounds.size.y / 2, 0);
        startPosition = transform.position;

        if (_isRootScript)
        {
            var secondScript = Instantiate(this, transform.parent);
            secondScript._isRootScript = false;
            secondScript.transform.position = transform.position + new Vector3(0, bounds.size.y / 2, 0);
            secondScript.GetComponent<MeshRenderer>().sharedMaterial = GetComponent<MeshRenderer>().sharedMaterial;
        }

        speed *= height;
    }

    private void FixedUpdate()
    {
        float value = transform.position.y + speed * Time.deltaTime - startPosition.y;
        value %= height;
        transform.position = startPosition + new Vector3(0, value, 0);
    }
}
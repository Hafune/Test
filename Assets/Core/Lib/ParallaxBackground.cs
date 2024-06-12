using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMultiplier;
    [SerializeField] private Vector2 force;
    [SerializeField] private bool infiniteHorizontal;
    [SerializeField] private bool infiniteVertical;

    private Transform cameraTransform;
    private Vector3 startCameraPosition;
    private Vector3 startDifference;
    private Vector3 startPosition;
    private Vector2 forcePosition;
    private float textureUnitSizeX;
    private float textureUnitSizeY;

    private void Start()
    {
        cameraTransform = Camera.main!.transform;
        startCameraPosition = cameraTransform.position;
        var sprite = GetComponent<SpriteRenderer>().sprite;
        var size = sprite.textureRect;

        var lossyScale = transform.lossyScale;

        textureUnitSizeX = size.width / sprite.pixelsPerUnit * lossyScale.x;
        textureUnitSizeY = size.height / sprite.pixelsPerUnit * lossyScale.y;
        startPosition = transform.position;
    }

    private void LateUpdate()
    {
        forcePosition += force * Time.deltaTime;
        forcePosition.x %= textureUnitSizeX;
        forcePosition.y %= textureUnitSizeY;
        
        var deltaMovement = cameraTransform.position - startCameraPosition;
        var offset = deltaMovement * parallaxEffectMultiplier + forcePosition;

        var infiniteOffsetX = 0f;
        var infiniteOffsetY = 0f;

        if (infiniteHorizontal)
        {
            infiniteOffsetX = (int) (deltaMovement.x / textureUnitSizeX) * textureUnitSizeX;
            offset.x %= textureUnitSizeX;
        }

        if (infiniteVertical)
        {
            infiniteOffsetY = (int) (deltaMovement.y / textureUnitSizeY) * textureUnitSizeY;
            offset.y %= textureUnitSizeY;
        }

        transform.position = new Vector3(
            startPosition.x + infiniteOffsetX + offset.x,
            startPosition.y + infiniteOffsetY + offset.y,
            transform.position.z
        );
    }
}
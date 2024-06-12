using UnityEngine;

public class EffectFollower : MonoBehaviour
{
    private Transform _follow;

    private void OnDisable()
    {
        _follow = null;
    }

    private void Update()
    {
        // var isTargetActive = _follow != null && _follow.gameObject.activeSelf;
        //
        // if (isTargetActive)
        //     transform.position = _follow.position;
        // else
        //     _follow = null;
    }

    public void SetFollow(Transform follow) => _follow = follow;
}
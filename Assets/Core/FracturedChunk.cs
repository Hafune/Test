using System;
using UnityEngine;

[Serializable]
public class FracturedChunk : MonoBehaviour
{
    private Vector3 m_v3InitialLocalPosition;
    private Quaternion m_qInitialLocalRotation;
    private Vector3 m_v3InitialLocalScale;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        m_v3InitialLocalPosition = transform.localPosition;
        m_qInitialLocalRotation = transform.localRotation;
        m_v3InitialLocalScale = transform.localScale;
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Token: 0x060001DA RID: 474 RVA: 0x0000F0D8 File Offset: 0x0000D2D8
    public void ResetChunk()
    {
        if (!_rigidbody)
            Awake();
        
        _rigidbody.isKinematic = true;
        transform.localPosition = m_v3InitialLocalPosition;
        transform.localRotation = m_qInitialLocalRotation;
        transform.localScale = m_v3InitialLocalScale;
    }

    public void EnablePhysics() => _rigidbody.isKinematic = false;
}
using System;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("EXIT");
    }
}
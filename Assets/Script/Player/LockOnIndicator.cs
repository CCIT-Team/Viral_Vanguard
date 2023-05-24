using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnIndicator : MonoBehaviour
{
    public Transform myCamera;

    private void Awake()
    {
        myCamera = Camera.main.transform;
    }

    private void Update()
    {
        transform.LookAt(transform.position + myCamera.rotation * Vector3.forward, myCamera.rotation * Vector3.up);
    }
}



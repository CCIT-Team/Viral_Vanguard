using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateObject : MonoBehaviour
{
    public float rotateSpeed;
    public bool isYrotate = true;
    void Update()
    {
        if(isYrotate)
        {
            transform.Rotate(Vector3.left * rotateSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }
    }
}

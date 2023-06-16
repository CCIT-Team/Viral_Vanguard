using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieCam : MonoBehaviour
{
    public float movespeed;
    public float rotatespeed;

    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        transform.localPosition += Vector3.forward * horizontal * movespeed * Time.deltaTime;
        transform.Rotate(Vector3.up * vertical * rotatespeed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            movespeed += 10;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            movespeed -= 10;
        }
    }
}

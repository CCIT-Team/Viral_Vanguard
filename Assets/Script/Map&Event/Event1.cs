using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event1 : MonoBehaviour
{
    public Animator ani;
    public Camera playerCam;
    public GameObject event1;

    private void Awake()
    {
        if(ani == null){ani = null;}

    }
    private void OnTriggerEnter(Collider other)
    {
        event1.SetActive(true);
        ani.SetTrigger("Q");
        playerCam.gameObject.SetActive(false);
        Destroy(this);
    }
}

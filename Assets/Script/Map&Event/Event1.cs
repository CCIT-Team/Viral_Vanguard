using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event1 : MonoBehaviour
{
    public Animation ani;

    private void OnTriggerEnter(Collider other)
    {
        ani.Play();
    }
}

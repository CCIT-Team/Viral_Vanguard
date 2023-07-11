using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedArmsSoundTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}

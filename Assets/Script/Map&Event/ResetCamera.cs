using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCamera : MonoBehaviour
{
    public Camera PlayerCam;

    private void Update()
    {
        if (gameObject.activeInHierarchy == true)
        {
            StartCoroutine(ResetCam());
        }
    }
    IEnumerator ResetCam()
    {
        if (PlayerCam.enabled == true){ Debug.Log("Turn OFF"); PlayerCam.enabled = false;}
        yield return new WaitForSecondsRealtime(0.05f);
        if (PlayerCam.enabled == false) { Debug.Log("Turn ON"); PlayerCam.enabled = true; gameObject.SetActive(false); }
    }
}

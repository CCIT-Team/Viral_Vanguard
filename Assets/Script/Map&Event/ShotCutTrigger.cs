using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotCutTrigger : MonoBehaviour
{
    public ShotCutNumber shotCutNumber;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && shotCutNumber == ShotCutNumber.ShotCut0)
        {
            ShotCut.shotcutOpen0 = true;
        }
        if (other.CompareTag("Player") && shotCutNumber == ShotCutNumber.ShotCut1)
        {
            ShotCut.shotcutOpen1 = true;
        }
    }

    public enum ShotCutNumber
    { 
     ShotCut0,
     ShotCut1  
    };
}

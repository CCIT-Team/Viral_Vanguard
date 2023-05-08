using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRange : MonoBehaviour //임시코드
{
    public EVENT_TYPE eventtype;
    bool inRange = false;
    bool InRange
    {
        get { return inRange; }
        set
        {
            inRange = value;
            EventManager.instance.PostNotification(eventtype, this, InRange);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        InRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        InRange = false;
    }
}

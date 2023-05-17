using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAnimationEvent : MonoBehaviour
{
    void HideThis()
    {
        this.gameObject.SetActive(false);
    }

    void Q()
    {
        this.GetComponent<Animator>().SetTrigger("Q");
    }

    void SlowTime()
    {
        Time.timeScale = 0.1f;
    }
    
    void OriginTime()
    {
        Time.timeScale = 1;
    }
}

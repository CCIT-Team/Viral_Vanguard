using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventListener : MonoBehaviour, IListener
{
    float sampleValue = 100;
    
    public float SampleValue
    {
        get { return sampleValue; }
        set
        {
            sampleValue = value;
            EventManager.instance.PostNotification(EVENT_TYPE.eSampleEvent, this, SampleValue);
        }
    }
    
    void Start()
    {
        EventManager.instance.AddListener(EVENT_TYPE.eSampleEvent, this);
        EventManager.instance.AddListener(EVENT_TYPE.eFindPlayer, this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SampleValue -= 20;
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            SampleValue -= 50;
        }

    }

    public void OnEvent(EVENT_TYPE EventType, Component Sender, object Param = null)
    {
        switch(EventType)
        {
            case EVENT_TYPE.eFindPlayer:
                TrackingStart();
                break;
            case EVENT_TYPE.eHit:
                WeightChange(0);
                break;
            case EVENT_TYPE.eParried:
                Parried();
                WeightChange(1);
                break;
            case EVENT_TYPE.eGuarded:
                WeightChange(2);
                break;
            case EVENT_TYPE.eNormalRange1:
                RangeCheck();
                break;
            case EVENT_TYPE.eNormalRange2:
                RangeCheck();
                break;
            case EVENT_TYPE.eActionRange1:
                RangeCheck();
                break;
            case EVENT_TYPE.eSpecialRange1:
                RangeCheck();
                break;
            case EVENT_TYPE.eSpecialRange2:
                RangeCheck();
                break;
            case EVENT_TYPE.eSpecialRange3:
                RangeCheck();
                break;
        }
    }

    void TrackingStart() { }
    void RangeCheck() { }
    void Parried() { }
    void WeightChange(int i) { }
}

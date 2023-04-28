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
            case EVENT_TYPE.eSampleEvent:
                Debug.Log("이벤트 실행. HP : "+ Param);
                break;
        }
    }
}

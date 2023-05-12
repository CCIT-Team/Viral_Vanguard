using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveControler : MonoBehaviour, IListener
{
    //일정시간마다 행동?
    public float actTime = 5;
    float timeGo = 0;

    BossActionBase actionBase;

     void Awake()
    {
        actionBase = GetComponent<BossActionBase>();
    }
    void Start()
    {
        EventManager.instance.AddListener(EVENT_TYPE.eFindPlayer, this);
    }

    void Update()
    {
        timeGo += Time.deltaTime;
        if (actTime <= timeGo)
        {
            EventManager.instance.PostNotification(EVENT_TYPE.eActTime, this);
            timeGo = 0;
        }
    }

    public void OnEvent(EVENT_TYPE EventType, Component Sender, object Param = null)
    {
        switch (EventType)
        {
            case EVENT_TYPE.eActTime:
                break;
            case EVENT_TYPE.eGuarded:
                break;
            case EVENT_TYPE.eParried:
                break;
            case EVENT_TYPE.eNormalRange1:
                break;
            case EVENT_TYPE.eNormalRange2:
                break;
            case EVENT_TYPE.eSpecialRange1:
                break;
            case EVENT_TYPE.eSpecialRange2:
                break;
            case EVENT_TYPE.eSpecialRange3:
                break;
            case EVENT_TYPE.eEnterBattle:
                break;
            case EVENT_TYPE.eDead:
                break;
        }
    }
}

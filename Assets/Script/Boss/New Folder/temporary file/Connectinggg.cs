using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//임시 연결용 스크립트. 삭제 예정
public class Connectinggg : MonoBehaviour, IListener
{
    public BossMove bm;
    void Start()
    {
        EventManager.instance.AddListener(EVENT_TYPE.eNormalRange1, this);
        EventManager.instance.AddListener(EVENT_TYPE.eSpecialRange1, this);
        EventManager.instance.AddListener(EVENT_TYPE.eSpecialRange3, this);
        EventManager.instance.AddListener(EVENT_TYPE.eFindPlayer, this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEvent(EVENT_TYPE EventType, Component Sender, object Param = null)
    {
        switch(EventType)
        {
            //case EVENT_TYPE.eDefaultRange1:
            //    bm.NormalRange = (bool)Param;
            //    break;
            //case EVENT_TYPE.eSpecialRange1:
            //    bm.SpecialRange = (bool)Param;
            //    break;
            //case EVENT_TYPE.eSpecialRange3:
            //    bm.InductionRange = (bool)Param;
            //    break;
            //case EVENT_TYPE.eFindPlayer:
            //    break;
        }
    }
}

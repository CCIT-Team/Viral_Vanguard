using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlayerInput : MonoBehaviour, IListener
{
    
    void Start()
    {
     /*플레이어(가) 때림 = 보스 맞음 = trigger
       플레이어가 멀어짐 = 보스 사거리 밖 = trigger range
       플레이어가 막음 = 보스 막힘 = player animation / 
       플레이어가 패링 = 보스 경직
       플레이어가 다가옴 = 보스 사거리 안
       플레이어가 회복함 = ?
       */  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEvent(EVENT_TYPE EventType, Component Sender, object Param = null)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlayerInput : MonoBehaviour, IListener
{
    
    void Start()
    {
     /*�÷��̾�(��) ���� = ���� ���� = trigger
       �÷��̾ �־��� = ���� ��Ÿ� �� = trigger range
       �÷��̾ ���� = ���� ���� = player animation / 
       �÷��̾ �и� = ���� ����
       �÷��̾ �ٰ��� = ���� ��Ÿ� ��
       �÷��̾ ȸ���� = ?
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

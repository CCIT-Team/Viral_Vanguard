using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    public Image fillImage;
    public float maxHP;
    public float currentHP;

    //public BossStatus boss1;
    //public BossStatus boss1;
    //public BossStatus boss1;
    //public BossStatus boss1;
    //public BossStatus boss1;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            //maxHP = boss1.maxHp;
            //currentHP = boss1.currentHp;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentHP -= 10;
            HpUpdate();
        }
    }

    void HpUpdate()
    {
        fillImage.fillAmount = currentHP / maxHP;
    }
}

//public class BossStatus
//{
//    public int maxHp;
//    public int currentHp;
//    public string name;
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public Text bossnametext;
    public string bossname;
    void Start()
    {
        bossnametext.text = bossname;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

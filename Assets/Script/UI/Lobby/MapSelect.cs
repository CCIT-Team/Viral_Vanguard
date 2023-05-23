using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour
{
    public GameObject section;
    public Image sectionClick;

    void CursorOnTheObjectroof()
    {
        sectionClick.material.color = new Color(0, 0, 75, 100);
    }

    

   
}

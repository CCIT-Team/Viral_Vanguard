using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    /// <summary>
    /// Main Scene Elements
    /// </summary>
    public ButtonArrays buttonArrays;


    /// <summary>
    /// Main Scene Functions
    /// </summary>
    public void SelectSaveDataList(GameObject savedatalistpanel)
    {
        savedatalistpanel.SetActive(true);
    }

    public void GameClose()
    {
        Debug.Log("게임을 종료합니다.");
        Application.Quit();
    }

    /// <summary>
    /// Integration
    /// </summary>
    /// 
    public void BackButton(GameObject targetobject)
    {
        targetobject.SetActive(false);
    }
}

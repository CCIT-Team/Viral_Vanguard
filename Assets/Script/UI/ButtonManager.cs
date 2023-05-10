using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    /// <summary>
    /// Main Scene Functions
    /// </summary>
   public void Continue()
    {
        SaveDataManager.Instance.currentSaveFileIndex = 0;
        SaveDataManager.Instance.GameSave("/SaveFile" + 0 + ".txt",0);
        SaveDataManager.Instance.GameLoad();

    }

    public void SaveDataSelect(int index)
    {
        SaveDataManager.Instance.currentSaveFileIndex = index;
    }

    public void GameStartInSaveDataList()
    {
        SaveDataManager.Instance.GameSave(
            "/SaveFile" + SaveDataManager.Instance.currentSaveFileIndex + ".txt",
            SaveDataManager.Instance.currentSaveFileIndex);
        SaveDataManager.Instance.GameLoad();
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
}

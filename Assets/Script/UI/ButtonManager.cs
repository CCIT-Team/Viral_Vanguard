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

    public void NewGame(GameObject saveDataMax)
    {
        if(SaveDataManager.Instance.saveDataCount < 5)
        {
            int count = SaveDataManager.Instance.saveDataCount;
            SaveDataManager.Instance.GameSave(
                "/SaveFile" + 
                count + 
                ".txt", 
                count);
            SaveDataManager.Instance.saveDataCount++;
        }
        else if(SaveDataManager.Instance.saveDataCount == 5)
        {
            saveDataMax.SetActive(true);
            //UIManager.Instance.OpenUI(saveDataMax);
        }
    }

    public void MaxSaveDataInNewGame(GameObject pleaseDeleteSaveData)
    {
        if(SaveDataManager.Instance.saveDataCount == 5)
        {
            pleaseDeleteSaveData.SetActive(true);
        }
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

using JetBrains.Annotations;
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

        SceneFader.Instance.StartFadeOut("Lobby",3f);
    }

    public void SaveDataSelect(int index)
    {
        SaveDataManager.Instance.currentSaveFileIndex = index;
        SaveDataManager.Instance.GameSave("/SaveFile" + index + ".txt", index);
    }

    public void GameStartInSaveDataList()
    {
        SaveDataManager.Instance.GameSave(
            "/SaveFile" + SaveDataManager.Instance.currentSaveFileIndex + ".txt",
            SaveDataManager.Instance.currentSaveFileIndex);
        SaveDataManager.Instance.GameLoad();

        SceneFader.Instance.StartFadeOut("Lobby",3f);
    }

    public void NewGame(GameObject saveDataMax)
    {
        if(SaveDataManager.Instance.saveDataCount < 5)
        {
            int count = SaveDataManager.Instance.saveDataCount;
            SaveDataManager.Instance.GameSave("/SaveFile" + count + ".txt", count);
            SaveDataManager.Instance.saveDataCount++;
            SceneFader.Instance.StartFadeOut("Lobby",3f);
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
            UIManager.Instance.OpenUI(pleaseDeleteSaveData);
        }
    }

    public void SaveDataDelete()
    {
        int dataIndex = SaveDataManager.Instance.currentSaveFileIndex;
        SaveDataManager.Instance.DeleteGameData(dataIndex);
        //SaveDataManager.Instance.GameSave("/SaveFile" + dataIndex + ".txt", dataIndex);

        if (dataIndex == 0)
        {
            SaveDataManager.Instance.saveData0 = new SaveData();
            SaveDataManager.Instance.saveDatas[dataIndex] = SaveDataManager.Instance.saveData0;
        }
        else if (dataIndex == 1)
        {
            SaveDataManager.Instance.saveData1 = new SaveData();
            SaveDataManager.Instance.saveDatas[dataIndex] = SaveDataManager.Instance.saveData1;
        }
        else if (dataIndex == 2)
        {
            SaveDataManager.Instance.saveData2 = new SaveData();
            SaveDataManager.Instance.saveDatas[dataIndex] = SaveDataManager.Instance.saveData2;
        }
        else if (dataIndex == 3)
        {
            SaveDataManager.Instance.saveData3 = new SaveData();
            SaveDataManager.Instance.saveDatas[dataIndex] = SaveDataManager.Instance.saveData3;
        }
        else if (dataIndex == 4)
        {
            SaveDataManager.Instance.saveData4 = new SaveData();
            SaveDataManager.Instance.saveDatas[dataIndex] = SaveDataManager.Instance.saveData4;
        }

        SaveDataManager.Instance.GameSave("/SaveFile" + dataIndex + ".txt", dataIndex);

        SceneFader.Instance.StartFadeOut("Lobby",3f);
    }

    public void GO()
    {
        int dataIndex = SaveDataManager.Instance.currentSaveFileIndex;
        SaveDataManager.Instance.GameSave("/SaveFile" + dataIndex + ".txt", dataIndex);

        SceneFader.Instance.StartFadeOut("Lobby", 3f);
    }

    public void GameClose()
    {
        Debug.Log("게임을 종료합니다.");
        TimeManager.Instance.SetTimeValue();
        SaveDataManager.Instance.GameSave(
                "/SaveFile" + SaveDataManager.Instance.saveFileNumber[SaveDataManager.Instance.currentSaveFileIndex] + ".txt",
                SaveDataManager.Instance.currentSaveFileIndex);
        Application.Quit();
    }

    /// <summary>
    /// Integration
    /// </summary>
    /// 
}

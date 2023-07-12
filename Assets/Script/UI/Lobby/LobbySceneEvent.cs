using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

// �ش� ��ũ��Ʈ�� Virual_Vanguard ������Ʈ�� Lobby Scene ������ ���� �մϴ�.

public class LobbySceneEvent : MonoBehaviour
{
    public GameObject[] panels;
    public Sprite[] lobbyBackGrounds;
    public Image backgroundImage;

    /// <summary>
    /// Operations Panel
    /// </summary>
    public MapSelect[] mapSelects;
    public GameObject[] targetBossInfo;

    /// <summary>
    /// 
    /// </summary>
    public GameObject[] operatorCall;


    public void Start()
    {
        if (!SaveDataManager.Instance.saveDatas[SaveDataManager.Instance.currentSaveFileIndex].isLobbyTutorialClear)
            operatorCall[0].SetActive(true);
        if (!SaveDataManager.Instance.saveDatas[SaveDataManager.Instance.currentSaveFileIndex].isStateTutorialClear)
            operatorCall[1].SetActive(true);
        if (!SaveDataManager.Instance.saveDatas[SaveDataManager.Instance.currentSaveFileIndex].isOperatorTutorialClear)
            operatorCall[2].SetActive(true);
    }

    #region For The Button Event

    public void ChangeBackGroundSprite(Sprite source)
    {
        backgroundImage.sprite = source;
    }
    public void ChangeLobbyMenuImage(int backgroundimageNumber)
    {
        for(int i=0; i < panels.Length; i++) { panels[i].SetActive(false); }
        //lobbyBackgroundImage.sprite = lobbyBackGrounds[backgroundimageNumber];
        panels[backgroundimageNumber].gameObject.SetActive(true);

        if(backgroundimageNumber != 2)
        for (int i = 0; i < mapSelects.Length; i++)
        {
            mapSelects[i].targetBossInfo[0].SetActive(false);
            mapSelects[i].dispatch.sprite = mapSelects[i].originalSprite;
            mapSelects[i].OnOff();
        }
    }

    public void Dispatch(ImageHover imagehover)
    {
        if(imagehover.enabled)
        {
            //���� ���� ����
            //

            SceneFader.Instance.StartFadeOut("NikeMainRoad" , 3f);
        }
    }

    public void TutorialClear(int index)
    {
        if (index == 0)
            SaveDataManager.Instance.saveDatas[SaveDataManager.Instance.currentSaveFileIndex].isLobbyTutorialClear = true;
        else if(index == 1)
            SaveDataManager.Instance.saveDatas[SaveDataManager.Instance.currentSaveFileIndex].isStateTutorialClear = true;
        else if(index == 2)
            SaveDataManager.Instance.saveDatas[SaveDataManager.Instance.currentSaveFileIndex].isOperatorTutorialClear = true;
    }

    public void EnableSettingCanvas()
    {
        if (UIManager.Instance.optionCanvas != null)
        {
            UIManager.Instance.optionCanvas.SetActive(true);
            UIManager.Instance.OpenUI(UIManager.Instance.optionCanvas);
        }
    }

    public void SaveDataFile()
    {
        if(SaveDataManager.Instance != null)
        {
            SaveDataManager.Instance.GameSave(
                "/SaveFile" + SaveDataManager.Instance.saveFileNumber[SaveDataManager.Instance.currentSaveFileIndex] + ".txt", 
                SaveDataManager.Instance.currentSaveFileIndex);
        }
    }

    public void LoadDataFile()
    {
        if (SaveDataManager.Instance != null)
        {
            SaveDataManager.Instance.GameLoad();
        }
    }

    public void ExitGame()
    {
        TimeManager.Instance.SetTimeValue();
        SaveDataManager.Instance.GameSave(
                "/SaveFile" + SaveDataManager.Instance.saveFileNumber[SaveDataManager.Instance.currentSaveFileIndex] + ".txt",
                SaveDataManager.Instance.currentSaveFileIndex);
        Application.Quit();
    }
    #endregion
}

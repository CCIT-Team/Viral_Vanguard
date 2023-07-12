using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

// 해당 스크립트는 Virual_Vanguard 프로젝트의 Lobby Scene 에서만 동작 합니다.

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

    public Image test;
    public TextMeshProUGUI fillAmount;
    public TMP_Text percent;
    public TMP_Text time;

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
            //보스 정보 전달
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

    public void Update()
    {
        //test.fillAmount = Mathf.Clamp01(Mathf.Round(TimeManager.Instance.limitTime));

        test.fillAmount = Mathf.Clamp(Mathf.Round(TimeManager.Instance.limitTime / 60), 0, 100) / 100;

        percent.text = Mathf.Clamp(Mathf.Round(TimeManager.Instance.limitTime / 60), 0, 100).ToString() + "%";
        time.text = Mathf.Round(TimeManager.Instance.limitTime / 60).ToString() + "M";

        //Debug.Log(Mathf.Round(TimeManager.Instance.limitTime / 60));
        //Debug.Log(Mathf.Clamp(Mathf.Round(TimeManager.Instance.limitTime / 60), 0, 100));
    }
}

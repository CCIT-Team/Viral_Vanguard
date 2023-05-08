using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ButtonArrays
{
    // 저장된 데이터가 없는 경우
    public GameObject[] noSaveDataVisualizationElements; 
    // 저장된 데이터가 있는 경우
    public GameObject[] SaveDataVisualizationElements;

    public GameObject[] saveDataArrays;
}

public class MainSceneEvent : MonoBehaviour
{
    public ButtonArrays buttonArrays = new ButtonArrays();

    public void Start()
    {
        if (SavaDataManager.Instance.isSaveDataExist)
        {
            for (int i = 0; i < buttonArrays.SaveDataVisualizationElements.Length; i++)
                buttonArrays.SaveDataVisualizationElements[i].SetActive(true);
        }
        else if(!SavaDataManager.Instance.isSaveDataExist)
        {
            for (int i = 0; i < buttonArrays.noSaveDataVisualizationElements.Length; i++)
                buttonArrays.noSaveDataVisualizationElements[i].SetActive(true);
        }
    }

    public void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ButtonArrays
{
    // ����� �����Ͱ� ���� ���
    public GameObject[] noSaveDataVisualizationElements; 
    // ����� �����Ͱ� �ִ� ���
    public GameObject[] SaveDataVisualizationElements;

    public GameObject[] saveDataArrays;
}

public class MainSceneEvent : MonoBehaviour
{
    public bool isSaveDataExist = false;
    public ButtonArrays buttonArrays = new ButtonArrays();

    public void Awake()
    {
        if (isSaveDataExist)
        {
            for (int i = 0; i < buttonArrays.SaveDataVisualizationElements.Length; i++)
                buttonArrays.SaveDataVisualizationElements[i].SetActive(true);
        }
        else
        {
            for (int i = 0; i < buttonArrays.noSaveDataVisualizationElements.Length; i++)
                buttonArrays.noSaveDataVisualizationElements[i].SetActive(true);
        }
    }

    public void Update()
    {
        
    }
}

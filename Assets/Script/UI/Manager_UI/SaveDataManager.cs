using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    //Player
    public int stateHpUpCount = 0;
    public int stateSteminaUpCount = 0;

    //Weapon
    public float equipWeaponID = 0;
    public List<string> obtainWeaponList = new List<string>();
    

    //Boss
    public int bossCount = 5;
    public List<string> bossClearList = new List<string>();

    //Time
    public float playTime = 0;


    void Initialization()
    {
        stateHpUpCount = 0;
        stateSteminaUpCount = 0;
        equipWeaponID = 0;
        playTime = 0;

        obtainWeaponList.Add("Weapon1");
        obtainWeaponList.Add("Weapon2");
        obtainWeaponList.Add("Weapon3");
        obtainWeaponList.Add("Weapon4");
        obtainWeaponList.Add("Weapon5");


        bossClearList.Add("Boss1"); // ("���� �̸� , Ŭ���� ����) 
        bossClearList.Add("Boss2"); // ("���� �̸� , Ŭ���� ����) 
        bossClearList.Add("Boss3"); // ("���� �̸� , Ŭ���� ����) 
        bossClearList.Add("Boss4"); // ("���� �̸� , Ŭ���� ����) 
        bossClearList.Add("Boss5"); // ("���� �̸� , Ŭ���� ����) 
    }
}

public class SaveDataManager : Singleton<SaveDataManager>
{
    [HideInInspector] public SaveData saveData0 = new SaveData();
    [HideInInspector] public SaveData saveData1 = new SaveData();
    [HideInInspector] public SaveData saveData2 = new SaveData();
    [HideInInspector] public SaveData saveData3 = new SaveData();
    [HideInInspector] public SaveData saveData4 = new SaveData();

    [HideInInspector] public SaveData[] saveDatas = new SaveData[5];
    [HideInInspector] public SaveData saveData;

    public string _SAVE_DATA_DIRECTORY;  // ������ ���� ���
    public string _SAVE_FILENAME;
    public int saveDataCount;


    public bool isSaveDataExist = false;

    private string[] saveFileNumber = { "0","1","2", "3","4" };
    public int currentSaveFileIndex; // ���� ��� ���̺� ���Ϸ� ����?

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _SAVE_DATA_DIRECTORY = Application.dataPath + "/SaveData/";

        saveData0.stateHpUpCount = 1;
        saveData1.stateHpUpCount = 2;
        saveData2.stateHpUpCount = 3;
        saveData3.stateHpUpCount = 4;
        saveData4.stateHpUpCount = 5;

        saveDatas[0] = saveData0;
        saveDatas[1] = saveData1;
        saveDatas[2] = saveData2;
        saveDatas[3] = saveData3;
        saveDatas[4] = saveData4;

        //GameSave("/SaveFile" + saveFileNumber[0] + ".txt",0);
        //GameSave("/SaveFile" + saveFileNumber[1] + ".txt",1);
        //GameSave("/SaveFile" + saveFileNumber[2] + ".txt",2);
        //GameSave("/SaveFile" + saveFileNumber[3] + ".txt",3);
        //GameSave("/SaveFile" + saveFileNumber[4] + ".txt",4);
        GameLoadAllData();
    }

    public void GameSave(string SAVE_FILENAME, int currentsavefileindex)
    {
        this._SAVE_FILENAME = SAVE_FILENAME; 
        this.currentSaveFileIndex = currentsavefileindex;
        
        string json = JsonUtility.ToJson(saveDatas[currentsavefileindex]);
        File.WriteAllText(_SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);
        saveData = saveDatas[currentsavefileindex];
        Debug.Log(json);
    }

    public void GameLoad()
    {
        if (File.Exists(_SAVE_DATA_DIRECTORY + _SAVE_FILENAME))
        {
            string loadJson = File.ReadAllText(_SAVE_DATA_DIRECTORY + _SAVE_FILENAME);
            saveDatas[currentSaveFileIndex] = JsonUtility.FromJson<SaveData>(loadJson);
        }
    }

    public void GameLoadAllData()
    {
        for (int i = 0; i < 5; i++)
        {
            if (File.Exists(_SAVE_DATA_DIRECTORY + "/SaveFile" + saveFileNumber[i] + ".txt")) // ���� �����ϴ� ��Ȳ
            {
                //saveData = this.saveDatas[i];
                string loadJson = File.ReadAllText(_SAVE_DATA_DIRECTORY + "/SaveFile" + saveFileNumber[i] + ".txt");
                saveDatas[i] = JsonUtility.FromJson<SaveData>(loadJson);
                //
                saveDataCount = 5;
                isSaveDataExist = true;
            }
            else
            {
                Debug.Log(i + "���� �����Ͱ� �����մϴ�.");
                saveDataCount = i;
                Directory.CreateDirectory(_SAVE_DATA_DIRECTORY);// SaveData ���� ����

                if (i == 0)
                {
                    currentSaveFileIndex = 0;
                    isSaveDataExist = false;
                }
                return;
            }
        }
    }

    public void DeleteGameData(int currentsavefileindex)
    {
        File.Delete(_SAVE_DATA_DIRECTORY + "/SaveFile" + saveFileNumber[currentsavefileindex] + ".txt");
    }
}

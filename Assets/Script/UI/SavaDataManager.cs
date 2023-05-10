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


        bossClearList.Add("Boss1"); // ("보스 이름 , 클리어 여부) 
        bossClearList.Add("Boss2"); // ("보스 이름 , 클리어 여부) 
        bossClearList.Add("Boss3"); // ("보스 이름 , 클리어 여부) 
        bossClearList.Add("Boss4"); // ("보스 이름 , 클리어 여부) 
        bossClearList.Add("Boss5"); // ("보스 이름 , 클리어 여부) 
    }
}

public class SavaDataManager : Singleton<SavaDataManager>
{
    public SaveData saveData = new SaveData();

    private string _SAVE_DATA_DIRECTORY;  // 저장할 폴더 경로
    private string _SAVE_FILENAME;

    public bool isSaveDataExist = true;

    private string[] saveFileNumber = { "0","1","2", "3","4" };

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        _SAVE_DATA_DIRECTORY = Application.dataPath + "/SaveData/";

        saveData.stateHpUpCount = 20;
        GameSave("/SaveFile" + saveFileNumber[0] +".txt");
        GameLoad();
    }

    public void GameDataUpdate()
    {

    }

    public void GameSave(string SAVE_FILENAME)
    {
        this._SAVE_FILENAME = SAVE_FILENAME;
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(_SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);

        Debug.Log(json);
    }

    public void GameLoad()
    {
        //if (!Directory.Exists(SAVE_DATA_DIRECTORY))
        //{
        //    isSaveDataExist = false;
        //    Directory.CreateDirectory(SAVE_DATA_DIRECTORY);// SaveData 폴더 생성
        //}

        if(File.Exists(_SAVE_DATA_DIRECTORY + _SAVE_FILENAME)) // 파일 존재하는 상황
        {
            string loadJson = File.ReadAllText(_SAVE_DATA_DIRECTORY + _SAVE_FILENAME);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);
            Debug.Log(saveData.stateHpUpCount);
        }
        else
        {
            isSaveDataExist = false;
            Directory.CreateDirectory(_SAVE_DATA_DIRECTORY);// SaveData 폴더 생성
        }
    }
}

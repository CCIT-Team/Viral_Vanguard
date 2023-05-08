using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveDataValue
{
    //Player
    public int stateHpUpCount = 0;
    public int stateSteminaUpCount = 0;

    //Weapon
    public float equipWeaponID = 0;
    public Dictionary<string, bool> obtainWeaponList = new Dictionary<string, bool>();
    
    //Boss
    public int bossCount = 5;
    public Dictionary<string, bool> bossClearList = new Dictionary<string, bool>();

    //Time
    public float playTime = 0;


    void Initialization()
    {
        stateHpUpCount = 0;
        stateSteminaUpCount = 0;
        equipWeaponID = 0;
        playTime = 0;

        obtainWeaponList.Add("Weapon1", false);
        obtainWeaponList.Add("Weapon2", false);
        obtainWeaponList.Add("Weapon3", false);
        obtainWeaponList.Add("Weapon4", false);
        obtainWeaponList.Add("Weapon5", false);


        bossClearList.Add("Boss1", false); // ("���� �̸� , Ŭ���� ����) 
        bossClearList.Add("Boss2", false); // ("���� �̸� , Ŭ���� ����) 
        bossClearList.Add("Boss3", false); // ("���� �̸� , Ŭ���� ����) 
        bossClearList.Add("Boss4", false); // ("���� �̸� , Ŭ���� ����) 
        bossClearList.Add("Boss5", false); // ("���� �̸� , Ŭ���� ����) 
    }
}

public class SavaDataManager : Singleton<SavaDataManager>
{
    public SaveDataValue saveDataValue = new SaveDataValue();

    private string SAVE_DATA_DIRECTORY;  // ������ ���� ���
    private string SAVE_FILENAME = "/SaveFile.txt"; // ���� �̸�
    public bool isSaveDataExist = true;

    void GameSave()
    {

    }

    void GameLoad()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/SaveData/";

        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
        {
            isSaveDataExist = false;
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);// SaveData ���� ����
        }
    }
}

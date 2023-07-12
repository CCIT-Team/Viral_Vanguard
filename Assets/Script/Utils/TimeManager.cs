using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeManager : Singleton<TimeManager>
{
    public float playTime = 0;
    public float limitTime = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playTime += Time.deltaTime;

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            limitTime += Time.deltaTime;
        }
    }

    public void SetTimeValue()
    {
        SaveDataManager.Instance.saveData.playTime = this.playTime;
        SaveDataManager.Instance.saveData.limitTime = this.limitTime;
    }
}

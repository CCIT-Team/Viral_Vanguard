using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryBoss : MonoBehaviour
{
    private static TemporaryBoss instance;
    public static TemporaryBoss Instance
    {
        set { instance = value; }
        get { return instance; }
    }

    public string bossName = "юс╫ца╓";
    public float maxHp = 1200;
    public float currentHp= 1200;

    void Awake() => Instance = this;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonster : MonoBehaviour
{
    public GameObject monsterPreFab;
    [Range(0, 10)]
    public int monsterAmount = 3;
    void Awake()
    {
        for(int i = 0; i<monsterAmount;i++)
        {
            GameObject monster = Instantiate(monsterPreFab, this.transform.position + new Vector3(Mathf.Cos(Mathf.PI * 2 / monsterAmount * i), 0, Mathf.Sin(Mathf.PI * 2 / monsterAmount * i)), Quaternion.identity);
        }
    }
}

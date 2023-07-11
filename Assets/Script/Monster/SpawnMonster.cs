using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpawnMonster : MonoBehaviour
{
    public GameObject monsterPrefab;
    [Range(0, 10)]
    public int monsterAmount = 3;
    public float spawnRange = 1;
    public float patrolRange = 15;

    void Awake()
    {
        GetComponent<SphereCollider>().radius = patrolRange;
        if(monsterPrefab.TryGetComponent(out NewMonsterMovement movement))
        {
            for (int i = 0; i < monsterAmount; i++)
            {
                GameObject monster = Instantiate(monsterPrefab, this.transform.position + new Vector3(Mathf.Cos(Mathf.PI * 2 / monsterAmount * i), 0, Mathf.Sin(Mathf.PI * 2 / monsterAmount * i)) * spawnRange, Quaternion.identity);
                monster.GetComponent<NewMonsterMovement>().patrolPoint.GetComponent<MonsterPatrol>().patrolRange = patrolRange;
            }
        }
        else
        {
            for (int i = 0; i < monsterAmount; i++)
            {
                GameObject monster = Instantiate(monsterPrefab, this.transform.position + new Vector3(Mathf.Cos(Mathf.PI * 2 / monsterAmount * i), 0, Mathf.Sin(Mathf.PI * 2 / monsterAmount * i)) * spawnRange, Quaternion.identity);
                monster.GetComponentInChildren<MonsterMovement>().patrolRange = patrolRange - 2;
            }
        }
    }
}

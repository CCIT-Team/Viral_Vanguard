using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        for(int i = 0; i<monsterAmount;i++)
        {
            GameObject monster = Instantiate(monsterPrefab, this.transform.position + new Vector3(Mathf.Cos(Mathf.PI * 2 / monsterAmount * i), 0, Mathf.Sin(Mathf.PI * 2 / monsterAmount * i)) * spawnRange, Quaternion.identity);
            if(monster.TryGetComponent(out NewMonsterMovement movement))
            {
            }
            else
            {
                monster.GetComponentInChildren<MonsterMovement>().patrolRange = patrolRange - 2;
            }
        }
    }
}

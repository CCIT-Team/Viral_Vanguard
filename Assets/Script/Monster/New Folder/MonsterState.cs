using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "Scriptable Object/Monster", order = int.MaxValue)]
public class MonsterState : ScriptableObject
{
    public EMonsterType type = EMonsterType.Normal;
    public float maxHealth = 50;
    public float damage = 5;
    public float moveSpeed = 1;
}
public enum EMonsterState
{
    None = -1,
    Patrol,
    Return,
    Chase,
    Attack,
    Stiffen,
    Dead
}

public enum EMonsterType
{
    None = -1,
    Normal,
    Crawling,
    Hiding,
    Mimicking,
    Bait
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "New Stats", menuName ="Stats System/New Character Stats")]
public class StatsObject : ScriptableObject
{
    public int level;
    public int exp;

    public int Health
    {
        get; set;
    }

}

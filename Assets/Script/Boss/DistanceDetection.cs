using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDetection : MonoBehaviour
{
    [SerializeField] BossMove boss;

    public enum DistanceType { PLAYERCHECK,  NORAMLATTACK1, NORMALATTACK2, ACTIONATTACK1, ACTIONATTACK1_1, SPECIALATTACK1, SPECIALATTACK2, SPECIALATTACK2_1, SPECIALATTACK3, SPECIALATTACK3_1 }
    public DistanceType distanceType;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (distanceType == DistanceType.PLAYERCHECK)
                boss.target = other.transform;
            boss.RangeCheck(distanceType, true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (distanceType == DistanceType.PLAYERCHECK)
                return;
            boss.RangeCheck(distanceType, false);
        }
    }

    void test()
    {

    }
}
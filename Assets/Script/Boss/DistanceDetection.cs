using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDetection : MonoBehaviour
{
    [SerializeField] BossMove boss;
    bool normalRange;
    bool inductionRange;
    bool specialRange;
    bool NormalRange
    {
        set
        {
            normalRange = value;
            boss.NormalRange = NormalRange;
        }
        get { return normalRange; }
    }

    bool InductionRange
    {
        set
        {
            inductionRange = value;
            boss.NormalRange = InductionRange;
        }
        get { return inductionRange; }
    }

    bool SpecialRange
    {
        set
        {
            specialRange = value;
            boss.NormalRange = SpecialRange;
        }
        get { return specialRange; }
    }

    void CheckRange(bool b)
    {
        NormalRange = b;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            CheckRange(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckRange(false);
        }
    }
}

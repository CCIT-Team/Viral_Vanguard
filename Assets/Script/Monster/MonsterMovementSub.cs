using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovementSub : MonoBehaviour
{
    public RangeType rangeType;
    public MonsterMovement mainmove;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch(rangeType)
            {
                case RangeType.Search:
                    mainmove.SetTarget(other.transform);
                    break;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (rangeType)
            {
                case RangeType.Attack:
                    mainmove.Attack();
                    break;
                case RangeType.Search:
                    mainmove.SetTarget(other.transform);
                    break;
            }
        }
    }
}

public enum RangeType { None = -1, Attack, Search }

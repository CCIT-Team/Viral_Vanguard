using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRange : MonoBehaviour
{
    public RangeType rangeType;
    public NewMonsterMovement mainmove;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //플레이어 인식
        {
            switch (rangeType)
            {
                case RangeType.Search:
                    mainmove.SetTarget(other.transform);
                    break;
                case RangeType.Detect:
                    if(mainmove.MType == EMonsterType.Mimicking)
                    {
                        mainmove.UnMimic();
                        mainmove.SetTarget(other.transform);
                    }
                    break;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) //공격
        {
            switch (rangeType)
            {
                case RangeType.Attack:
                    if (mainmove.MType == EMonsterType.Hiding)
                    {
                        mainmove.UnMimic();
                        mainmove.searchRange.gameObject.SetActive(true);
                        mainmove.SetTarget(other.transform);
                        mainmove.Attack(2);
                        mainmove.MType = EMonsterType.Normal;
                        GetComponent<BoxCollider>().size -= new Vector3(0.5f, 0, 0.85f);
                    }
                    else
                        mainmove.Attack(); 
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (rangeType)
            {
                case RangeType.Search:
                    mainmove.State = EMonsterState.Return;
                    break;
            }
        }
    }
}
public enum RangeType { None = -1, Attack, Search, Detect }

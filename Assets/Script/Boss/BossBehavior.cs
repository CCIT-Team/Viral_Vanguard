using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public BossStatus bs;
    public List<BehaviorElem> beHaviorElemList = new List<BehaviorElem>();
    
    void BehaviorStart(int i)
    {
        beHaviorElemList[i].bossAction.Action();
    }

    int GetRandomIndex()
    {
        List<int> weights = new List<int>();

        for(int i = 0; i < beHaviorElemList.Count; i++)
        {
            weights.Add(beHaviorElemList[i].weight);
        }

        int total = 0;
        for (int i = 0; i < weights.Count; i++)
            total += weights[i];

        int pivot = Mathf.RoundToInt(total * Random.Range(0.0f, 1.0f));
        int weight = 0;

        for(int i = 0; i < weights.Count; i++)
        {
            weight += weights[i];
            if (pivot <= weight)
                return i;
        }

        return 1;
    }

    //가중치 변경
    void ChangeWeight(int index, int value)
    {
        beHaviorElemList[index].weight += value;
    }

    //지울 거
    void Update()
    {
        if (Input.GetKey(KeyCode.Z)) { print(beHaviorElemList[GetRandomIndex()].behaviorName); }
        if (Input.GetKey(KeyCode.Z)) { BehaviorStart(GetRandomIndex()); }
    }
}

[System.Serializable]
public class BehaviorElem
{
    public string behaviorName;
    public int weight;
    public RedarmsBehavior bossAction;
}

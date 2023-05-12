using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public static BossBehavior instance;
    void Awake() => instance = this;

    public BossMove boss;
    public List<BehaviorElem> behaviorElemList = new List<BehaviorElem>();
    
    public void BehaviorStart(int i)
    {
        BossMove.instacne.rangeChecks[i].WillDo = true;
    }

    public int GetRandomIndex()
    {
        List<int> weights = new List<int>();

        for(int i = 0; i < behaviorElemList.Count; i++)
        {
            weights.Add(behaviorElemList[i].weight);
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
            {
                return i;
            }
        }
        return 1;
    }

    //가중치 변경 플레이어에 넣어야하남
    void ChangeWeight(int index, int value)
    {
        behaviorElemList[index].weight += value;
    }
}

[System.Serializable]
public class BehaviorElem
{
    public string behaviorName;
    public int weight;
    public RedarmsBehavior bossAction;
}

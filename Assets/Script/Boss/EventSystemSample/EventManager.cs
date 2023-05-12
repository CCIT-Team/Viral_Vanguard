using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable IDE0018 // �ζ��� ���� ���� �˸� ����
#pragma warning disable IDE0059 // ���ʿ��� �� �Ҵ� �˸� ����

public class EventManager : MonoBehaviour
{

    public static EventManager instance = null;
    Dictionary<EVENT_TYPE, List<IListener>> Listeners = new();

    //�ν��Ͻ�ȭ
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            DestroyImmediate(gameObject);
    }

    //�ٸ� ������ ���� �� ȣ���ϴ� �ڵ�
    public void AddListener(EVENT_TYPE eventType, IListener Listener)
    {
        List<IListener> ListenList = null;

        if (Listeners.TryGetValue(eventType, out ListenList))
        {
            ListenList.Add(Listener);
            return;
        }

#pragma warning disable IDE0028
        ListenList = new List<IListener>();
        ListenList.Add(Listener);
        Listeners.Add(eventType, ListenList);
    }


    //Ư�� �̺�Ʈ �ߵ� �� ȣ���ϴ� �ڵ�
    public void PostNotification(EVENT_TYPE eventType, Component Sender, object param= null)
    {
        List<IListener> ListenList = null;

        if (!Listeners.TryGetValue(eventType, out ListenList))
        {
            return;
        }

        for (int i = 0; i < ListenList.Count; i++)
            ListenList?[i].OnEvent(eventType, Sender, param);
    }


    //������Ʈ �ı� �� ����Ʈ ������
    public void RemoveRedundancies()
    {
        Dictionary<EVENT_TYPE, List<IListener>> newListeners = new();

        foreach(KeyValuePair<EVENT_TYPE,List<IListener>> Item in Listeners)
        {
            for(int i = Item.Value.Count -1; i >= 0; i--)
            {
                if (Item.Value[i].Equals(null))
                    Item.Value.RemoveAt(i);
            }
            
            if (Item.Value.Count > 0)
                newListeners.Add(Item.Key, Item.Value);
        }

        Listeners = newListeners;
    }
}

//�̺�Ʈ �̳Ѱ�
public enum EVENT_TYPE  //
{
    eSampleEvent,
    eDead,
    eHit,
    eFindPlayer,
    eEnterBattle,
    eGuarded,
    eParried,
    eHealthChange,
    eNormalRange1,
    eNormalRange2,
    eSpecialRange1,
    eSpecialRange2,
    eSpecialRange3,
    eActTime
}
public interface IListener
{
    void OnEvent(EVENT_TYPE EventType, Component Sender, object Param = null);
}



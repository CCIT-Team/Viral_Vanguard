using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchronizeTransform : MonoBehaviour
{
    [SerializeField]
    Transform root;
    // Start is called before the first frame update
    void Start()
    {
        BoneSynchronizing(this.transform,root);
    }

    void BoneSynchronizing(Transform rag,Transform origin)
    {
        for (int i = 0; i < rag.transform.childCount; i++)
        {
            if (rag.childCount != 0)
            {
                BoneSynchronizing(rag.transform.GetChild(i),origin.transform.GetChild(i));
            }
            rag.GetChild(i).localPosition = origin.transform.GetChild(i).localPosition;
            rag.GetChild(i).localRotation = origin.transform.GetChild(i).localRotation;
        }
    }
}

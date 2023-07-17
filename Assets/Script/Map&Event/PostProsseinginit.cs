using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProsseinginit : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
        if(!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }
}

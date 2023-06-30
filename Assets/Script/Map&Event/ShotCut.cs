using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotCut : MonoBehaviour
{
    public GameObject[] shotcutObject = new GameObject[2];
    public Material fenceMaterial;
    public ShotCutTrigger[] shotCutTriggers = new ShotCutTrigger[2];
    public Animator[] animators = new Animator[2];
    public static bool shotcutOpen0 = false;
    public static bool shotcutOpen1 = false;


    void Start()
    {
        fenceMaterial = shotcutObject[0].GetComponent<MeshRenderer>().material;
        shotCutTriggers[0] = shotcutObject[0].GetComponentInChildren<ShotCutTrigger>();
        shotCutTriggers[1] = shotcutObject[1].GetComponentInChildren<ShotCutTrigger>();
        animators[0] = shotcutObject[0].GetComponent<Animator>();
        animators[1] = shotcutObject[1].GetComponent<Animator>();
    }


    void Update()
    {
        if (shotcutOpen0)
        {
            animators[0].SetTrigger("0");
            if (animators[0].GetCurrentAnimatorStateInfo(0).IsName("kiero") &&
   animators[0].GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                animators[0].gameObject.SetActive(false);
            }
        }
        if (shotcutOpen1)
        {
            fenceMaterial = shotcutObject[1].GetComponent<MeshRenderer>().material;
            animators[1].SetTrigger("0");
            if (animators[1].GetCurrentAnimatorStateInfo(0).IsName("kiero") &&
animators[1].GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                animators[1].gameObject.SetActive(false);
            }
        }
    }
}

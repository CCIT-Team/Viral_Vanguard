using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneEvent : MonoBehaviour
{
    public Animator ani;
    public Camera playerCam;
    public GameObject eventObject;

    public bool haveAnimatorParameter = false;
    public int animatorInt;
    public float animatorfloat;
    public bool animatorbool;
    public string animatorParameterName;
    public AnimatorParameter animatorParameter;
    public enum AnimatorParameter
    {
        INTTYPE,
        FLOATTYPE,
        BOOLTYPE,
        TRIGGERTYPE
    };

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            eventObject.SetActive(true);
            playerCam.gameObject.SetActive(false);
            if (haveAnimatorParameter) { SelectType(); }
            Destroy(this);
        }
    }

    void SelectType()
    {
        switch (animatorParameter)
        {
            case AnimatorParameter.INTTYPE:
                ani.SetInteger(animatorParameterName, animatorInt);
                break;
            case AnimatorParameter.FLOATTYPE:
                ani.SetFloat(animatorParameterName, animatorfloat);
                break;
            case AnimatorParameter.BOOLTYPE:
                ani.SetBool(animatorParameterName, animatorbool);
                break;
            case AnimatorParameter.TRIGGERTYPE:
                ani.SetTrigger(animatorParameterName);
                break;
        }
    }
}

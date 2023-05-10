using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 가드 행동
/// </summary>
public class GuardBehaviour : GenericBehaviour
{
    public float guardTurnSmoothing;
    private int guardBool;
    private bool guard;
    private Transform myTransform;

    private void Start()
    {
        myTransform = transform;
        guardBool = Animator.StringToHash(AnimatorKey.Guard);
    }

    void Rotating()
    {
        Vector3 forward = behaviourController.playerCamera.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;

        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootStep : MonoBehaviour
{
    public AudioClip[] stepSounds;
    public Animator myAnimator;
    private int index;
    private Transform leftFoot, rightFoot;
    private float dist;
    private int groundedBool;
    private bool grounded;

    public enum Foot
    {
        LEFT,
        RIGHT
    }

    private Foot step = Foot.RIGHT;
    private float oldDist, maxDist = 0;

    private void Awake()
    {
        leftFoot = myAnimator.GetBoneTransform(HumanBodyBones.LeftFoot);
        rightFoot = myAnimator.GetBoneTransform(HumanBodyBones.RightFoot);
        groundedBool = Animator.StringToHash(AnimatorKey.Grounded);
    }

    private void PlayFootStep()
    {
        if (oldDist < maxDist)
        {
            return;
        }

        oldDist = maxDist = 0;
        int oldindex = index;
        while (oldindex == index)
        {
            index = Random.Range(0, stepSounds.Length - 1);
        }
        
    }
}

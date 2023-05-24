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
    public float factor = 0.15f;
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
        //매니저에서 플레이 배열 받아와서 재생해주기
    }

    private void Update()
    {
        if (!grounded && myAnimator.GetBool(groundedBool))
        {
            PlayFootStep();
        }
        grounded = myAnimator.GetBool(groundedBool);
        float factor = 0.15f;

        if (grounded && myAnimator.velocity.magnitude > 1.6f) //움직이고 있다면
        {
            oldDist = maxDist;
            switch (step)
            {
                case Foot.LEFT:
                    dist = leftFoot.position.y - transform.position.y;
                    maxDist = dist > maxDist ? dist : maxDist;
                    if (dist <= factor)
                    {
                        PlayFootStep();
                        step = Foot.RIGHT;
                    }
                    break;
                case Foot.RIGHT:
                    dist = rightFoot.position.y - transform.position.y;
                    maxDist = dist > maxDist ? dist : maxDist;
                    if (dist <= factor)
                    {
                        PlayFootStep();
                        step = Foot.LEFT;
                    }
                    break;
            }
        }
    }
}

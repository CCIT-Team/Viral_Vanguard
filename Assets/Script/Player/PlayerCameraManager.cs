﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerCameraManager : MonoBehaviour
{
    [SerializeField]
    public Camera myCamera;

    public Transform player;
    public Vector3 pivotOffset = new Vector3(0.0f, 1.0f, 0.0f);
    public Vector3 camOffset = new Vector3(0.0f, 2.0f, -4.0f);
    public float smooth = 10f;
    public float horizontalAimingSpeed;
    public float verticalAimingSpeed;

    //
    public float maxVerticalAngle = 30.0f;
    public float minVerticalAngle = -60.0f;
    public float camShake = 4f;
    private float angleH = 0.0f;
    private float angleV = 0.0f;

    //
    private Transform cameraTransform;

    //
    private Vector3 relCameraPos;
    private float relCameraPosMag;

    private Vector3 smoothPivotOffset;
    private Vector3 targetPivotOffset;
    private Vector3 smoothCamOffset;
    private Vector3 targetCamOffset;

    private float defaultFOV; //기본 시야값
    private float targetFOV; //타겟 시야값
    private float targetMaxVerticleAngle; //카메라 수직 최대 각도
    private float shakeAngle = 0f; 

    public float GetH
    {
        get => angleH;
    }

    //
    public float smooth_speed = 0.125f;
    public Vector3 offset;
    bool inputMode = false;

    [SerializeField]
    public bool islockOn = false;

    public LockOn lockOn;
    public Sprite lockOnImage;
    private void Awake()
    {
        cameraTransform = transform;
        myCamera = cameraTransform.GetComponent<Camera>();

        //
        cameraTransform.position = player.position + Quaternion.identity * pivotOffset + Quaternion.identity * camOffset;
        cameraTransform.rotation = Quaternion.identity;

        //
        relCameraPos = cameraTransform.position - player.position;
        relCameraPosMag = relCameraPos.magnitude - 0.5f;

        smoothPivotOffset = pivotOffset;
        smoothCamOffset = camOffset;
        defaultFOV = myCamera.fieldOfView;
        angleH = player.eulerAngles.y;
        if (lockOn == null) { GetComponent<LockOn>(); }

        ResetTargetOffsets();
        ResetFOV();
        ResetMaxVerticalAngle();
    }

    void FixedUpdate()
    {
        if (player != null && inputMode == false)
        {
            if (!islockOn)
            {
                Vector3 desired_position = player.position + offset;
                Vector3 smoothed_position = Vector3.Lerp(transform.position, desired_position, smooth_speed);
                transform.position = smoothed_position;
            }
            else
            {
                //Vector3 dir = player.transform.localEulerAngles - transform.localEulerAngles;
                Quaternion rotation = Quaternion.LookRotation(player.forward, player.up);
                //rotation.eulerAngles = new Vector3(rotation.eulerAngles.x + 22.3f, rotation.eulerAngles.y, rotation.eulerAngles.z);
                rotation.eulerAngles = rotation.eulerAngles + new Vector3(22.3f, 0, 0);
                transform.rotation = rotation;
                Vector3 desired_position = player.position + new Vector3(offset.x, offset.y ,0) + offset.z * player.forward;
                Vector3 smoothed_position = Vector3.Lerp(transform.position, desired_position, smooth_speed);
                transform.position = smoothed_position;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (islockOn) { islockOn = false; }
            else { islockOn = true; }
            lockOn.Lock();
        }
    }

    public void ResetTargetOffsets()
    {
        targetPivotOffset = pivotOffset;
        targetCamOffset = camOffset;
    }

    public void ResetFOV()
    {
        targetFOV = defaultFOV;
    }

    public void ResetMaxVerticalAngle()
    {
        targetMaxVerticleAngle = maxVerticalAngle;
    }

    public void ShakeVertical(float degree) 
    {
        shakeAngle = degree;
    }

    public void SetTargetOffset(Vector3 newPivotOffset, Vector3 newCamOffset) 
    {
        targetPivotOffset = newPivotOffset;
        targetCamOffset = newCamOffset;
    }

    public void SetFOV(float customFOV) 
    {
        targetFOV = customFOV;
    }

    bool ViewingPosCheck(Vector3 checkPos, float playerHeight)
    {
        Vector3 target = player.position + (Vector3.up * playerHeight);
        if(Physics.SphereCast(checkPos, 0.2f, target - checkPos, out RaycastHit hit, relCameraPosMag))
        {
            if(hit.transform != player && !hit.transform.GetComponent<Collider>().isTrigger)
            {
                return false;
            }
        }
        return true;
    }

    bool ReverseViewingPosCheck(Vector3 checkPos, float playerHeight, float maxDistance)
    {
        Vector3 origin = player.position + (Vector3.up * playerHeight);
        if(Physics.SphereCast(origin, 0.2f, checkPos - origin, out RaycastHit hit, maxDistance))
        {
            if(hit.transform != player && hit.transform != transform && !hit.transform.GetComponent<Collider>().isTrigger)
            {
                return false;
            }
        }
        return true;
    }

    bool DoubleViewingCheck(Vector3 checkPos, float Offset)
    {
        float playerFocusHeight = player.GetComponent<CapsuleCollider>().height * 0.75f;
        return ViewingPosCheck(checkPos, playerFocusHeight) && ReverseViewingPosCheck(checkPos, playerFocusHeight, Offset);
    }

    public float GetCurrentPivotMagnitude(Vector3 finalPivotOffset)
    {
        return Mathf.Abs((finalPivotOffset - smoothPivotOffset).magnitude);
    }
}
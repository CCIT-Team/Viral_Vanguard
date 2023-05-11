using System.Collections;
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

    public float maxVerticalAngle = 30.0f;
    public float minVerticalAngle = -60.0f;
    public float camShakeBounce = 4f;
    private float angleHorizontal = 0.0f;
    private float angleVertical = 0.0f;

    private Transform cameraTransform;

    private Vector3 relCameraPos;
    private float relCameraPosMag;

    private Vector3 smoothPivotOffset;
    private Vector3 targetPivotOffset;
    private Vector3 smoothCamOffset;
    private Vector3 targetCamOffset;

    private float defaultFieldOfView; //기본 시야값
    private float targetFieldOfView; //타겟 시야값
    private float targetMaxVerticleAngle; //카메라 수직 최대 각도
    private float shakeAngle = 0f;

    public float _getHorizotal
    {
        get => angleHorizontal;
    }

    //
    public float smooth_speed = 0.125f;
    public Vector3 offset;
    public bool inputMode = true;

    [SerializeField]
    public bool islockOn = false;

    public LockOn lockOn;
    public Sprite lockOnImage;

    private void Awake()
    {
        cameraTransform = transform;
        myCamera = cameraTransform.GetComponent<Camera>();

        cameraTransform.position = player.position + Quaternion.identity * pivotOffset + Quaternion.identity * camOffset;
        cameraTransform.rotation = Quaternion.identity;

        relCameraPos = cameraTransform.position - player.position;
        relCameraPosMag = relCameraPos.magnitude - 0.5f;

        smoothPivotOffset = pivotOffset;
        smoothCamOffset = camOffset;
        defaultFieldOfView = myCamera.fieldOfView;
        angleHorizontal = player.eulerAngles.y;
        if (lockOn == null) { GetComponent<LockOn>(); }

        ResetTargetOffsets();
        ResetFieldOfView();
        ResetMaxVerticalAngle();
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            if (islockOn)
            {
                Quaternion rotation = Quaternion.LookRotation(player.forward, player.up);
                rotation.eulerAngles = rotation.eulerAngles + new Vector3(22.3f, 0, 0);
                transform.rotation = rotation;
                Vector3 desired_position = player.position + new Vector3(offset.x, offset.y, 0) + offset.z * player.forward;
                Vector3 smoothed_position = Vector3.Lerp(transform.position, desired_position, smooth_speed);
                transform.position = smoothed_position;
            }


        }
    }

    void Update()
    {
       if(!islockOn)
        {
            angleHorizontal += Mathf.Clamp(Input.GetAxis("Mouse X"), -1f, 1f) * horizontalAimingSpeed;
            angleVertical += Mathf.Clamp(Input.GetAxis("Mouse Y"), -1f, 1f) * verticalAimingSpeed;
            angleVertical = Mathf.Clamp(angleVertical, minVerticalAngle, targetMaxVerticleAngle);

            angleVertical = Mathf.LerpAngle(angleVertical, angleVertical + shakeAngle, 10f * Time.deltaTime);

            //카메라 회전
            Quaternion camYRotation = Quaternion.Euler(0.0f, angleHorizontal, 0.0f);
            Quaternion aimRotation = Quaternion.Euler(-angleVertical, angleHorizontal, 0.0f);
            cameraTransform.rotation = aimRotation;

            //Set fieldOfView
            myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, targetFieldOfView, Time.deltaTime);

            Vector3 baseTempPosition = player.position + camYRotation * targetPivotOffset;
            Vector3 noCollisionOffset = targetCamOffset;

            for (float zOffset = targetCamOffset.z; zOffset <= 0f; zOffset += 0.5f)
            {
                noCollisionOffset.z = zOffset;
                if (DoubleViewingCheck(baseTempPosition + aimRotation * noCollisionOffset, Mathf.Abs(zOffset)) || zOffset == 0f)
                {
                    break;
                }
            }

            smoothPivotOffset = Vector3.Lerp(smoothPivotOffset, targetPivotOffset, smooth * Time.deltaTime);
            smoothCamOffset = Vector3.Lerp(smoothCamOffset, noCollisionOffset, smooth * Time.deltaTime);

            cameraTransform.position = player.position + camYRotation * smoothPivotOffset + aimRotation * smoothCamOffset;

            if (shakeAngle > 0.0f)
            {
                shakeAngle -= camShakeBounce * Time.deltaTime;
            }
            else if (shakeAngle < 0.0f)
            {
                shakeAngle += camShakeBounce * Time.deltaTime;
            }
        }
        

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (islockOn)
            {
                islockOn = false;
            }
            else
            {
                inputMode = false;
                islockOn = true;

            }
            lockOn.Lock();
        }
    }

    public void ResetTargetOffsets()
    {
        targetPivotOffset = pivotOffset;
        targetCamOffset = camOffset;
    }

    public void ResetFieldOfView()
    {
        targetFieldOfView = defaultFieldOfView;
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

    public void SetFieldOfView(float customFieldOfView) 
    {
        targetFieldOfView = customFieldOfView;
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

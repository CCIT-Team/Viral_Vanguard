using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    #region 변수
    public Camera myCamera;
    public BehaviourController behaviourController;
    public Transform player;
    public Vector3 pivotOffset = new Vector3(0.0f, 1.0f, 0.0f);
    public Vector3 camOffset = new Vector3(0.0f, 2.0f, -4.0f);
    public float smooth = 10f;
    public float horizontalAimingSpeed;
    public float verticalAimingSpeed;

    public float maxVerticalAngle = 30.0f;
    public float minVerticalAngle = -60.0f;
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
    private float camShakeAmount;
    private float shakeTime;
    Vector3 previousTransform;

    // 락온 리스트 만들어서 가장 가까운것 확인 후 벽있는지 확인 후 그게 맞다면 회전할 수 있도록 하기 가능하면 카메라 물리 연산도 같이 해서 카메라랑 플레이어 사이에 물체가 있다면 디폴트 카메라 처럼 움직이게 하기
    public List<TartgetLockOnTransform> lockOnTargets = new List<TartgetLockOnTransform>();
    public float lockOnSphere;
    public float maximumLockOnDistance;
    public Transform currentLockOnTarget;
    public Transform nearestLockOnTarget;
    public LayerMask targetLayerMask;
    public bool isLockOn = false;
    public bool isLockOning = false;
    private float lockOnTime;
    public float minLockOnTime = 0.5f;
    public Sprite lockOnImage;
    public bool isDelay;
    #endregion

    public float _getHorizotal
    {
        get => angleHorizontal;
    }

    private void Awake()
    {
        cameraTransform = transform;
        cameraTransform.position = player.position + Quaternion.identity * pivotOffset + Quaternion.identity * camOffset;
        cameraTransform.rotation = Quaternion.identity;

        relCameraPos = cameraTransform.position - player.position;
        relCameraPosMag = relCameraPos.magnitude - 0.5f;

        smoothPivotOffset = pivotOffset;
        smoothCamOffset = camOffset;
        defaultFieldOfView = myCamera.fieldOfView;
        angleHorizontal = player.eulerAngles.y;

        ResetTargetOffsets();
        ResetFieldOfView();
        ResetMaxVerticalAngle();
    }

    void Update()
    {
        if (!isLockOn)
        {
            DefaultCamera();
        }
        else
        {
            //여기서 가까운거 리스트 정리해서 받고 커런트 트랜스톰에 넣기 + r누르면 다음으로 가까운 적으로 락온되게
            LockOnTarget();
        }

        if (Input.GetKey(KeyCode.R) && !isLockOn && !isDelay)
        {
            isLockOning = true;
            if(isLockOning)
            {
                lockOnTime += Time.deltaTime;
                if(lockOnTime >= minLockOnTime)
                {
                    LockOnActivate();
                }
            }
        }
        else if(Input.GetKey(KeyCode.R) && isLockOn && !isDelay)
        {
            isLockOning = true;
            if(isLockOning)
            {
                lockOnTime += Time.deltaTime;
                if (lockOnTime >= minLockOnTime)
                {
                    LockOnDeactivate();
                }
            }
        }
    }

    IEnumerator LockOnLocked()
    {
        yield return new WaitForSeconds(2f);
        isDelay = false;
    }

    public void DefaultCamera()
    {
        angleHorizontal += Mathf.Clamp(Input.GetAxis("Mouse X"), -1f, 1f) * horizontalAimingSpeed;
        angleVertical += Mathf.Clamp(Input.GetAxis("Mouse Y"), -1f, 1f) * verticalAimingSpeed;
        angleVertical = Mathf.Clamp(angleVertical, minVerticalAngle, targetMaxVerticleAngle);

        //angleVertical = Mathf.LerpAngle(angleVertical, angleVertical + shakeAngle, 10f * Time.deltaTime);

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

        //카메라 흔들기
        previousTransform = cameraTransform.position;
        if (shakeTime > 0f)
        {
            transform.position = Random.insideUnitSphere * camShakeAmount + previousTransform;
            shakeTime -= Time.deltaTime;
        }
        else if( shakeTime < 0f)
        {
            shakeTime = 0.0f;
            cameraTransform.position = previousTransform;
        }
    }

    public void LockOnTargetCheck()
    {
        // 락온 리스트 만들어서 가장 가까운것 확인 후 벽있는지 확인 후 그게 맞다면 회전할 수 있도록 하기 가능하면 카메라 물리 연산도 같이 해서 카메라랑 플레이어 사이에 물체가 있다면 디폴트 카메라 처럼 움직이게 하기
        float shortestTarget = Mathf.Infinity;
        //가장 먼저 적이 있는지 확인
        Collider[] colliders = Physics.OverlapSphere(player.position, lockOnSphere, targetLayerMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            TartgetLockOnTransform targets = colliders[i].GetComponent<TartgetLockOnTransform>();
            if(targets != null)
            {
                Vector3 lockTargetDirection = targets.transform.position - player.position;
                float distanceFromTarget = Vector3.Distance(player.transform.position, targets.transform.position);
                float viewableAngle = Vector3.Angle(lockTargetDirection, cameraTransform.forward);

                if(targets.transform.root != player.transform.root && viewableAngle > -50 && viewableAngle < 50 && distanceFromTarget <= maximumLockOnDistance)
                {
                    lockOnTargets.Add(targets);
                }
            }
        }
        //가장 가까운 적이 무엇인지 확인 + 벽이 막고 있는지 확인
        for (int j = 0; j < lockOnTargets.Count; j++)
        {
            float lockOnTargetDistance = Vector3.Distance(player.position, lockOnTargets[j].transform.position);
            
            if(lockOnTargetDistance < shortestTarget)
            {
                shortestTarget = lockOnTargetDistance;
                nearestLockOnTarget = lockOnTargets[j].lockOnTransform;
                currentLockOnTarget = nearestLockOnTarget;
            }
        }
    }

    public void LockOnTarget()
    {
        //가장 가까운 적쪽으로 회전
        if (currentLockOnTarget != null)
        {
            Vector3 dir = currentLockOnTarget.position - transform.position;
            dir.Normalize();
            dir.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = targetRotation;
            player.rotation = targetRotation;
            dir = currentLockOnTarget.position - transform.position;
            dir.Normalize();

            Vector3 camPosition = player.position + pivotOffset + camOffset.z * player.forward;
            Vector3 smoothed_position = Vector3.Lerp(transform.position, camPosition, smooth);
            transform.position = smoothed_position;

            //카메라 흔들기
            previousTransform = transform.position;
            if (shakeTime > 0f)
            {
                transform.position = Random.insideUnitSphere * camShakeAmount + previousTransform;
                shakeTime -= Time.deltaTime;
            }
            else if (shakeTime < 0f)
            {
                shakeTime = 0.0f;
                transform.position = previousTransform;
            }
        }
    }

    public void LockOnActivate()
    {
        isLockOn = true;
        LockOnTargetCheck();
        isDelay = true;
        behaviourController.myAnimator.SetBool(behaviourController.lockOn, true);
        StartCoroutine(LockOnLocked());
        isLockOning = false;
        lockOnTime = 0;
    }

    public void LockOnDeactivate()
    {
        isLockOn = false;
        isDelay = true;
        behaviourController.myAnimator.SetBool(behaviourController.lockOn, false);
        StartCoroutine(LockOnLocked());
        isLockOning = false;
        lockOnTime = 0;
    }

    public void ResetLockOn()
    {
        //각 트렌스폼 리셋
        //리스트 리셋
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

    public void CamShakeTime(float time, float shakeAmount)
    {
        shakeTime = time;
        camShakeAmount = shakeAmount;
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

    public float GetCurrentPivotSqrMagnitude(Vector3 finalPivotOffset)
    {
        return Mathf.Abs((finalPivotOffset - smoothPivotOffset).sqrMagnitude);
    }
}

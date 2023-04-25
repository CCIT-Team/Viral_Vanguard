using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraManager : MonoBehaviour
{
    [SerializeField]
    public Camera cam;

    public Transform player;
    public float smooth_speed = 0.125f;
    public Vector3 offset;
    bool inputMode = false;

    public int camera_shaking_num;

    [SerializeField]
    public bool islockOn = false;

    public LockOn lockOn;
    public Sprite lockOnImage;
    private void Awake()
    {
        if (lockOn == null) { GetComponent<LockOn>(); }
        if (cam == null) { cam = Camera.main; }
    }
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (player != null && inputMode == false)
        {
            if (!islockOn)
            {
                Vector3 desired_position = player.position + new Vector3(offset.x, offset.y, 0) + offset.z * player.forward;
                Vector3 smoothed_position = Vector3.Lerp(transform.position, desired_position, smooth_speed);
                transform.position = smoothed_position;
            }
            else
            {
                Quaternion rotation = Quaternion.LookRotation(player.forward, player.up);
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
        CameraShake();
    }

    void CameraShake()
    {
        switch (camera_shaking_num)
        {
            case 0:
                StopCoroutine(CameraShaking(0, 0));
                break;
            case 1:// 1,2타 공격
                StartCoroutine(CameraShaking(0.25f, 0.05f));
                break;
            case 2://3타 공격
                StartCoroutine(CameraShaking(0.25f, 0.07f));
                break;
            case 3://몬스터에게 피격 
                StartCoroutine(CameraShaking(0.23f, 0.03f));
                break;
            case 4://빅뱅
                StartCoroutine(CameraShaking(0.7f, 0.085f));
                break;
            //case 5://이벤트 스폰
            //    StartCoroutine(CameraShaking(0.00005f, 0.000003f));
            //    break;
            //case 6://이벤트 보스 출현
            //    StartCoroutine(CameraShaking(0.000035f, 0.00001f));
                //break;
        }
    }

    IEnumerator CameraShaking(float duration, float manitude)
{
    float timer = 0;

    while (timer <= duration)
    {
        Camera.main.transform.localPosition = Random.insideUnitSphere * manitude + transform.position;

        timer += Time.deltaTime;
        yield return null;

        Camera.main.transform.localPosition = transform.position;
        camera_shaking_num = 0;
    }
}
}

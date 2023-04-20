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
}

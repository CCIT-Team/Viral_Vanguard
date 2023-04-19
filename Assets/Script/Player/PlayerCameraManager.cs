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
        if(lockOn == null) { GetComponent<LockOn>(); }
        if(cam == null) { cam = Camera.main; }
    }
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (player != null && inputMode == false)
        {
            Vector3 desired_position = player.position + offset;
            Vector3 smoothed_position = Vector3.Lerp(transform.position, desired_position, smooth_speed);
            transform.position = smoothed_position;
            if(islockOn)
            {
                Vector3 dir = player.transform.position - transform.position;
                //Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime);
            }
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if (islockOn) {islockOn = false; }
            else{islockOn = true; }
            lockOn.Lock();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnIndicator : MonoBehaviour
{
    public PlayerCamera playerCamera;
    public float ThickRotationSpeed = 0.1f;
    public float SlimRotationSpeed = 0.1f;
    public float OuterRotationSpeed = 0.1f;

    private GameObject Thick;
    private GameObject Slim;
    private GameObject Outer;

    // Start is called before the first frame update
    void Start()
    {
        Thick = transform.GetChild(1).gameObject;
        Slim = transform.GetChild(2).gameObject;
        Outer = transform.GetChild(3).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerCamera.currentLockOnTarget != null)
        {
            Thick.transform.Rotate(0, 0, ThickRotationSpeed * Time.deltaTime);
            Slim.transform.Rotate(0, 0, SlimRotationSpeed * Time.deltaTime);
            Outer.transform.Rotate(0, 0, OuterRotationSpeed * Time.deltaTime);
        }
    }
}

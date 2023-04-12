using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Animator ani;

    public float speed;
    int attackNum = 0;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveMent();
        Attack();
    }

    void MoveMent()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontal * speed * Time.deltaTime, 0, vertical * speed * Time.deltaTime);
        transform.position += move;
        if (vertical > 0)
        {
            ani.SetBool("Run", true);
            ani.SetBool("Back", false);
        }
        else if (vertical < 0)
        {
            ani.SetBool("Back", true);
        }
        else
        {
            ani.SetBool("Run", false);
            ani.SetBool("Back", false);
        }
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (attackNum < 4)
            {
                attackNum += 1;
            }
            else
            {
                attackNum = 0;
            }

        }
    }
}

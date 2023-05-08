using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    public PlayerCameraManager pcm;
    GameObject lockonenemy;
    bool ischeckMonster = false;//몬스터가 감지 되었는가?

    public void FixedUpdate()
    {
        if(ischeckMonster && pcm.player != null)
        {
            Debug.Log("lock on");
            Quaternion rotation = Quaternion.LookRotation(pcm.player.forward, pcm.player.up);
            rotation.eulerAngles = rotation.eulerAngles + new Vector3(22.3f, 0, 0);
            pcm.transform.rotation = rotation;
            Vector3 desired_position = pcm.player.position + new Vector3(pcm.offset.x, pcm.offset.y, 0) + pcm.offset.z * pcm.player.forward;
            Vector3 smoothed_position = Vector3.Lerp(pcm.transform.position, desired_position, pcm.smooth_speed);
            pcm.transform.position = smoothed_position;
        }
    }
    
    public void Lock()//몬스터 락온
    {
        if (pcm.islockOn)
        {
            pcm.inputMode = false;
            Collider[] colls = Physics.OverlapSphere(transform.position, 15);
            //Debug.Log("lock on ready1" + colls[0]);
            for(int i = 0; i < 10; i++)
            {
                if(colls[i].CompareTag("Monster"))
                {
                    lockonenemy = colls[i].gameObject;
                    ischeckMonster = true;
                    OnLockOnImage();
                    break;
                }
                else { pcm.islockOn = false; }
            }

        //    if (colls[0].CompareTag("Monster"))
        //    {
        //        lockonenemy = colls[0].gameObject;
        //        //Debug.Log("lock on ready2");
        //        ischeckMonster = true;
        //        lockonenemy.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = pcm.lockOnImage;
        //        if (lockonenemy.gameObject.GetComponentInChildren<SpriteRenderer>().sprite == null)
        //        {
        //            lockonenemy.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = pcm.lockOnImage;
        //            lockonenemy = null;
        //        }
        //    }
        //    else if(!colls[0].CompareTag("Monster"))
        //    {
        //        Collider[] colls1 = Physics.OverlapSphere(transform.position, 15);
        //        lockonenemy = colls1[0].gameObject;
        //       // Debug.Log("lock on ready1.1" + colls1[0]);
        //    }
        //    else { pcm.islockOn = false;}
        }
        else
        {
            ischeckMonster = false;
            pcm.transform.localRotation = transform.localRotation;
            lockonenemy.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = null;
            lockonenemy = null;
        }
    }

    void OnLockOnImage()
    {
        lockonenemy.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = pcm.lockOnImage;
        if (lockonenemy.gameObject.GetComponentInChildren<SpriteRenderer>().sprite == null)
        {
            lockonenemy.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = pcm.lockOnImage;
            lockonenemy = null;
        }
    }
}


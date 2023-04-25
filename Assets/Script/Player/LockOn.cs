using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    public PlayerCameraManager pcm;
    GameObject lockonenemy;
    private void Update()
    {
        if (pcm.islockOn)
        {
            //pcm.player.transform.LookAt(lockonenemy.transform);
            Vector3 dir = lockonenemy.transform.position - pcm.player.transform.position;
            Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
            pcm.player.transform.rotation = rotation;
        }
    }

    public void Lock()//몬스터 락온
    {
        if (pcm.islockOn)
        {
            Collider[] colls = Physics.OverlapSphere(transform.position, 15);
            lockonenemy = colls[0].gameObject;
            if (lockonenemy.CompareTag("Monster"))
            {
                lockonenemy.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = pcm.lockOnImage;
                if (lockonenemy.gameObject.GetComponentInChildren<SpriteRenderer>().sprite == null)
                {
                    lockonenemy.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = pcm.lockOnImage;
                    lockonenemy = null;
                }
            }
        }
        else
        {

            lockonenemy.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = null;
            lockonenemy = null;
        }
    }
}


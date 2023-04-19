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
            pcm.player.transform.LookAt(lockonenemy.transform);
        }
    }

    public void Lock()//몬스터 락온
    {
        if (pcm.islockOn)
        {
            Collider[] colls = Physics.OverlapSphere(transform.position, 15);

            lockonenemy = colls[0].gameObject;
            if (colls[0].CompareTag("Monster"))
            {
                pcm.player.transform.LookAt(colls[0].transform);
                //pcm.cam.transform.LookAt(pcm.player.transform)

                //pcm.offset.z = colls[0].transform.position.z;
                colls[0].gameObject.GetComponentInChildren<SpriteRenderer>().sprite = pcm.lockOnImage;
                if (colls[0].gameObject.GetComponentInChildren<SpriteRenderer>().sprite == null)
                {
                    colls[0].gameObject.GetComponentInChildren<SpriteRenderer>().sprite = pcm.lockOnImage;
                    colls[0] = null;
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


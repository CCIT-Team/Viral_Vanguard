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

    public void Lock()//���� ����
    {
        if (pcm.islockOn)
        {
            Collider[] colls = Physics.OverlapSphere(transform.position, 15);
            lockonenemy = colls[0].gameObject;
            if (colls[0].CompareTag("Monster"))
            {
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


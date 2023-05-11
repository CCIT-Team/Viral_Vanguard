using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    public PlayerCameraManager playerCameraManager;
    GameObject lockonenemy;

    [HideInInspector]
    public bool ischeckMonster = false;//���Ͱ� ���� �Ǿ��°�?
    
    public void Lock()//���� ����
    {
        if (playerCameraManager.islockOn)
        {
            Collider[] colls = Physics.OverlapSphere(transform.position, 15);
            for(int i = 0; i < 10; i++)
            {
                if(colls[i].CompareTag("Monster"))
                {
                    lockonenemy = colls[i].gameObject;
                    ischeckMonster = true;
                    OnLockOnImage();
                    break;
                }
            }
        }
        else
        {
            ischeckMonster = false;
            playerCameraManager.transform.localRotation = transform.localRotation;
            lockonenemy.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = null;
            lockonenemy = null;
        }
    }

    void OnLockOnImage()
    {
        lockonenemy.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = playerCameraManager.lockOnImage;
        if (lockonenemy.gameObject.GetComponentInChildren<SpriteRenderer>().sprite == null)
        {
            lockonenemy.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = playerCameraManager.lockOnImage;
            lockonenemy = null;
        }
    }
}


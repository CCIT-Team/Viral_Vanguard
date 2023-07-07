using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchronizeTransform : MonoBehaviour
{
    [SerializeField]
    Transform root;

    [SerializeField]
    SkinnedMeshRenderer[] bodyMesh;

    [SerializeField]
    Material dissolveMaterial;
    Material mat;

    private void OnEnable()
    {
        mat = Instantiate(dissolveMaterial);
        foreach (SkinnedMeshRenderer mesh in bodyMesh)
        {
            mesh.material = mat;
        }
        BoneSynchronizing(this.transform, root);
        StartCoroutine(Dissolve(4));
    }

    void BoneSynchronizing(Transform rag,Transform origin)
    {
        for (int i = 0; i < rag.transform.childCount; i++)
        {
            if (rag.childCount != 0)
            {
                BoneSynchronizing(rag.transform.GetChild(i),origin.transform.GetChild(i));
            }
            rag.GetChild(i).localPosition = origin.transform.GetChild(i).localPosition;
            rag.GetChild(i).localRotation = origin.transform.GetChild(i).localRotation;
        }
    }

    //-----------------------------------------------

    IEnumerator Dissolve(float time = 0.01f)
    {
        yield return new WaitForSeconds(time);
        float i = 0;
        while (i <= 2)
        {
            yield return new WaitForSecondsRealtime(0.05f);
            i += 0.05f;
            mat.SetFloat("_Float", i);
        }
        transform.root.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour, IPointerClickHandler
{
    public GameObject sectionButton;
    public MeshRenderer meshRenderer;
    public Image selectCursorImage;
    public Sprite selectCursorClickSprite;
    public Sprite selectCursorOnSprite;

    public MapSelect[] mapSelects;

    // 2023-05-24 Jun
    public GameObject[] targetBossInfo;
    public ImageHover imageHover;
    public Image dispatch;
    public Sprite originalSprite;
    public Sprite changeSprite;

    bool isSelected;

    void Start()
    {
        imageHover.enabled = false;
    }

    public void CursorOnTheObjectroof()
    {
        meshRenderer.materials[0].SetColor("_EmissiveColor", new Color(0, 0, 38, 50));
        meshRenderer.materials[1].SetColor("_EmissiveColor", new Color(0, 0, 75, 100));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isSelected)
        {
            selectCursorImage.sprite = selectCursorClickSprite; // 클릭 이미지로 변경
            targetBossInfo[1].SetActive(false);
            targetBossInfo[2].SetActive(false);
            targetBossInfo[0].SetActive(true);
            CursorOnTheObjectroof();
            OnOff();
            dispatch.sprite = changeSprite;
            imageHover.enabled = true;
            isSelected = true;
        }
        else if(isSelected)
        {
            selectCursorImage.sprite = selectCursorOnSprite; // Non클릭 이미지로 변경
            targetBossInfo[0].SetActive(false);
            meshRenderer.materials[0].SetColor("_EmissiveColor", new Color(18, 100, 65, 50));
            meshRenderer.materials[1].SetColor("_EmissiveColor", new Color(30, 10, 75, 100));
            dispatch.sprite = originalSprite;
            imageHover.enabled = false;

            isSelected = false;
        }
    }

    public void OnOff()
    {
        foreach (MapSelect mapSelect in mapSelects)
        {
            //mapSelect.selectCursorImage.enabled = false;
            mapSelect.selectCursorImage.sprite = mapSelect.selectCursorOnSprite;
            mapSelect.meshRenderer.materials[0].SetColor("_EmissiveColor", new Color(18, 100, 65, 50));
            mapSelect.meshRenderer.materials[1].SetColor("_EmissiveColor", new Color(30, 10, 75, 100));
        }
    }
}

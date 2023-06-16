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
        Color wallColor = new Color(0.65f, 0.1939f, 0, 0.5019f);
        Color roofColor = new Color(0.75f, 0.375f, 0, 1f);
        meshRenderer.materials[0].SetColor("_EmissiveColor", wallColor);
        meshRenderer.materials[1].SetColor("_EmissiveColor", roofColor);
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

            Color wallColor = new Color(0.38f, 0.38f, 0.38f, 0.5019f);
            Color roofColor = new Color(0.7490f, 0.7490f, 0.7490f, 1f);
            meshRenderer.materials[0].SetColor("_EmissiveColor", wallColor);
            meshRenderer.materials[1].SetColor("_EmissiveColor", roofColor);

            dispatch.sprite = originalSprite;
            imageHover.enabled = false;

            isSelected = false;
        }
    }

    public void OnOff()
    {
        Color wallColor = new Color(0.38f, 0.38f, 0.38f, 0.5019f);
        Color roofColor = new Color(0.7490f, 0.7490f, 0.7490f, 1f);
        foreach (MapSelect mapSelect in mapSelects)
        {
            //mapSelect.selectCursorImage.enabled = false;
            mapSelect.selectCursorImage.sprite = mapSelect.selectCursorOnSprite;
            mapSelect.meshRenderer.materials[0].SetColor("_EmissiveColor", wallColor);
            mapSelect.meshRenderer.materials[1].SetColor("_EmissiveColor", roofColor);
        }
    }
}

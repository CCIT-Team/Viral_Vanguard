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

    public GameObject targetBossInfo;

    bool isSelected;

    public void CursorOnTheObjectroof()
    {
        meshRenderer.materials[0].SetColor("_EmissiveColor", new Color(0, 0, 38, 50));
        meshRenderer.materials[1].SetColor("_EmissiveColor", new Color(0, 0, 75, 100));
    }

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    selectCursorImage.enabled = true; // 이미지 표시
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    if (isSelected == false)
    //        selectCursorImage.enabled = false; // 이미지 숨김
    //}

    public void OnPointerClick(PointerEventData eventData)
    {
        selectCursorImage.sprite = selectCursorClickSprite; // 클릭 이미지로 변경
        isSelected = true;
        targetBossInfo.SetActive(true);
        CursorOnTheObjectroof();
        OnOff();
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
        targetBossInfo.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.Experimental.GraphView;

public class MouseEventHandler : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler
{
    public Sprite originalImage;
    public Sprite changedImage;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 마우스 올림
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 마우스 내림
    }
}

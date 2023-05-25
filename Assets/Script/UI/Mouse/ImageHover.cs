using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;

    public Sprite originalSprite;
    public Sprite hoverSprite;

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.sprite = hoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = originalSprite;
    }

    public void EnabledImageHover()
    {
        if(this.enabled)
            this.enabled = false;
        else if(!this.enabled)
            this.enabled = true;
    }
}

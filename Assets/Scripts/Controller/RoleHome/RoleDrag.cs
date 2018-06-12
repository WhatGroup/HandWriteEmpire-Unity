using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoleDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform canvas;

    public Image portrait;

    private Image liHui;

    public Sprite sprite;
    

    private Vector2 offset = new Vector3();


    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 mouseDown = eventData.position;
        Vector2 mouseUGUIPos = new Vector2();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mouseDown, eventData.enterEventCamera,
                out mouseUGUIPos);
        liHui = Instantiate(portrait);
        liHui.sprite = sprite;
        liHui.SetNativeSize();
        liHui.rectTransform.SetParent(canvas);
        liHui.rectTransform.anchoredPosition = mouseUGUIPos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mouseDrag = eventData.position;
        Vector2 uguiPos = new Vector2();
        bool isRect =
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mouseDrag, eventData.enterEventCamera,
                out uguiPos);
        if (isRect)
        {
            liHui.rectTransform.anchoredPosition = offset + uguiPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(liHui.gameObject);
    }
}
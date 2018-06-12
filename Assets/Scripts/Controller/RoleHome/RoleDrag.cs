using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoleDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public PositionController posOne;
    public PositionController posTwo;
    public PositionController posThree;

    public RectTransform canvas;

    public string roleType;

    public Image portrait;

    private Image liHui;

    public Sprite sprite;


    private Vector2 offset = new Vector3();


    public void OnBeginDrag(PointerEventData eventData)
    {
        var mouseDown = eventData.position;
        var mouseUGUIPos = new Vector2();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mouseDown, eventData.enterEventCamera,
            out mouseUGUIPos);
        liHui = Instantiate(portrait);
        liHui.sprite = sprite;
        liHui.SetNativeSize();
        liHui.rectTransform.SetParent(canvas);
        liHui.rectTransform.anchoredPosition = mouseUGUIPos;
        liHui.raycastTarget = false;
        RoleHomeUIManager._instance.currentDragRole = roleType;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var mouseDrag = eventData.position;
        var uguiPos = new Vector2();
        var isRect =
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mouseDrag, eventData.enterEventCamera,
                out uguiPos);
        if (isRect) liHui.rectTransform.anchoredPosition = offset + uguiPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(liHui.gameObject);
        if (posOne.isOnPos) posOne.ShowRole();

        if (posTwo.isOnPos) posTwo.ShowRole();

        if (posThree.isOnPos) posThree.ShowRole();
    }
}
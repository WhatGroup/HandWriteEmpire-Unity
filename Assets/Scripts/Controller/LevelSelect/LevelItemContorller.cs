using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelItemContorller : MonoBehaviour
{
    private ItemStateController itemController;
    private FlagStateController flagController;
    private ClickHandler clickHandler;

    private void Awake()
    {
        itemController = GetComponent<ItemStateController>();
        flagController = GetComponent<FlagStateController>();
        clickHandler = GetComponent<ClickHandler>();
    }

    public void SetItemContentLeft()
    {
        SetPanelLocalPositionX(itemController.CurrenLevel.transform, -180);
        SetPanelLocalPositionX(itemController.OkLevel.transform, -180);
        SetPanelLocalPositionX(itemController.LockLevel.transform, -180);
    }


    //设置图标在左
    public void SetItemContentRight()
    {
        SetPanelLocalPositionX(itemController.CurrenLevel.transform, 180);
        SetPanelLocalPositionX(itemController.OkLevel.transform, 180);
        SetPanelLocalPositionX(itemController.LockLevel.transform, 180);
    }

    private void SetPanelLocalPositionX(Transform transform, int positionX)
    {
        var temp = transform.localPosition;
        temp.x = positionX;
        transform.localPosition = temp;
    }

    public void SetActiveState(LevelInfo info)
    {
        var state = info.state;
        var level = info.level;
        switch (state)
        {
            case LevelState.CURRENT:
                itemController.CurrenLevel.SetActive(true);
                clickHandler.AddOnClickCurrenLevelListener(level);
                break;
            case LevelState.OK:
                itemController.OkLevel.SetActive(true);
                flagController.SetFlagIcon(info.flag);
                clickHandler.AddOnClickOkLevelListener(level);
                break;
            case LevelState.LOCK:
                itemController.LockLevel.SetActive(true);
                clickHandler.AddOnClickLockLevelListener(level);
                break;
        }
    }
}
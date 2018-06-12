using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;
using UnityEngine.EventSystems;

public class PositionController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject attackRole;
    public GameObject defenseRole;
    public GameObject cureRole;
    public bool isOnPos = false;
    
    public void OnPointerExit(PointerEventData eventData)
    {
        isOnPos = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOnPos = true;
    }

    public void ShowRole()
    {
        if (!GeneralUtils.IsStringEmpty(RoleHomeUIManager._instance.currentDragRole))
        {
            switch (RoleHomeUIManager._instance.currentDragRole)
            {
                case RoleInfo.ATTACK:
                    attackRole.SetActive(true);
                    cureRole.SetActive(false);
                    defenseRole.SetActive(false);
                    break;
                case RoleInfo.CURE:
                    attackRole.SetActive(false);
                    cureRole.SetActive(true);
                    defenseRole.SetActive(false);
                    break;
                case RoleInfo.DEFENSE:
                    attackRole.SetActive(false);
                    cureRole.SetActive(false);
                    defenseRole.SetActive(true);
                    break;
            }

            RoleHomeUIManager._instance.currentDragRole = "";
        }
    }
}
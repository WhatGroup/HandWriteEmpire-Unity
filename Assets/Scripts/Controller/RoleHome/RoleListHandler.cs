using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleListHandler : MonoBehaviour
{
    public RoleItemController roleItem1;

    public RoleItemController roleItem2;

    public RoleItemController roleItem3;

    // Use this for initialization
    void Start()
    {
        roleItem1.ID = 1;
        roleItem2.ID = 2;
        roleItem3.ID = 3;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
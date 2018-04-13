using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackListener : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            BackHandler._instance.PopScene();
        }
    }
}
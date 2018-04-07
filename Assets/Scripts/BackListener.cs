using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackListener : MonoBehaviour
{
    void Update()
    {
        BackHandler._instance.PopScene();
    }
}
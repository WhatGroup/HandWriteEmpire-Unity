using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingUIController : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2f);
        BackHandler._instance.GoToAdventure();
    }
}
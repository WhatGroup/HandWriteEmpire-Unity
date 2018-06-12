using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUIController : MonoBehaviour
{
    public Text loadingScaleText;

    public Slider loadingBar;

    public int speed;


    private void Update()
    {
        loadingBar.value += Time.deltaTime * speed;
        loadingScaleText.text = (int) loadingBar.value + "%";
        if (loadingBar.value >= 100) BackHandler._instance.GoToAdventure();
    }
}
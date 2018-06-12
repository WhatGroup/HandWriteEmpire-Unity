using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        GetComponent<AudioSource>().volume = PrefsManager.Volume;
    }
}
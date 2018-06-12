using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindWordUIManager : MonoBehaviour
{
    public GameObject afterFind;

    public void OnClickFindBtn()
    {
        afterFind.SetActive(true);
    }

    public void OnClickContinueBtn()
    {
        afterFind.SetActive(false);
    }
}
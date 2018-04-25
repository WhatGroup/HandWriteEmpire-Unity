using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerHandler : MonoBehaviour {

    public void OnClickResourceBtn()
    {
        BackHandler._instance.AddScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("02_Resource");
    }
    public void OnClickMeBtn()
    {
        BackHandler._instance.AddScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("03_Me");
    }
    public void OnClickSOLOBtn()
    {
        BackHandler._instance.AddScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("04_SOLO");
    }
    public void OnClickDoubleBtn()
    {
        BackHandler._instance.AddScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("05_Double");
    }
    public void OnClickTrainBtn()
    {
        BackHandler._instance.AddScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("06_Train");
    }

    public void OnClickRoleManagerBtn()
    {
        BackHandler._instance.AddScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("07_RoleManager");
    }
}

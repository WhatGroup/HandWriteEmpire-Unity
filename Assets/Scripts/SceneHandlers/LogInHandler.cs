using UnityEngine;
using UnityEngine.SceneManagement;

public class LogInHandler : MonoBehaviour
{
    public void OnClickLogInBtn()
    {
        BackHandler._instance.AddScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("01_Manager");
    }
}
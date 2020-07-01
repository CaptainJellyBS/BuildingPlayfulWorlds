using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public int firstScene;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1.0f;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(firstScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

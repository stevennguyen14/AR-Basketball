using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;

    public void OpenMenu()
    {
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
    }

    public void CloseMenu()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
    }
}

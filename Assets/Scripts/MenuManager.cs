using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{


    public void playAdditionGame()
    {
        GameManager.instance.gameMode = 1;
        SceneManager.LoadScene("GameScene");
    }

    public void playSubtractionGame()
    {
        GameManager.instance.gameMode = 2;
        SceneManager.LoadScene("GameScene");
    }

    public void playMulipcationGame()
    {
        GameManager.instance.gameMode = 3;
        SceneManager.LoadScene("GameScene");
    }

    public void playMixedGame()
    {
        GameManager.instance.gameMode = 4;
        SceneManager.LoadScene("GameScene");
    }

}

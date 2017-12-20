using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{



    public Image _MenuPause;


    public void OnPlayButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnQuitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnPauseButton()
    {
        _MenuPause.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnContinueButton()
    {
        _MenuPause.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnPlayAgainButton()
    {
        SceneManager.LoadScene("Game");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void Option()
    {
        Debug.Log("Option");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        //Set screen size for Standalone

        Screen.SetResolution(564, 960, false);

    }
    public void Play()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}

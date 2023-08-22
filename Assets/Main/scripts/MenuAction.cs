using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuAction : MonoBehaviour
{
    public GameObject music_on;
    public GameObject music_off;
    public bool isMusicOn = true;
    public bool isPaused = false;

    private void Start()
    {
        if (Application.loadedLevelName == "Menu")
        {
            PlayerPrefs.SetInt("Keys", 0);
        }
    }

    void Update()
    {
        if (isMusicOn && music_on != null)
        {
            music_on.SetActive(true);
            music_off.SetActive(false);
            AudioListener.volume = 1;
        }
        if (!isMusicOn && music_off != null)
        {
            music_on.SetActive(false);
            music_off.SetActive(true);
            AudioListener.volume = 0;
        }
    }

    public void goToMainMenu()
    {
        // Load the Menu scene
        Application.LoadLevel("Menu");
        PauseManager pauseManager = FindObjectOfType<PauseManager>();
        if (pauseManager != null)
        {
            pauseManager.isPaused = false;
        }
    }

    public void goToGameWin()
    {
        // Load the GameWin scene
        Application.LoadLevel("GameWin");
    }

    public void revive()
    {
        // Load the MainLevel scene
        Application.LoadLevel("MainLevel");

        // Get the PauseManager instance and set isPaused to false to ensure the game is not paused when starting/restarting the level
        PauseManager pauseManager = FindObjectOfType<PauseManager>();
        if (pauseManager != null)
        {
            AudioListener.volume = 1;
            pauseManager.isPaused = false;
             AudioListener.pause = false;
        }
    }

    public void goToGame()
    {
        // Load the MainLevel scene
        Application.LoadLevel("BackStory");

        // Get the PauseManager instance and set isPaused to false to ensure the game is not paused when starting/restarting the level
        PauseManager pauseManager = FindObjectOfType<PauseManager>();
        if (pauseManager != null)
        {
            AudioListener.volume = 1;
            pauseManager.isPaused = false;
            AudioListener.pause = false;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                GameManager.GMInstance.UpdateGameState(GameState.GamePause);
                Pause();   
            }
        }
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        GameManager.GMInstance.RestorePrevieousGameState();
    }

    public void Quit()
    {
        //Resume();
        Debug.Log("Quitting game...");
    }

    public void Menu()
    {
        Resume();
        GameManager.GMInstance.UpdateGameState(GameState.GameStart);
    }

    public void Options()
    {
        Resume();
        GameManager.GMInstance.UpdateGameState(GameState.GameOptions);
    }
}

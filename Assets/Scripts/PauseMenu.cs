using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI, BackgroundImg;
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
        BackgroundImg.SetActive(true);
        SetImageAlpha(BackgroundImg.GetComponent<Image>(), .5f);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        SetImageAlpha(BackgroundImg.GetComponent<Image>(), 1f);
        BackgroundImg.SetActive(false);
        GameManager.GMInstance.RestorePrevieousGameState();
    }
    private void SetImageAlpha(Image img, float alpha)
    {
        var tmpColor = img.color;
        tmpColor.a = alpha;
        img.color = tmpColor;
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
        PauseMenuUI.SetActive(false);
        GameManager.GMInstance.UpdateGameState(GameState.GameOptions);
    }
}

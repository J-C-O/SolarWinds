using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameManager.GMInstance.UpdateGameState(GameState.RegisterPlayer);
    }

    public void ShowOptions()
    {
        GameManager.GMInstance.UpdateGameState(GameState.GameOptions);
    }
    public void QuitGame()
    {
        Debug.Log("Quit pressed");
        Application.Quit();
    }

    public void StartDemoRiddle()
    {
        SceneManager.LoadScene("Riddle 1");
    }
}

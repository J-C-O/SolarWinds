using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class RegisterPlayer : MonoBehaviour
{

    public TMP_Text PlayerList;
    public TMP_InputField NameInput;
    private string inputValue { get { return NameInput.text; } }
    public void AddListPlayer()
    {
        if ((inputValue != null) || inputValue != "")
        {
            PlayerManager.PMInstance.AddPlayer(inputValue);
            NameInput.text = "";
        }
        else
        {
            Debug.Log("can't add empty or null");
        }
    }

    


    public void PlayGame()
    {
        GameManager.GMInstance.UpdateGameState(GameState.PlayerTurn);
    }
    public void Back()
    {
        GameManager.GMInstance.RestorePrevieousGameState();
    }
}

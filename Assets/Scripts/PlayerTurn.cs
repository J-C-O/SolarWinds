using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurn : MonoBehaviour
{
    public TMP_Text PlayerNamePanel, PlayerPointsPanel;

    void Update()
    {
        if(PlayerManager.PMInstance.activePlayer != null)
        {
            PlayerNamePanel.text = PlayerManager.PMInstance.activePlayer.Name;
            PlayerPointsPanel.text = ConfigurationManager.CMInstance.TextPointsOfPlayer + PlayerManager.PMInstance.activePlayer.Points.ToString();

            PlayerNamePanel.transform.parent.GetComponent<Image>().color = PlayerManager.PMInstance.activePlayer.PlayerColor;
            PlayerNamePanel.transform.parent.GetComponent<Image>().sprite = null;

            PlayerPointsPanel.transform.parent.GetComponent<Image>().color = PlayerManager.PMInstance.activePlayer.PlayerColor;
            PlayerPointsPanel.transform.parent.GetComponent<Image>().sprite = null;
        }
    }

    public void ShowOptions()
    {
        GameManager.GMInstance.UpdateGameState(GameState.GamePause);
    }
}

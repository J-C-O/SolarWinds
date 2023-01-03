using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
    public TMP_Text PlayerNamePanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerManager.PMInstance.activePlayer != null)
        {
            PlayerNamePanel.text = PlayerManager.PMInstance.activePlayer.Name;
        }
        
    }

    public void ShowOptions()
    {
        GameManager.GMInstance.UpdateGameState(GameState.GamePause);
    }
}

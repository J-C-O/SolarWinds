using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListController : MonoBehaviour
{
    
    public Player player;
    public Button RemovePlayerButton;
    
    public void SetRemovePlayerButton()
    {
        RemovePlayerButton.onClick.AddListener(RemovePlayer);
        Debug.Log(gameObject.name + ": RemovePlayerFunction was set.");
    }
    public void RemovePlayer()
    {
        if(player == null)
        {
            Debug.Log("player not set:" + gameObject.name);
        }
        PlayerManager.PMInstance.RemovePlayer(player);
        Destroy(gameObject);
        
    }
    public void SetPlayer(Player newPlayer)
    {
        player = newPlayer;
        Debug.Log("player set:" + gameObject.name + "=" + player.Name);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager PMInstance;
    public List<Player> Players = new List<Player>();
    public int maxPlayers = 4;
    public Button addPlayerButton;
    public Player activePlayer;
    #region UI Player listing
    public Transform PlayerContent;
    public GameObject ListPlayer;
    
    //erzeugt grafische Auflistung der Player
    public void ListPlayers()
    {
        CleanPlayerContent();
        GameObject playerObject;

        for (int i = 0; i < Players.Count; i++)
        {
            playerObject = Instantiate(ListPlayer, PlayerContent);
            playerObject.name = "Player_" + i.ToString();
            playerObject.transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString() + ". " + Players[i].Name;
            playerObject.transform.GetChild(0).GetComponent<Text>().enabled = true;
            playerObject.transform.GetChild(1).GetComponent<Button>().enabled = true;
            playerObject.transform.GetChild(1).GetComponent<Image>().enabled = true;

            playerObject.transform.GetComponent<PlayerListController>().SetPlayer(Players[i]);
            playerObject.gameObject.SetActive(true);
        }

    }

    //entfernt grafische Auflistung der Player
    public void CleanPlayerContent()
    {
        Transform template = PlayerContent.GetChild(0);
        ListPlayer = template.gameObject;
        foreach (Transform player in PlayerContent)
        {
            Destroy(player.gameObject);
        }
        Destroy(template.gameObject);
    }
    #endregion


    private void Awake()
    {
        PMInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerContent.transform.GetChild(0).gameObject.SetActive(false);
    }

    #region player registration methods
    public void AddPlayer(string name)
    {
        int uniqueID = Players.Count;

        if(Players.Count < maxPlayers)
        {
            Player player = ScriptableObject.Instantiate(ItemManager.IMInstance.player);
            player.PlayerID = uniqueID;
            player.Name = name;
            player.AddInventory(ItemManager.IMInstance.GetRandomItem());
            Players.Add(player);
            ListPlayers();
        }
        else
        {
            Debug.Log("maximum number of players reached");
        }
    }
    public void RemovePlayer(Player player)
    {
        Destroy(PlayerContent.GetChild(player.PlayerID - 1).gameObject);
        Players.Remove(player);
        ListPlayers();
    }
    #endregion

    public void SetActivePlayer(Player player)
    {
        activePlayer.SetIsActive(false);
        if(player != null)
        {
            activePlayer = player.GetCopy();
        }
        else
        {
            Debug.Log(string.Format("{0}.{1} player is null", this.GetType().Name , System.Reflection.MethodBase.GetCurrentMethod()));
        }
        activePlayer.SetIsActive(true);
    }
    public void SetActivePlayer(int index)
    {
        int playerCount = Players.Count;
        if(playerCount == 0)
        {
            Debug.Log("No Players in list");
        }
        else
        {
            if (activePlayer != null)
            {
                activePlayer.SetIsActive(false);
            }

            if (index >= 0 && index < playerCount)
            {
                activePlayer = Players[index].GetCopy();
            }
            else
            {
                Debug.Log(string.Format("{0}.{1} index is out of bounds", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod()));
            }
            activePlayer.SetIsActive(true);
        }
    }
    public void ClearActivePlayer()
    {
        activePlayer = null;
    }
    public Player GetActivePlayer()
    {
        return activePlayer;
    }

    public void ExecuteTurn()
    {
        if( activePlayer != null)
        {
            //ziehe neue Karte
            activePlayer.AddInventory(ItemManager.IMInstance.GetRandomItem());
            //warte auf eingabe und führe diese aus
        }
    }
}

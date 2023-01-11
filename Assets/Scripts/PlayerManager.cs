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
    public GameObject GetPlayerListEntryTemplate()
    {
        GameObject template = new GameObject();
        template.name = "PlayerListEntryTemplate";
        template.AddComponent<RectTransform>();
        template.AddComponent<PlayerListController>();
        template.AddComponent<Button>();

        GameObject playerName = new GameObject();
        playerName.name = "PlayerName";
        playerName.AddComponent<Text>();
        playerName.GetComponent<Text>().font = ConfigurationManager.CMInstance.ListFont;
        playerName.GetComponent<Text>().fontSize = 18;
        playerName.GetComponent<Text>().supportRichText = true;
        playerName.GetComponent<Text>().lineSpacing = 1;

        GameObject removePlayerButton = new GameObject();
        removePlayerButton.name = "RemovePlayerButton";
        removePlayerButton.AddComponent<Image>();
        removePlayerButton.GetComponent<Image>().sprite = ConfigurationManager.CMInstance.Sprite_ButtonClose;
        removePlayerButton.AddComponent<Button>();

        template.GetComponent<PlayerListController>().RemovePlayerButton = removePlayerButton.GetComponent<Button>();

        playerName.transform.SetParent(template.transform);
        removePlayerButton.transform.SetParent(template.transform);
        template.transform.SetParent(PlayerContent);

        template.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        template.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        template.GetComponent<RectTransform>().pivot = new Vector2((float)0.5, (float)0.5);
        template.GetComponent<RectTransform>().sizeDelta = new Vector2(ConfigurationManager.CMInstance.PlayerListEntry_Width, ConfigurationManager.CMInstance.PlayerListEntry_Height);

        playerName.GetComponent<RectTransform>().anchorMin = new Vector2(.5f, .5f);
        playerName.GetComponent<RectTransform>().anchorMax = new Vector2(.5f, .5f);
        playerName.GetComponent<RectTransform>().pivot = new Vector2(.5f, .5f);
        playerName.GetComponent<RectTransform>().sizeDelta = new Vector2(177.61f, 26.9464f);
        playerName.GetComponent<RectTransform>().anchoredPosition = new Vector3(38.805f, 29.866f);

        removePlayerButton.GetComponent<RectTransform>().anchorMin = new Vector2(.5f, .5f);
        removePlayerButton.GetComponent<RectTransform>().anchorMax = new Vector2(.5f, .5f);
        removePlayerButton.GetComponent<RectTransform>().pivot = new Vector2(.5f, .5f);
        removePlayerButton.GetComponent<RectTransform>().sizeDelta = new Vector2(32.786f, 32.7858f);
        removePlayerButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(144f, 29.866f);

        return template;
    }
    //erzeugt grafische Auflistung der Player
    public void ListPlayers()
    {
        GameObject template = GetPlayerListEntryTemplate();
        CleanPlayerContent();
        
        if (Players.Count == 0) return;
        for (int i = 0; i < Players.Count; i++)
        {
            GameObject playerObject = Instantiate(template, PlayerContent);
            playerObject.name = "Player_" + i.ToString();
            playerObject.transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString() + ". " + Players[i].Name;
            playerObject.transform.GetChild(0).GetComponent<Text>().enabled = true;
            playerObject.transform.GetChild(1).GetComponent<Button>().enabled = true;
            playerObject.transform.GetChild(1).GetComponent<Image>().enabled = true;

            playerObject.transform.GetComponent<PlayerListController>().SetPlayer(Players[i]);
            playerObject.transform.GetComponent<PlayerListController>().SetRemovePlayerButton();
            playerObject.gameObject.SetActive(true);
        }
        Destroy(template);
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
        Color playerColor;
        if (uniqueID < ConfigurationManager.CMInstance.PlayerColors.Length)
        {
            playerColor = ConfigurationManager.CMInstance.PlayerColors[uniqueID];
        }
        else
        {
            playerColor = GetRandomColor();
        }
        
        if(Players.Count < maxPlayers)
        {
            Player player = ScriptableObject.Instantiate(ItemManager.IMInstance.player);
            player.PlayerID = uniqueID;
            player.Name = name;
            player.PlayerColor = playerColor;
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
        //Destroy(PlayerContent.GetChild(player.PlayerID - 1).gameObject);
        Destroy(PlayerContent.GetChild(player.PlayerID).gameObject);
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

    public void InitPlayers()
    {
        Players = new List<Player>();
        ListPlayers();
    }
    private Color GetRandomColor()
    {
        Color color = new Color(
            UnityEngine.Random.Range(0f, 1f), //Red
            UnityEngine.Random.Range(0f, 1f), //Green
            UnityEngine.Random.Range(0f, 1f), //Blue
            1f //Alpha
            );
        return color;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager PMInstance;
    public List<Player> Players = new List<Player>();
    private int maxPlayers = 4;

    private void Awake()
    {
        PMInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPlayer(string name)
    {
        int uniqueID = Players.Count;

        if(Players.Count < maxPlayers)
        {
            Players.Add(new Player(name, uniqueID));
        }
        else
        {
            Debug.Log("maximum number of players reached");
        }
    }
    public void RemovePlayer(Player player)
    {  
        if (Players.Contains(player))
        {
            Players.Remove(player);
        }
        else
        {
            Debug.Log("player does not exist");
        }
    }
}

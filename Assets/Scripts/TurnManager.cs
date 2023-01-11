using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager TMInstance;
    public static int PlayerIndex = 0;
    public int TurnsPerRound {
        get {
            return PlayerManager.PMInstance.Players.Count;
        }
    }
    private Item randomItem;
    public void Awake()
    {
        TMInstance = this;

    }

    //depracted, todo delete after tests
    //public void StartTurnBasedRound()
    //{
    //    bool flag = false;
    //    while (!flag)
    //    {
    //        for(int index = 0; index < TurnsPerRound; index++)
    //        {
    //            PlayerManager.PMInstance.SetActivePlayer(index);
    //            randomItem = ItemManager.IMInstance.GetRandomItem();
    //            GameManager.GMInstance.UpdateGameState(GameState.PlayerTurnRandomCard);

    //        }
    //        flag = VictoryConditionMet();
    //    }
    //}
    public void InitRound()
    {
        PlayerIndex = 0;
    }
    public void StartRound()
    {
        PlayerManager.PMInstance.SetActivePlayer(PlayerIndex);
        Debug.Log("Active player set to: " + PlayerManager.PMInstance.GetActivePlayer().Name);
        if (VictoryConditionMet())
        {
            GameManager.GMInstance.UpdateGameState(GameState.GameEnd);
        }
        else
        {
            StartTurn();
        }
    }
    public void StartTurn()
    {
        randomItem = ItemManager.IMInstance.GetRandomItem();      
        GameManager.GMInstance.UpdateGameState(GameState.PlayerTurnRandomCard);
    }
    public void NextPlayer()
    {
        if(PlayerIndex + 1 < TurnsPerRound)
        {
            PlayerIndex++;
        }
        else
        {
            PlayerIndex = 0;
        }
        StartRound();
    }
    public Item GetRandomItem()
    {
        return randomItem;
    }
    private bool VictoryConditionMet()
    {
        List<Player> resultPlayers = new List<Player>(PlayerManager.PMInstance.Players.FindAll(isVictoryPlayer));
        return resultPlayers.Count > 0;
        //return true;
    }

    private static bool isVictoryPlayer(Player p)
    {
        return p.Points >= GameManager.GMInstance.NeededVictoryPoints;
    }
}


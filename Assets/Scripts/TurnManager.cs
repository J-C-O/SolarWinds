using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager TMInstance;
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

    public void StartTurnBasedRound()
    {
        bool flag = false;
        while (!flag)
        {
            for(int index = 0; index < TurnsPerRound; index++)
            {
                PlayerManager.PMInstance.SetActivePlayer(index);
                randomItem = ItemManager.IMInstance.GetRandomItem();
                GameManager.GMInstance.UpdateGameState(GameState.PlayerTurnRandomCard);

            }
            flag = VictoryConditionMet();
        }
    }
    public Item GetRandomItem()
    {
        return randomItem;
    }
    private bool VictoryConditionMet()
    {
        List<Player> resultPlayers = new List<Player>(PlayerManager.PMInstance.Players.FindAll(isVictoryPlayer));
        //return resultPlayers.Count > 0;
        return true;
    }

    private static bool isVictoryPlayer(Player p)
    {
        return p.Points >= GameManager.GMInstance.NeededVictoryPoints;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRound : MonoBehaviour
{
    public int TurnsPerRound = PlayerManager.PMInstance.Players.Count;
    private Player currentPlayer;
    private bool IsGameVictory()
    {
        return true;
    }
    public void StartRound()
    {
        while (!IsGameVictory())
        {
            for(int x = 0; x < TurnsPerRound; x++)
            {

            }
        }
    }

    private void ExecuteTurn(int playerID)
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

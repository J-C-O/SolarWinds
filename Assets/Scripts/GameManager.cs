using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager GMInstance;
    public GameState State;

    public static event Action<GameState> OnGameStateChanged;
    public void Awake()
    {
        GMInstance = this;
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.GameStart:
                HandleStartGame();
                break;
            case GameState.RegisterPlayer:
                HandleRegisterPlayer();
                break;
            case GameState.PlayerTurn:
                HandlePlayerTurn();
                break;
            case GameState.GamePause:
                HandleGamePause();
                break;
            case GameState.GameEnd:
                HandleGameEnd();
                break;
            default:
                Debug.LogWarning("Unknown GameState");
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    //this method contains the logic for the GameStart-State
    private void HandleStartGame()
    {
        throw new NotImplementedException();
    }

    //this method contains the logic for the RegisterPlayer-State
    private void HandleRegisterPlayer()
    {
        throw new NotImplementedException();
    }

    //this method contains the logic for the PlayerTurn-State
    private void HandlePlayerTurn()
    {
        throw new NotImplementedException();
    }

    //this method contains the logic for the GamePause-State
    private void HandleGamePause()
    {
        throw new NotImplementedException();
    }

    //this method contains the logic for the GameEnd-State
    private void HandleGameEnd()
    {
        throw new NotImplementedException();
    }


    





    //Gamestates for papermodel implementation
    public enum GameState
    {
        //at the start the player should see the main menu
        GameStart,
        //then the main player should be able to register fellow players
        RegisterPlayer,
        //each player has n turns in a game
        PlayerTurn,
        //pause menu
        GamePause,
        //at the end the player should see the main menu... again
        GameEnd
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GMInstance;
    public int NeededVictoryPoints = 100;
    public int MaxPlayers = 4;
    public int MaxItemsPerPlayerInventory = 4;
    public GameState State;
    private GameState oldState;
    private GameState nextState;

    public static event Action<GameState> OnGameStateChanged;
    public void Awake()
    {
        GMInstance = this;
    }

    private void Start()
    {
        oldState = GameState.GameStart;
        nextState = GameState.UndefinedState;
        UpdateGameState(GameState.GameStart);
    }

    public void UpdateGameState(GameState newState)
    {
        oldState = State;
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
            case GameState.GameOptions:
                HandleGameOptions();
                break;
            case GameState.UndefinedState:
                break;
            case GameState.PlayerTurnRandomCard:
                HandlePlayerTurnRandomCard();
                break;
            case GameState.PlayerTurnPlayerAction:

                break;
            case GameState.InventoryUpdate:
                Debug.Log(State.ToString());
                break;
            default:
                Debug.LogWarning("Unknown GameState: " + newState.ToString());
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void HandlePlayerTurnRandomCard()
    {
        Debug.Log(State.ToString());

        
    }

    private void HandleGameOptions()
    {
        Debug.Log(State.ToString());
    }

    public void RestorePrevieousGameState()
    {
        nextState = State;
        UpdateGameState(oldState);
    }

    public GameState GetLastGameState()
    {
        return oldState;
    }
    //this method contains the logic for the GameStart-State
    private void HandleStartGame()
    {
        Debug.Log(State.ToString());
    }

    //this method contains the logic for the RegisterPlayer-State
    private void HandleRegisterPlayer()
    {
        Debug.Log(State.ToString());
    }

    //this method contains the logic for the PlayerTurn-State
    private void HandlePlayerTurn()
    {
        Debug.Log(State.ToString());
        TurnManager.TMInstance.StartRound();
    }

    //this method contains the logic for the GamePause-State
    private void HandleGamePause()
    {
        Debug.Log(State.ToString());
    }

    //this method contains the logic for the GameEnd-State
    private void HandleGameEnd()
    {
        Debug.Log(State.ToString());
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GMInstance;
    public int NeededVictoryPoints = 100;
    public int MaxPlayers = 4;
    public int MaxItemsPerPlayerInventory = 4;
    public GameState State;
    private GameState oldState;
    private GameState lastIngameState;
    private GameState nextState;
    private GameObject fieldCenter;
    [SerializeField]
    private GameObject gameBoard;
    [SerializeField]
    private GameObject cameraTarget;

    public GameObject FieldCenter { 
        get => fieldCenter;
        set {
            fieldCenter = value;
            UpdateCamTarget(FieldCenter);
        } 
    }


    private GameState[] NoInGameStates = {
        GameState.GameOptions,
        GameState.GamePause,
        GameState.GameStart,
        GameState.GameEnd,
        GameState.RegisterPlayer
    };
    private void UpdateCamTarget(GameObject fieldCenter)
    {
        if(fieldCenter != null && cameraTarget != null)
        {
            cameraTarget.transform.position = fieldCenter.transform.position;
        }
    }

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


    #region Manipulate GameState Methods
    public void UpdateGameState(GameState newState)
    {
        if (NoInGameStates.Contains(newState) && !NoInGameStates.Contains(State))
        {
            lastIngameState = State;
        }
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
                HandleInventoryUpdate();
                break;
            default:
                Debug.LogWarning("Unknown GameState: " + newState.ToString());
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }
    
    public GameState GetLastGameState()
    {
        return oldState;
    }
    
    public void RestorePrevieousGameState()
    {
        nextState = State;
        if(oldState == GameState.GameOptions)
        {
            UpdateGameState(lastIngameState);
        }
        else
        {
            UpdateGameState(oldState);
        }
        
    }
    #endregion

    #region HandleGameState Methods
    //this method contains the logic for the HandleInventoryUpdate-State
    private void HandleInventoryUpdate()
    {
        Debug.Log(State.ToString());
        if (!gameBoard.activeSelf) gameBoard.SetActive(true);
        UpdateGameState(GameState.PlayerTurnPlayerAction);
    }

    //this method contains the logic for the PlayerTurnRandomCard-State
    private void HandlePlayerTurnRandomCard()
    {
        Debug.Log(State.ToString());
    }

    //this method contains the logic for the GameOptions-State
    private void HandleGameOptions()
    {
        Debug.Log(State.ToString());
    }
    
    //this method contains the logic for the GameStart-State
    private void HandleStartGame()
    {
        Debug.Log(State.ToString());
        //initalize/reset Game
        PlayerManager.PMInstance.InitPlayers();
        TurnManager.TMInstance.InitRound();
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
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class MenuManager : MonoBehaviour
{
    [field: SerializeField]
    public GameObject 
                MainMenuPanel, 
                OptionMenuPanel, 
                BackGroundImg, 
                RegisterPlayerPanel, 
                PauseMenuPanel, 
                PlayerTurnPanel, 
                GameEndPanel, 
                DrawRandomItemPanel, 
                GameBoard,
                PlayerInventoryPanel;
    private GameObject currentView;

    public void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.GameStart:
                BackGroundImg.SetActive(true);
                if (currentView == null)
                {
                    currentView = MainMenuPanel;
                    MainMenuPanel.SetActive(true);
                }
                else
                {
                    SwitchView(currentView, MainMenuPanel);
                }
                break;
            case GameState.RegisterPlayer:
                BackGroundImg.SetActive(true);
                SwitchView(currentView, RegisterPlayerPanel);
                break;
            case GameState.PlayerTurn:
                BackGroundImg.SetActive(false);
                SwitchView(currentView, PlayerTurnPanel);
                GameBoard.SetActive(true);
                GameManager.GMInstance.UpdateGameState(GameState.PlayerTurnRandomCard);
                break;
            case GameState.GamePause:
                GameState[] InGameStates = { GameState.PlayerTurn, GameState.PlayerTurnRandomCard };

                BackGroundImg.SetActive(true);
                if (InGameStates.Contains(GameManager.GMInstance.GetLastGameState()))
                {
                    BackGroundImg.SetActive(false);
                }
                SwitchView(currentView, PauseMenuPanel);
                break;
            case GameState.GameEnd:
                SwitchView(currentView, GameEndPanel);
                break;
            case GameState.GameOptions:
                SwitchView(currentView, OptionMenuPanel);
                break;
            case GameState.PlayerTurnRandomCard:
                //BackGroundImg.SetActive(false);
                PlayerInventoryPanel.SetActive(false);
                DrawRandomItemPanel.SetActive(true);
                //SwitchView(currentView, DrawRandomItemPanel);
                break;
            case GameState.UndefinedState:
                break;
            case GameState.PlayerTurnPlayerAction:
                //BackGroundImg.SetActive(false);
                SwitchView(DrawRandomItemPanel, PlayerInventoryPanel);
                break;
            case GameState.InventoryUpdate:

                break;
            default:
                break;
        }
    }

    private void SwitchView(GameObject previewsView, GameObject nextView)
    {
        currentView = nextView;
        previewsView.SetActive(false);
        nextView.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        //currentView = MainMenuPanel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

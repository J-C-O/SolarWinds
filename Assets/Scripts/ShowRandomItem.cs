using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowRandomItem : MonoBehaviour
{
    public Image Icon;
    public TMPro.TextMeshProUGUI ItemName, TurnOfText;
    public Button TossAwayButton, PickUpButton;
    private void Update()
    {
        SetUpDialog();
    }
    public void SetUpDialog()
    {
        TurnOfText.text = ConfigurationManager.CMInstance.TextTurnOfPlayer + PlayerManager.PMInstance.GetActivePlayer().Name;
        if (TurnManager.TMInstance.GetRandomItem() != null)
        {
            Icon.sprite = TurnManager.TMInstance.GetRandomItem().ItemIcon;
            ItemName.text = TurnManager.TMInstance.GetRandomItem().ItemName;
        }
        TossAwayButton.gameObject.SetActive(true);
        PickUpButton.gameObject.SetActive(true);
        if (PlayerManager.PMInstance.GetActivePlayer().inventory.Count == 0)
        {
            TossAwayButton.gameObject.SetActive(false);
        }
        if (PlayerManager.PMInstance.GetActivePlayer().inventory.Count == ConfigurationManager.CMInstance.MaxPlayerInventoryItems)
        {
            PickUpButton.gameObject.SetActive(false);
        }
    }
    public void TossAway()
    {
        gameObject.SetActive(false);
        GameManager.GMInstance.UpdateGameState(GameState.InventoryUpdate);
        GameManager.GMInstance.UpdateGameState(GameState.PlayerTurnPlayerAction);
    }

    public void PickRandomUp()
    {
        Item random = TurnManager.TMInstance.GetRandomItem();
        Player player = PlayerManager.PMInstance.GetActivePlayer();
        if (random != null && player != null)
        {
            player.AddInventory(TurnManager.TMInstance.GetRandomItem());
            PlayerManager.PMInstance.SetActivePlayer(player);
            gameObject.SetActive(false);
            GameManager.GMInstance.UpdateGameState(GameState.PlayerTurnPlayerAction);
        }
    }
}

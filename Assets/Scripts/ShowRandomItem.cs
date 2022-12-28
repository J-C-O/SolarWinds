using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowRandomItem : MonoBehaviour
{
    public Image Icon;
    public TMPro.TextMeshProUGUI ItemName;

    public void SetUpDialog()
    {
        if (TurnManager.TMInstance.GetRandomItem() != null)
        {
            Icon.sprite = TurnManager.TMInstance.GetRandomItem().ItemIcon;
            ItemName.text = TurnManager.TMInstance.GetRandomItem().ItemName;
        }
    }
    public void TossAway()
    {
        gameObject.SetActive(false);
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

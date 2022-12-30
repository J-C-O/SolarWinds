using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryItemController : InventoryItemController
{
    protected Player _player;
    public int _inventoryIndex;
    public Button SwapButton;
    // Start is called before the first frame update
    public override void RemoveItem()
    {
        _player.RemoveItem(item);
        Destroy(gameObject);
    }
    public void SetSwapButton()
    {
        SwapButton.onClick.AddListener(SwapWithFirstItem);
        Debug.Log(gameObject.name + ": SwapButtonFunction was set.");
    }
    public void RemoveThis()
    {
        Destroy(this);
    }
    public void SetPlayer(Player player)
    {
        item.GetOwned(player);
        _player = player;
        
    }
    public void SetInventoryIndex(int index)
    {
        _inventoryIndex = index;
        Debug.Log(gameObject.name + ": Index was set to " + index.ToString());
    }
    private void SwapItem(int oldindex, int newIndex)
    {
        Item tmp = PlayerManager.PMInstance.activePlayer.inventory[newIndex];

        PlayerManager.PMInstance.activePlayer.inventory[newIndex] = PlayerManager.PMInstance.activePlayer.inventory[oldindex];
        PlayerManager.PMInstance.activePlayer.inventory[oldindex] = tmp;

        GameManager.GMInstance.UpdateGameState(GameState.InventoryUpdate);
    }
    public void SwapWithFirstItem()
    {
        SetInventoryIndex(System.Int16.Parse(gameObject.name));
        SwapItem(_inventoryIndex, 0);

        //GameManager.GMInstance.UpdateGameState(GameState.InventoryUpdate);
    }
}

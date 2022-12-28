using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryItemController : InventoryItemController
{
    protected Player _player;
    private int _inventoryIndex;
    // Start is called before the first frame update
    public override void RemoveItem()
    {
        _player.RemoveItem(item);
        Destroy(gameObject);
    }

    public void SetPlayer(Player player)
    {
        item.GetOwned(player);
        _player = player;
    }
    public void SetInventoryIndex(int index)
    {
        _inventoryIndex = index;
    }
    private void SwapItem(int oldindex, int newIndex)
    {
        Item tmp = _player.inventory[newIndex];
        _player.inventory[newIndex] = _player.inventory[oldindex];
        _player.inventory[oldindex] = tmp;
    }
    public void SwapWithFirstItem()
    {
        if(_inventoryIndex > 0)
        {
            SwapItem(_inventoryIndex, 0);
        }
    }
}

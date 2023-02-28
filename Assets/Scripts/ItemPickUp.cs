using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item Item;
    public Item requiredForDestroy;
    public bool skipOwnerCheck;

    void Pickup()
    {
        if(!IsOwned()) {
            return;
        }

        if (PlayerManager.PMInstance != null && PlayerInventory.PIInstance != null) {
            var pmInstance = PlayerManager.PMInstance;
            if (pmInstance.activePlayer == null) {
                return;
            }
            var activeItem = PlayerInventory.PIInstance.GetActiveItem();
            if (activeItem != null && activeItem != requiredForDestroy) {
                return;
            }
            pmInstance.activePlayer.inventory.Remove(activeItem);
            Destroy(gameObject);
            GameManager.GMInstance.UpdateGameState(GameState.InventoryUpdate);
            return;
        }

        var inventory = InventoryManager.Instance;
        if (requiredForDestroy == null) {
            inventory.Add(Item);
            Destroy(gameObject);
            return;
        }
        if (inventory.Selected != requiredForDestroy)
        {
            return;
        }
        inventory.Remove(inventory.Selected);
        inventory.Selected = null;
        Destroy(gameObject);
    }

    private bool IsOwned() {
        if (this.skipOwnerCheck) {
            return true;
        }
        var ownable = GetComponent<Ownable>();
        if (ownable == null) {
            return false;
        }
        if (PlayerManager.PMInstance == null) {
            // riddle mode
            return ownable.owner == 0;
        }
        return ownable.owner == PlayerManager.PMInstance.activePlayer.PlayerID;
    }

    private void OnMouseDown()
    {
        Pickup();
    }
}

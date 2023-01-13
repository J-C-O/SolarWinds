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
        return ownable.owner == PlayerManager.PMInstance.activePlayer.PlayerID;
    }

    private void OnMouseDown()
    {
        Pickup();
    }
}

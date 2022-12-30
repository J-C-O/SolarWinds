using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item Item;

    void Pickup()
    {
        var ownable = GetComponent<Ownable>();
        if (ownable == null) {
            return;
        }
        // TODO: current active player
        if (ownable.owner != 0) {
            return;
        }
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        Pickup();
    }
}

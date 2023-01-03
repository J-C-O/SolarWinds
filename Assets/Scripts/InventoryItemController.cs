using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    protected Item item;
    public Button RemoveButton;
    public virtual void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);

        Destroy(gameObject);
    }
    public virtual void AddItem(Item newItem)
    {
        item = newItem;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{

    public Item item;
    public Button RemoveButton;

    public void Start() {
        var button = GetComponent<Button>();
        button.onClick.AddListener(() => InventoryManager.Instance.Selected = item);
    }

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);
        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }
}

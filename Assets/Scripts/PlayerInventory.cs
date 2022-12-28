using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public Image IconActiveItem;
    public Transform ItemStore;
    public GameObject ItemSlotTemplate;

    public PlayerInventoryItemController[] InventoryItems;
    public void Update()
    {
        if(GetActiveItem().ItemName != null)
        {
            IconActiveItem.sprite = GetActiveItem().ItemIcon;
            ListStoredItems();
        }
        else
        {
            Debug.Log("No active Item");
        }
        
    }

    public void ListStoredItems()
    {
        CleanItemStore();
        for (int i = 1; i < GetActivePlayer().inventory.Count; i++)
        {
            GameObject obj = Instantiate(ItemSlotTemplate, ItemStore);
            
            obj.transform.Find("Icon").GetComponent<Image>().sprite = GetActivePlayer().inventory[i].ItemIcon;
        }
        SetInventoryItems();
        ItemSlotTemplate.SetActive(false);
    }

    private void CleanItemStore()
    {
        foreach(Transform t in ItemStore)
        {
            Destroy(t.gameObject);
        }
    }

    public void SetInventoryItems()
    {
        InventoryItems = new PlayerInventoryItemController[GetActivePlayer().inventory.Count];

        //todo inventory.count ist 4 
        for (int i = 0; i < GetActivePlayer().inventory.Count; i++)
        {
            InventoryItems[i] = ItemSlotTemplate.GetComponentInChildren<PlayerInventoryItemController>();
            if(GetActivePlayer().inventory[i] != null)
            {
                InventoryItems[i].AddItem(GetActivePlayer().inventory[i]);
                InventoryItems[i].SetPlayer(GetActivePlayer());
                InventoryItems[i].SetInventoryIndex(i);
            }
            
        }
    }

    private Player GetActivePlayer()
    {
        if (PlayerManager.PMInstance.activePlayer == null)
        {
            PlayerManager.PMInstance.SetActivePlayer(0);
        }
        return PlayerManager.PMInstance.activePlayer;
    }

    private Item GetActiveItem()
    {
        if (PlayerManager.PMInstance.activePlayer.inventory[0] == null)
        {
            return null;
        }
        return GetActivePlayer().inventory[0];
        
    }


    public void SwitchActiveItem(int inventoryIndex)
    {
        Item lastactive = GetActiveItem();
        GetActivePlayer().inventory[0] = GetActivePlayer().inventory[inventoryIndex];
        GetActivePlayer().inventory[inventoryIndex] = lastactive;
    }
}

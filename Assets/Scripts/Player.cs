using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Item/Create New Player")]
public class Player : ScriptableObject
{
    //Properties
    public string Name;
    public int PlayerID;
    public double Points = 0;
    private bool isActive;
    public List<Item> inventory = new List<Item>();

    public Color PlayerColor;

    public Player GetCopy()
    {
        return this;
    }
    internal void RemoveItem(Item item)
    {
        inventory.Remove(item);
        GameManager.GMInstance.UpdateGameState(GameState.InventoryUpdate);
    }

    public void SetPlayerID(int id)
    {
        PlayerID = id;
    }
    public Player(string name, int id)
    {
        Name = name;
        PlayerID = id;
        SetIsActive(false);
    }
    public bool GetIsActive()
    {
        return isActive;
    }

    public void AddInventory(Item item)
    {
        if(inventory == null)
        {
            Debug.Log("Inventory is null");
        }
        else
        {
            if(inventory.Count < GameManager.GMInstance.MaxItemsPerPlayerInventory)
            {
                inventory.Add(item);
            }
            else
            {
                Debug.Log("Inventory full");
            }
            
        }
        GameManager.GMInstance.UpdateGameState(GameState.InventoryUpdate);
    }

    public void SetIsActive(bool value)
    {
        isActive = value;
    }

    public void SumPoints(double summand)
    {
        Points += summand;
    }

    public void InitInventory()
    {
        inventory = new List<Item>();
    }
}

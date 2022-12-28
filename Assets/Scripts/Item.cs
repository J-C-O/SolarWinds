using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="Item/Create New Item")]
public class Item : ScriptableObject
{
    public  PowerType InputPower;
    public  PaperModelItem ItemType;

    public int Id => (int)ItemType;
    public string ItemName => ItemType.ToString();
     
    public int Value;
    [SerializeField]
    protected Sprite itemIcon;
    public Sprite ItemIcon { get => itemIcon; set => itemIcon = value; }

    
    public bool IsActive;
    protected Player owner;

    public void GetOwned(Player player)
    {
        if(player != null)
        {
            Disown();
            owner = player; 
            player.AddInventory(this);
        }
        else
        {
            Debug.Log(string.Format("{0}.{1} player is null", owner.name, System.Reflection.MethodBase.GetCurrentMethod()));
        }
    }

    private void Disown()
    {
        if(owner != null)
        {
            owner = null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Properties
    public string Name;
    public int PlayerID;
    public int Points = 0;
    private bool isActive;

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

    public void SetIsActive(bool value)
    {
        isActive = value;
    }
}

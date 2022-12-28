using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager IMInstance;

    public Item[] items;
    public Player player;

    private void Awake()
    {
        IMInstance = this;
    }

    public Item GetRandomItem()
    {
        int random = Random.Range(0, items.Length - 1);

        return items[random];
    }
}

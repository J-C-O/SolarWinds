using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager IMInstance;

    public Item[] items;
    public int[] counts;
    public Player player;
    private List<Item> deck;

    private void Awake()
    {
        deck = new List<Item>();
        IMInstance = this;
        for (int i = 0; i < items.Length; i++) {
            for (int j = 0; j < counts[i]; j++) {
                deck.Add(items[i]);
            }
        }
        // shuffle
        deck = deck.OrderBy(i => Random.Range(0, int.MaxValue)).ToList();
    }

    public Item GetRandomItem()
    {
        int random = Random.Range(0, deck.Count - 1);
        var selected = deck[random];
        deck.RemoveAt(random);
        return selected;
    }
}

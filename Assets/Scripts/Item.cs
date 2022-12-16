using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="Item/Create New Item")]
public class Item : ScriptableObject
{
    public int Id;
    public string ItemName;
    [SerializeField]
    private Sprite itemIcon;
    public int Value;

    public Sprite ItemIcon { get => itemIcon; set => itemIcon = value; }

    public PowerType PowerType;
    public bool IsActive;
    public GameObject prefab;
}

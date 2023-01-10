using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConfigurationManager : MonoBehaviour
{
    public static ConfigurationManager CMInstance;

    public void Awake()
    {
        CMInstance = this;
    }


    public Sprite 
                Sprite_ButtonLarge,
                Sprite_ButtonClose,
                Sprite_BackGroundMini,
                Sprite_ItemIconPlaceholder,
                Sprite_ItemIconMirror,
                Sprite_ItemIconSolarConsumer,
                Sprite_ItemIconWindConsumer;

    public float
                PlayerInventorySlot_Width = 60,
                PlayerInventorySlot_Height = 60,
                PlayerInventorySlotIcon_Width = 122,
                PlayerInventorySlotIcon_Height = 122;

    public int
                MaxPlayerInventoryItems = 4,
                GameBoard_Width = 11,
                GameBoard_Height = 11;
    public string
                TextTurnOfPlayer = "Turn of ";

    public Color
                DefaultTileColorBase,
                DefaultTileColorOffset,
                DeactiveTileColorBase,
                DeactiveTileColorOffset;
                
}

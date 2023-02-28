using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory PIInstance;
    public Image IconActiveItem;
    public Transform ItemStore;
    public Item SelectedItem;
    //public GameObject ItemSlotTemplate;
    public GameObject ItemSlotTemplate
    {
        get {
            GameObject template = new GameObject();
            template.name = "InventorySlotTemplate";
            template.AddComponent<PlayerInventoryItemController>();
            template.AddComponent<Button>();
            template.GetComponent<PlayerInventoryItemController>().SwapButton = template.GetComponent<Button>();
            template.AddComponent<Image>();
            template.GetComponent<Image>().sprite = ConfigurationManager.CMInstance.Sprite_BackGroundMini;
            GameObject icon = new GameObject();
            icon.name = "Icon";
            icon.AddComponent<Image>();
            icon.transform.SetParent(template.transform);
            template.transform.SetParent(ItemStore);

            template.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
            template.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
            template.GetComponent<RectTransform>().pivot = new Vector2((float)0.5, (float)0.5);
            template.GetComponent<RectTransform>().sizeDelta = new Vector2(ConfigurationManager.CMInstance.PlayerInventorySlot_Width, ConfigurationManager.CMInstance.PlayerInventorySlot_Height);
            icon.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
            icon.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
            icon.transform.localPosition = Vector3.zero;
            icon.GetComponent<RectTransform>().sizeDelta = new Vector2(ConfigurationManager.CMInstance.PlayerInventorySlotIcon_Width, ConfigurationManager.CMInstance.PlayerInventorySlotIcon_Height);

            icon.GetComponent<RectTransform>().localScale = new Vector3((float)0.31227, (float)0.31227, 1);
            return template;
        }
    }
    public PlayerInventoryItemController[] InventoryItems;

    public void Awake()
    {
        PIInstance = this;
    }
    private GameObject GetInventorySlotTemplate()
    {
        GameObject template = new GameObject();
        template.name = "InventorySlotTemplate";
        template.AddComponent<PlayerInventoryItemController>();
        template.AddComponent<Button>();
        template.GetComponent<PlayerInventoryItemController>().SwapButton = template.GetComponent<Button>();
        template.AddComponent<Image>();
        template.GetComponent<Image>().sprite = ConfigurationManager.CMInstance.Sprite_BackGroundMini;
        GameObject icon = new GameObject();
        icon.name = "Icon";
        icon.AddComponent<Image>();
        icon.transform.SetParent(template.transform);
        //template.transform.SetParent(ItemStore);

        template.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        template.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        template.GetComponent<RectTransform>().pivot = new Vector2((float)0.5, (float)0.5);
        template.GetComponent<RectTransform>().sizeDelta = new Vector2(ConfigurationManager.CMInstance.PlayerInventorySlot_Width, ConfigurationManager.CMInstance.PlayerInventorySlot_Height);
        icon.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        icon.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        icon.transform.localPosition = Vector3.zero;
        icon.GetComponent<RectTransform>().sizeDelta = new Vector2(ConfigurationManager.CMInstance.PlayerInventorySlotIcon_Width, ConfigurationManager.CMInstance.PlayerInventorySlotIcon_Height);

        icon.GetComponent<RectTransform>().localScale = new Vector3((float)0.31227, (float)0.31227, 1);
        return template;
    }
    public void Start()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        GameManager.GMInstance.UpdateGameState(GameState.InventoryUpdate);

    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (state == GameState.InventoryUpdate)
        {
            if (GetActiveItem() != null)
            {
                IconActiveItem.sprite = GetActiveItem().ItemIcon;
                //nicht entfernen, Inventar muss doppelt bereinigt werden, damit es funktioniert
                CleanItemStore();
                ListStoredItems(); 
            }
            else
            {
                Debug.Log("No active Item");
                IconActiveItem.sprite = null;
                //if(GetActivePlayer().inventory.Count > 0)
                //{
                //    GetActivePlayer().inventory[0] = GetActivePlayer().inventory[1];
                //    GetActivePlayer().RemoveItem(GetActivePlayer().inventory[1]);
                //}
            }
        }
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    public void ListStoredItems()
    {
        GameObject template = GetInventorySlotTemplate();
        Debug.Log(string.Format("Create InventorySlots for {0} Items", (GetActivePlayer().inventory.Count - 1).ToString()));
        //nicht entfernen, Inventar muss doppelt bereinigt werden, damit es funktioniert
        CleanItemStore();
        if (GetActivePlayer().inventory.Count == 0) return;
        for (int i = 1; i < GetActivePlayer().inventory.Count; i++)
        {
            GameObject obj = Instantiate(template, ItemStore);

            obj.transform.Find("Icon").GetComponent<Image>().sprite = GetActivePlayer().inventory[i].ItemIcon;
            obj.transform.Find("Icon").GetComponent<Image>().enabled = true;
            obj.GetComponent<Button>().enabled = true;
            obj.GetComponent<Image>().enabled = true;
            obj.SetActive(true);
            obj.name = string.Format("{0}", i.ToString());

            obj.GetComponent<PlayerInventoryItemController>().AddItem(GetActivePlayer().inventory[i]);
            obj.GetComponent<PlayerInventoryItemController>().SetInventoryIndex(i);
            obj.GetComponent<PlayerInventoryItemController>().SetPlayer(GetActivePlayer());
            obj.GetComponent<PlayerInventoryItemController>().SetSwapButton();
        }
        Destroy(template);
        Debug.Log(string.Format("{0} childs added to ItemStore", ItemStore.childCount.ToString()));
    }

    private void CleanItemStore()
    {
        foreach (Transform t in ItemStore)
        {
            DestroyImmediate(t.gameObject);
        }
        Debug.Log("Inventory cleaned");
        Debug.Log(string.Format("Childs of ItemStore after cleaning: {0}", (ItemStore.childCount).ToString()));
    }

    public Player GetActivePlayer()
    {
        if (PlayerManager.PMInstance.activePlayer == null)
        {
            PlayerManager.PMInstance.SetActivePlayer(0);
        }
        return PlayerManager.PMInstance.activePlayer;
    }

    public Item GetActiveItem()
    {
        if (PlayerManager.PMInstance.activePlayer.inventory.Count == 0 || PlayerManager.PMInstance.activePlayer.inventory[0] == null)
        {
            return null;
        }
        return GetActivePlayer().inventory[0];      
    }

    public void SetSelectedItem()
    {
        SelectedItem = GetActiveItem();
    }
}

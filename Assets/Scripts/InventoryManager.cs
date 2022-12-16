using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public Item Selected;
    public Transform ItemContent;
    public GameObject InventoryItem;

    public Toggle EnableRemove;

    public InventoryItemController[] InventoryItems;

    public KeyCode OpenInventoryKey;
    public Button openInventoryButton;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(OpenInventoryKey))
        {
            openInventoryButton.onClick.Invoke();
        }
    }

    public void Add(Item item)
    {
        Items.Add(item);
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
        ListItems();
    }
    public void CleanItemContent()
    {
        foreach (Transform item in ItemContent)
        {
            item.gameObject.SetActive(false);
            Destroy(item.gameObject);
        }
    }
    public void ListItems()
    {
        CleanItemContent();

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveItem").GetComponent<Button>();

            itemName.text = item.ItemName;
            itemIcon.sprite = item.ItemIcon;

            if (EnableRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }
        }
        SetInventoryItems();
    }

    public void EnableItemsRemove()
    {
        if (EnableRemove.isOn)
        {
            foreach(Transform item in ItemContent)
            {
                item.Find("RemoveItem").gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveItem").gameObject.SetActive(false);
            }
        }
    }

    public void SetInventoryItems()
    {
        var controllers = ItemContent.GetComponentsInChildren<InventoryItemController>();
        InventoryItems = new InventoryItemController[controllers.Length];

        for(int i = 0; i < Items.Count; i++)
        {
            InventoryItems[i] = controllers[i];
            InventoryItems[i].AddItem(Items[i]);
        }
    }
}

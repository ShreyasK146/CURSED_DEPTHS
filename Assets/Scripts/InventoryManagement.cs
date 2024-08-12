using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManagement : MonoBehaviour
{
    #region Variable to handle inventory items and it's functionality
    public static InventoryManagement Instance;
    public List<Item> Items = new List<Item>();
    public Transform itemContent;
    public GameObject inventoryItem;
    public GameObject readButton;
    public delegate void ItemSelectedEventHandler(object sender, Item selectedItem);//not in use
    public event EventHandler<Item> OnItemSelected;
    [SerializeField] private InventoryItem i;
    #endregion

    #region Method to handle the instance of a inventory
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            // If an instance already exists, destroy this one
            Destroy(gameObject);
            return;
        }

    }
    #endregion

    #region Method to add the item in inventory
    public void Add(Item item)
    {
        if (item != null)
        {
            Items.Add(item);

            if (i != null && i.spawnPrefabDropped != null && !i.spawnPrefabDropped.ContainsKey(item))
            {
                i.spawnPrefabDropped[item] = false; // Initialize as false since the item is not dropped yet
            }

            ListItems();
        }
    }
    #endregion

    #region Method to remove the item from inventory
    public void Remove(Item item)
    {
        if (item != null)
        {
            Items.Remove(item);

            ListItems();
        }


    }
    #endregion

    #region Method to check if the item is in inventory
    public bool HasItem(string itemName)
    {
        foreach (var item in Items)
        {
            if (item != null && item.itemName == itemName)
            {
                return true; // Found the item in the inventory
            }
        }
        return false; // Item not found in the inventory
    }
    #endregion

    #region Method to refresh and list the items in inventory and inventorycanvas
    public void ListItems()
    {

        //clean content before open
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }


        foreach (var item in Items)
        {
            GameObject obj = Instantiate(inventoryItem, itemContent);
            ;
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            obj.GetComponent<Button>().onClick.AddListener(() => { SelectedItem(item); });

        }
        int amount = 0;
        foreach (var item in Items)
        {
            amount = amount + item.price;
            i.totalAmountDisplay.text = amount.ToString();

        }
        if (Items.Count == 0)
        {
            readButton.gameObject.SetActive(false);
        }
    }
    #endregion

    #region Method is used to notify subscribers (event handlers) when an item is selected.
    public void SelectedItem(Item selectedItem)
    {
        OnItemSelected?.Invoke(this, selectedItem);
    }
    #endregion



}

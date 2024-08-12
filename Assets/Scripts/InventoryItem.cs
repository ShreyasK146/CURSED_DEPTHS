

using System;
using System.Collections;
using System.Collections.Generic;

using System.Text.RegularExpressions;
using System.Threading;
using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IDragHandler
{
    Item item;
    private Item currentlySelectedItem;
    private Transform inspectPrefab;
    private Transform spawnPrefab;
    private Transform playerPos;
    public Button dropButton;
    public GameObject player;
    public InventoryManagement inventoryManagement;
    public TextMeshProUGUI totalAmountDisplay;
    [SerializeField] private StoryScript story;
    [SerializeField] GridLayoutGroup gridLayoutGroup;
    [SerializeField] GameObject inspectPanel;
    [SerializeField] private GameObject readButton;
    [HideInInspector] public static bool buttonClicked = false;
    [HideInInspector] public Dictionary<Item, bool> spawnPrefabDropped = new Dictionary<Item, bool>();
    

    
    public void Start()
    {
            
        inventoryManagement.OnItemSelected += ItemSelected;
        inventoryManagement.OnItemSelected += UpdateStoryText;
        dropButton.onClick.AddListener(DropTheItem);
        playerPos = player.GetComponent<Transform>();
        dropButton.gameObject.SetActive(false);
        
    }

    #region Method that handles the UI part of storytext
    private void UpdateStoryText(object sender, Item e)
    {
        if (story.dict.ContainsKey(e.id))
        {
            story.storyText.text = story.dict[e.id];
        }
    }
    #endregion

    #region Method that subscribes to OnItemSelected event and handles the Functionality of Item selecting
    public void ItemSelected(object sender, Item e)
    {
       
        readButton.gameObject.SetActive(true);
        if(e.itemName != "GoldenKey" && e.itemName!= "Book" && e.itemName!= "KeyDoor1" && e.itemName!= "KeyDoor2" && e.itemName!= "KeyDoor3" && e.itemName != "Goldcoins" && e.itemName != "Goldcoins1" && e.itemName != "Goldcoins2" && e.itemName != "Goldcoins3")
            dropButton.gameObject.SetActive(true);
        else
            dropButton.gameObject.SetActive(false);
        if (spawnPrefabDropped.ContainsKey(e) && spawnPrefabDropped[e])
        {
            // The item has been dropped, set it to not dropped
            spawnPrefabDropped[e] = false;
        }

        // Check if there is a currently selected item
        if (currentlySelectedItem != null)
        {
            // Check if the associated spawnPrefab of the currently selected item is not marked as dropped
            if (spawnPrefabDropped.ContainsKey(currentlySelectedItem) && !spawnPrefabDropped[currentlySelectedItem])
            {
                // Destroy the spawnPrefab associated with the currently selected item
                if (spawnPrefab != null && spawnPrefab.gameObject != null)
                {
                    Destroy(spawnPrefab.gameObject);
                }
            }
        }

        // Set the currently selected item to the new item
        currentlySelectedItem = e;
        if (inspectPrefab != null)
        {
            
            Destroy(inspectPrefab.gameObject);
        }
        inventoryManagement.readButton.SetActive(true);
        inspectPrefab = Instantiate(e.prefab, new Vector3(1000.005f, 1000.038f, 997.32f), Quaternion.identity);
        spawnPrefab = Instantiate(e.prefab, new Vector3(900f, 900f, 900f), Quaternion.identity);
        Rigidbody rb = inspectPrefab.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
        }
        item = e;
        
    }
    #endregion

    #region Method to handle the functionality of dropping the item
    public void DropTheItem()
    {

        if (buttonClicked)
        {
            return; // Ignore additional clicks until cooldown period is over
        }

        buttonClicked = true;
        StartCoroutine(ButtonClickCooldown());
        if (spawnPrefab != null)
        {
       
            Rigidbody itemRigidbody = spawnPrefab.GetComponent<Rigidbody>();

            if (itemRigidbody != null)
            {
                
                itemRigidbody.useGravity = true;
                itemRigidbody.isKinematic = false;
                itemRigidbody.velocity = Vector3.zero;
               
                
            }
            PickupScript pickupScript = spawnPrefab.gameObject.AddComponent<PickupScript>();
            pickupScript.player = player;
            pickupScript.item = item;


           

            // Set the position and rotation to the player's position and rotation
            spawnPrefab.position = playerPos.position;
            spawnPrefab.rotation = playerPos.rotation;

            // Activate the itemPrefab to drop it from the player's position
            spawnPrefab.gameObject.SetActive(true);

            // Optionally, you might want to add some force to the dropped item for a more natural drop effect
            itemRigidbody.AddForce(playerPos.forward * 9f, ForceMode.Impulse);
            if (item != null)
            {
                spawnPrefabDropped[item] = true; // Set to true when the item is dropped
            }

            RemoveUIOfItem(item);
            dropButton.gameObject.SetActive(false);
            readButton.gameObject.SetActive(false);

        }
    }
    #endregion

    #region To have the delay between each mouse click
    IEnumerator ButtonClickCooldown()
    {
        yield return new WaitForSeconds(1f);
        buttonClicked = false;
    }
    #endregion

    #region Method used to update the UI after removing item
    public void RemoveUIOfItem(Item itemToRemove)
    {
        int amount = 0;
        foreach (Item i in inventoryManagement.Items)
        {
            amount = amount + i.price;
        }
        totalAmountDisplay.text = (amount-itemToRemove.price).ToString();
        //selectedItemAmountDisplay.alpha = 0;

        inventoryManagement.Items.Remove(itemToRemove);
        InventoryManagement.Instance.ListItems();
        Destroy(inspectPrefab.gameObject);  

    }
    #endregion

    #region Method used to handle the mouse drag of an element
    public void OnDrag(PointerEventData eventData)
    {
        if (inspectPrefab != null)
        {
            inspectPrefab.eulerAngles += new Vector3(-eventData.delta.y, -eventData.delta.x, 0f);
        } 
    }
    #endregion
}

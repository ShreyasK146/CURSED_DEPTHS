
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    #region Variables to handle the item pickup
    public Item item;
    public GameObject player;
    [SerializeField] TipManager tipManager;
    private bool playerIsInRange = false;
    #endregion

    #region Method to pickup items
    public void Update()
    {
        if (playerIsInRange && Input.GetButtonDown("E") && InventoryManagement.Instance.Items.Count <= 25)
        {
            if (item.itemName != "BlueCrystal" && item.itemName != "RedCrystal")
            {
                TogglePickup();
            }
            else if (item.itemName == "BlueCrystal" || item.itemName == "RedCrystal")
            {
                if (InventoryManagement.Instance.HasItem("Pickaxe"))
                {
                    TogglePickup();
                }

                else
                    tipManager.ShowTip("Pickaxe");
            }
        }
    }
    #endregion

    #region Method to check if player is in or out of range of thr items
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerIsInRange = true;
        }

    }
    public void OnTriggerExit(Collider other)
    {
        playerIsInRange = false;
    }
    #endregion

    #region method to add the item in inventory
    public void TogglePickup()
    {
        if (item.name != "BlueCrystal" || item.name != "RedCrystal")
        {

            InventoryManagement.Instance.Add(item);
        }
        else
        {
            if (InventoryManagement.Instance.HasItem("Pickaxe"))
            {
                InventoryManagement.Instance.Add(item);
            }
           
        }
        Destroy(gameObject);
    }
    #endregion
}

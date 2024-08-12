using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalTaker : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameObject player;
    [HideInInspector] public bool redCrystalPlaced = false;
    private bool playerInRangeCrystalTaker = false;


    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {

            if (this.CompareTag("crystaltaker"))
                playerInRangeCrystalTaker = true;

        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            if (this.CompareTag("crystaltaker"))
                playerInRangeCrystalTaker = false;
        }
    }

    private void Update()
    {

        if (playerInRangeCrystalTaker && !redCrystalPlaced)
        {
            if (Input.GetButtonDown("E") && InventoryManagement.Instance.HasItem("RedCrystal"))
            {
                playerMovement.EnterNewLocation("crystalplaced");
                redCrystalPlaced = true;
            }
            else if(Input.GetButtonDown("E"))
            {
                
                playerMovement.EnterNewLocation("crystaltaker");
            }

        }
        
    }
}

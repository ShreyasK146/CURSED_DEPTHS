
using UnityEngine;
using UnityEngine.SceneManagement;


public class Inventory : MonoBehaviour
{
    #region Variables to handle the inventory canvas
    public GameObject canvasForInventory;
    [SerializeField] GameObject readButton;
    [SerializeField] GameObject dropButton;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameObject canvasForText;
    #endregion

    #region Method that handles the inventory canvas 
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (!canvasForInventory.activeSelf && !canvasForText.activeSelf)
                {
                    Cursor.lockState = CursorLockMode.None;
                    canvasForInventory.SetActive(true);
                    InventoryItem.buttonClicked = false;
                    InventoryManagement.Instance.ListItems();
                    readButton.gameObject.SetActive(false);
                    playerMovement.canMove = false;
                    //Time.timeScale = 0f;

                }
                else if (canvasForInventory.activeSelf)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    canvasForInventory.SetActive(false);
                    dropButton.gameObject.SetActive(false);
                    readButton.gameObject.SetActive(false);
                    playerMovement.canMove = true;
                    //Time.timeScale = 1f;
                }
            }
        }

    }
    #endregion

}

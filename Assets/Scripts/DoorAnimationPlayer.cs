
using UnityEngine;

public class DoorAnimationPlayer : MonoBehaviour
{


    #region Variables to check players proximity to doors and others
    private bool playerInRangeDoor1 = false;
    private bool playerInRangeDoor2 = false;
    private bool playerInRangeDoor3 = false;
    private bool playerInRangeDoor4 = false;
    
    #endregion

    #region Variables to handle the door animations
    private Animator animator;
    private Animator animator2;
    private Animator animator3;
    private Animator animator4;

    private bool animationPlayed = false;
    private bool animationPlayed2 = false;
    private bool animationPlayed3 = false;
    private bool animationPlayed4 = false;
    #endregion


    [SerializeField] GameObject player;
    [SerializeField] AudioSource doorOpenAudio;
    [SerializeField] PlayerMovement playerMovement;
    [HideInInspector] public bool cursedDoorOpened = false;
    [SerializeField] CrystalTaker crystalTaker;
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator2 = GetComponent<Animator>();
        animator3 = GetComponent<Animator>();
        animator4 = GetComponent<Animator>();
    }

    #region Method to determine if player is near or not from the doors
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {

            if (this.CompareTag("Door1Trigger"))
                playerInRangeDoor1 = true;
            else if (this.CompareTag("Door2Trigger"))
                playerInRangeDoor2 = true;
            else if (this.CompareTag("Door3Trigger"))
                playerInRangeDoor3 = true;
            else if (this.CompareTag("Door4Trigger"))
                playerInRangeDoor4 = true;

            
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            // Reset the corresponding playerInRange flag when the player exits
            if (this.CompareTag("Door1Trigger"))
                playerInRangeDoor1 = false;
            else if (this.CompareTag("Door2Trigger"))
                playerInRangeDoor2 = false;
            else if (this.CompareTag("Door3Trigger"))
                playerInRangeDoor3 = false;
            else if (this.CompareTag("Door4Trigger"))
                playerInRangeDoor4 = false;
        }
    }
    #endregion

    #region Method to handle the door animations 1,2,3,4
    private void Update()
    {
        
        if (playerInRangeDoor1 && Input.GetButtonDown("E") && !animationPlayed)
        {

            bool hasKey1 = InventoryManagement.Instance.HasItem("KeyDoor1");

            if (hasKey1)
            {
                animator.SetTrigger("DoorOpen");
                doorOpenAudio.Play();
                animationPlayed = true;
            }
            else
            {
                playerMovement.EnterNewLocation("MetalDoor");
            }
        }

        if (playerInRangeDoor2 && Input.GetButtonDown("E") && !animationPlayed2)
        {

            bool hasKey2 = InventoryManagement.Instance.HasItem("KeyDoor2");

            if (hasKey2)
            {
                animator2.SetTrigger("DoorOpen2");
                doorOpenAudio.Play();
                animationPlayed2 = true;
            }
            else
            {
                playerMovement.EnterNewLocation("MetalDoor");
            }
        }

        if (playerInRangeDoor3 && Input.GetButtonDown("E") && !animationPlayed3)
        {

            bool hasGoldenKey = InventoryManagement.Instance.HasItem("GoldenKey");

            if (hasGoldenKey)
            {
                animator3.SetTrigger("DoorOpen3");
                doorOpenAudio.Play();
                animationPlayed3 = true;
                cursedDoorOpened = true;
                playerMovement.EnterNewLocation("CaveCollapse");
            }
            else
            {
                playerMovement.EnterNewLocation("GoldenDoor");
            }
        }
        if (playerInRangeDoor4 && Input.GetButtonDown("E") && !animationPlayed4)
        {
            bool hasKey3 = InventoryManagement.Instance.HasItem("KeyDoor3");
         
            if (hasKey3 || crystalTaker.redCrystalPlaced)
            {
                animator4.SetTrigger("DoorOpen4");
                doorOpenAudio.Play();
                animationPlayed4 = true;
           
            }
            else
            {
                playerMovement.EnterNewLocation("treasuredoor");
            }
        }
        
    }
    #endregion
}

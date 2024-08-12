using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    public PlayerStamina playerStamina;
    public CharacterController characterController;
    [SerializeField] GameObject characterModel;

    [SerializeField] ObjectiveTexts objective;
    [SerializeField] InventoryManagement inventoryManagement;
    [SerializeField] AudioSource jumpSound;
    [SerializeField] GameElementsAudio gameElementsAudio;
    [SerializeField] AsyncLoader loader;

    [SerializeField] GameObject lights;
    [SerializeField] GameObject credits;
    [SerializeField] GameObject canvasForInitialMessage;

    public string currentLocation = "StartingArea";
    public TipManager tipManager;
    private bool tipReady = false;
    private Animator animator;

    #region Variables to handle the player movements
    public float walkSpeed = 2.5f;
    public float runSpeed = 5f;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;
    public float speed;
    Vector3 velocity;
    public bool isGrounded;
    public bool isRunning = false;
    public bool canJump = true;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    private float jumpCooldown = 2f;
    public LayerMask groundMask;
    public bool canMove = true;
    public bool isMoving = false;
    #endregion

    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            if(!PlayerPrefs.HasKey("FirstRunCave"))
            {
                canvasForInitialMessage.gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;

            }
                
        }

        if (SceneManager.GetActiveScene().buildIndex == 1 && !PlayerPrefs.HasKey("BookCollected"))
        {
            objective.objective.text = "Venture into the Mysterious Cave";
            objective.optionObjective.text = "Explore the Island";
        }
        else if(SceneManager.GetActiveScene().buildIndex == 1 && PlayerPrefs.HasKey("BookCollected"))
        {
            objective.objective.text = "Return to the HeliPad";
            objective.optionObjective.text = "";
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            objective.objective.text = "Find the Golden key or Offer the Red Crystal";
            objective.optionObjective.text = "Explore the Cave and loot as much as you can";
        }
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("isCrouching", false);
        StartCoroutine(SetPlayerPosition());
    }

    #region Method to handle the player's spawning pos when switching the scenes
    private IEnumerator SetPlayerPosition()
    {
        yield return null; // Wait for one frame to ensure everything is initialized
        if (!PlayerPrefs.HasKey("FirstRunCave"))
        {
            // Set the "FirstRunCave" key when entering the cave for the first time
            PlayerPrefs.SetInt("FirstRunCave", 1);
            PlayerPrefs.Save();
        }
        else
        {
            // Set the player's position for returning to the scene
            GameObject respawnPoint = GameObject.Find("RespawnPoint");
            if (respawnPoint != null)
            {
                transform.position = respawnPoint.transform.position;
            }
        }
    }
    #endregion

    #region To check if Player clicked canvas button of InitialMessage
    public void CheckForButtonClicked()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (canvasForInitialMessage.activeSelf)
            {
                canvasForInitialMessage.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
                Cursor.lockState = CursorLockMode.Locked;
                tipReady = true;

            }
        }
    }
    #endregion

    #region Methods to check player movement,jumping,animation,etc..
    void Update()
    {

        
        if (inventoryManagement.HasItem("GoldenKey"))
        {
            objective.objective.text = "Acquire the legendary tome known as \"Eternal Vitality\".";
        }

        
        //CheckSphere is a function in Unity's physics system that checks if there are any overlapping colliders within a given spherical volume.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //is to ensure that the object has a stable downward velocity (velocity.y) when it is grounded.
        if (isGrounded && velocity.y <= 0)
        {
            //object remains grounded firmly. It counteracts the effect of gravity and prevents
            //the object from floating or bouncing due to small physics inaccuracies or variations in collision detection.
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");//players left or right movement
        float z = Input.GetAxis("Vertical");//players up or downward movement


        //adds the horizontal and vertical movement components together
        //represents the right direction (x-axis) 
        //represents the forward direction (z-axis) 
        Vector3 move = transform.right * x + transform.forward * z;


        isRunning = Input.GetKey(KeyCode.LeftShift);
        //doing this to check if the character is moving or not
        Vector3 isMove = transform.right * x + transform.forward * z;
        isMoving = move.magnitude > 0.1f;
        
        // Set animator parameters based on the player's stamina and movement and decrease/increase stamina overtime
        if (playerStamina.currentStamina > 0 && isRunning)
        {

            // Running animation
            speed = runSpeed;
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
            playerStamina.currentStamina -= playerStamina.depletionRate * Time.deltaTime;
        }
        else
        {
            // Walking animation
            speed = walkSpeed;
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", isMoving);
            if (!isRunning && playerStamina.currentStamina < playerStamina.maxStamina)
            {
                playerStamina.currentStamina += playerStamina.regenRate * Time.deltaTime;
                    
            }
        }

        //stamina should be between 0 to max
        playerStamina.currentStamina = Mathf.Clamp(playerStamina.currentStamina, 0f, playerStamina.maxStamina);

        //moving the character
        if(canMove)
        {
            characterController.Move(move * speed * Time.deltaTime);
        }    

        if (Input.GetButtonDown("Jump") && isGrounded && canJump && canMove)
        {

            canJump = false;
            characterModel.SetActive(false);
            animator.SetTrigger("TriggerJump");
            gameElementsAudio.walkingOnCave.Stop();
            gameElementsAudio.runningOnCave.Stop();
            gameElementsAudio.runningOnLand.Stop();
            gameElementsAudio.walkingOnLand.Stop();
            jumpSound.Play();
    

            // calculates the vertical component of the velocity (velocity.y) needed to perform a jump. 
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            if(playerStamina.currentStamina > 0)
            {
                playerStamina.currentStamina -= playerStamina.jumpCost; 
            }    

        }
        //if grounded player should be able to jump after certain cooldown period
        if (isGrounded)
        {
            
            StartCoroutine(ResetJumpCooldown());
           
        }

        //updates the vertical velocity (velocity.y) by adding the effect of gravity over time
        velocity.y += gravity * Time.deltaTime;

        //moves the character controller based on the updated velocity
        characterController.Move(velocity * Time.deltaTime);

        if(inventoryManagement.HasItem("Book"))
        {
             
             PlayerPrefs.SetInt("BookCollected", 1);
             PlayerPrefs.Save();

        }
        if(PlayerPrefs.HasKey("BookCollected") && SceneManager.GetActiveScene().buildIndex == 2)
        {
            objective.objective.text = "YOU GOT THE LEGENDARY BOOK.Escape the cave before it falls apart.";
            objective.optionObjective.text = "";
        }
        
    }
    #endregion

    #region Method to handle jumping immediately after a jump
    private IEnumerator ResetJumpCooldown()
    {
        yield return new WaitForSeconds(jumpCooldown);
       //playerCamera.cullingMask = originalCullingMask;
        if (playerStamina.currentStamina >30f)
        {
            canJump = true;
            characterModel.SetActive(true);
        }  
    }
    #endregion

    #region Method to display tips when player enters new location
    public void EnterNewLocation(string locationName)
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            if(tipReady) 
            {
                currentLocation = locationName;
                tipManager.ShowTip(currentLocation);
            }

            
        }
        else
        {
            currentLocation = locationName;
            tipManager.ShowTip(currentLocation);
        }
        
    }
    #endregion

    #region Method to handle the player's trigger events 
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cave"))
        {
            if(!PlayerPrefs.HasKey("BookCollected")) 
            {
                if (!PlayerPrefs.HasKey("FirstRunCave"))
                {
                    
                    PlayerPrefs.SetInt("FirstRunCave", 1);
                    PlayerPrefs.Save();
                }
                loader.LoadLevel(2);
                PlayerPrefs.DeleteKey("BookCollected");
                PlayerPrefs.Save();
                SceneManager.LoadScene("InsideTheCatacombs");
                Cursor.lockState = CursorLockMode.None;
            }
            
        }
        if (other.CompareTag("CursedStone"))
        {
            EnterNewLocation("CursedStone");
        }
        if (other.CompareTag("CaveExit"))
        {
            loader.LoadLevel(1);

            SceneManager.LoadScene("GameScene");
            Cursor.lockState = CursorLockMode.None;
        }

        if (other.CompareTag("GameEnd"))
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                if (PlayerPrefs.HasKey("BookCollected")) 
                {
                    credits.gameObject.SetActive(true);
                }
                else if(!PlayerPrefs.HasKey("BookCollected")) 
                {
                    
                }
                    
            }
        }    

    }
    #endregion

    #region Method to load the MainMenuscene
    public void LoadScene()
    {
        
        SceneManager.LoadScene("MainMenuScene");
        Time.timeScale = 1.0f;
    }
    #endregion

    #region Method to load CaveScene
    public void LoadSceneCataCombs()
    {
        PlayerPrefs.DeleteKey("BookCollected");
        PlayerPrefs.Save();
        SceneManager.LoadScene("InsideTheCatacombs");
        Time.timeScale = 1.0f;

    }
    #endregion

}

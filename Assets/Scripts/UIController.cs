
using UnityEngine;


public class UIController : MonoBehaviour
{
    #region Variables to control the UI behaviour
    public GameObject canvas;
    public GameObject menuAudioPlayer;
    [HideInInspector] public bool playerDied = false;
    
    [SerializeField] GameObject canvasForSettings;
    [SerializeField] GameObject StoryCanvas;
    [SerializeField] GameObject inventoryCanvas;
    [SerializeField] PlayerMovement player;
    [SerializeField] AudioSource walk;
    [SerializeField] AudioSource run;
    [SerializeField] AudioSource breath;
    #endregion

    void Start()
    {
        // Ensure the canvas starts inactive
        canvas.SetActive(false);
        
    }

    #region Method handlee the main canvas behaviour
    void Update()
    {
        // upon pressing escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            //canvas is active ? then lock the cursor,disable canvas,stop playing audio,resume the game 
            if (canvas.activeSelf && !playerDied)
            {

                Cursor.lockState = CursorLockMode.Locked;
                canvas.gameObject.SetActive(false);
                menuAudioPlayer.gameObject.SetActive(false);
                Time.timeScale = 1f;


            }
            //canvas is not active ? then unlock the cursor,enable canvas,start playing audio,pause the game 
            else
            {
                if (StoryCanvas.activeSelf || inventoryCanvas.activeSelf || canvasForSettings.activeSelf)
                {
                    inventoryCanvas.gameObject.SetActive(false);
                    StoryCanvas.gameObject.SetActive(false);
                    canvasForSettings.gameObject.SetActive(false);

                }
                Cursor.lockState = CursorLockMode.None;
                canvas.SetActive(true);
                menuAudioPlayer.gameObject.SetActive(true);
                if (walk.isPlaying)
                    walk.Stop();
                if (run.isPlaying)
                    run.Stop();
                if (breath.isPlaying)
                    breath.Stop();
                Time.timeScale = 0f;
                player.canMove = true;

            }

        }


    }
    #endregion

    #region Method to check if game is paused
    public bool IsGamePaused()
    {
        return Time.timeScale == 0f;
    }
    #endregion
}



using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathChecker : MonoBehaviour
{
    #region Variables to control the animation and canvases after player dies
    public PlayerHealthController healthController;
    public GameObject diedMessageCanvas;
    public GameObject canvasForDamage;
    public Animator animator;
    private string animationStateName = "DiedAnimation";
    //public GameObject restartCanvas;
    private bool animationPlayed = false;
    [SerializeField]private UIController ui;
    [SerializeField] AudioSource mainMenu;
    #endregion

    #region Method to handle the animation and canvases after player dies
    void Update()
    {
        if(healthController.currentHealth <=0f && !animationPlayed)
        {
            PlayerPrefs.DeleteKey("BookCollected");
            PlayerPrefs.Save();
            diedMessageCanvas.SetActive(true);
            animator.SetTrigger("playerDied");
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if(stateInfo.IsName(animationStateName) && stateInfo.normalizedTime >= 1.0f)
            {
                diedMessageCanvas.SetActive(false);
                animationPlayed = true;
                Time.timeScale = 0f;
                ui.playerDied = true;
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("MainMenuScene");
                if (!mainMenu.gameObject.activeSelf) 
                {
                    
                    mainMenu.gameObject.SetActive(true);
                    
                }
            }
        }
    }
    #endregion
}

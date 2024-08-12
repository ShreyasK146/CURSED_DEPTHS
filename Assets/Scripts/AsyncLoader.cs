using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsyncLoader : MonoBehaviour
{
    #region Loading screen elements 
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider loadingSlider;
    #endregion

    #region UI elements to be disabled if it's on 
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject inventory;
    [SerializeField] PlayerHealthController health;
    [SerializeField] GameObject tips;
    [SerializeField] GameObject text;
    #endregion

    #region Audio clips that needs to be disabled if it's on
    [SerializeField] AudioSource walk;
    [SerializeField] AudioSource run;
    [SerializeField] AudioSource breath;
    #endregion

    #region Method to load the loadingscreen
    public void LoadLevel(int level)
    {
        //if the scene is not mainmenu then just start the loading screen and disable audio & unnecessary UI elements
        if (SceneManager.GetActiveScene().buildIndex != 0) 
        {
            if (health.currentHealth > 0)
            {
                mainMenu.gameObject.SetActive(false);
                inventory.gameObject.SetActive(false);
                text.gameObject.SetActive(false);
                tips.gameObject.SetActive(false);
                if(walk.isPlaying || run.isPlaying || breath.isPlaying) 
                {
                    walk.Stop();
                    run.Stop(); 
                    breath.Stop();
                }
                loadingScreen.gameObject.SetActive(true);
                StartCoroutine(LoadLevelAsync(level));
            }
        }
        else
        {
            //if the scene is mainmenu then just start the loading screen
            mainMenu.gameObject.SetActive(false);
            loadingScreen.gameObject.SetActive(true);
            StartCoroutine(LoadLevelAsync(level));
        }
 
    }
    #endregion

    #region Coroutine to load the screen
    IEnumerator LoadLevelAsync(int levelToLoad)
    {

        //This method loads the specified scene asynchronously,
        //meaning it won't block the main thread and allows the game to continue running while the scene is being loaded.
        //The method returns an AsyncOperation object, which represents the loading process
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);
        
        while(!loadOperation.isDone) 
        {
            //Inside the loop, we calculate the progress of the loading operation.
            //The loadOperation.progress property represents the loading progress as a value between 0 (loading just started)
            //and 1 (loading complete). However, the loading progress doesn't go up to 1 directly; it stops at around 0.9 and then
            //jumps to 1. Therefore, we divide loadOperation.progress by 0.99 and clamp the result to a value between 0 and 1 using
            //Mathf.Clamp01(). This way, we get a smooth loading progress from 0 to 1.
            float progressValue = Mathf.Clamp01(loadOperation.progress/0.99f);

            //The calculated progressValue is assigned to the value property of the loadingSlider
            loadingSlider.value = progressValue;
            //This line tells the coroutine to pause here and return control to Unity for one frame. 
            //The next frame, the coroutine will resume from this point, allowing the game to update the UI,
            //handle input, and perform other tasks.This is important because, during an asynchronous loading operation,
            //you don't want to block the main thread for too long, as it could lead to a frozen or unresponsive game.
            //By yielding each frame, the game continues to function smoothly while the scene is being loaded in the background.
            yield return null;
        }
    }
    #endregion

}

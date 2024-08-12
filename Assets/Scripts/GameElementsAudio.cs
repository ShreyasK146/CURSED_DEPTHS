using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameElementsAudio : MonoBehaviour
{
    #region Audioclip variables that needs to be handled
    [SerializeField] AudioSource mainMenu;
    [SerializeField] AudioSource heavyBreathing;
    public AudioSource walkingOnCave;
    public AudioSource runningOnCave;
    public AudioSource walkingOnLand;
    public AudioSource runningOnLand;
    #endregion

    [SerializeField] PlayerStamina playerStamina;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameObject main;

    #region Variables to handle breathingaudio
    private bool fadingOut = false; // Flag to track whether the audio is currently fading out
    private float fadeOutDuration = 5.0f; // Adjust this value to control the duration of the fade-out
    private bool fadingIn = false; // Flag to track whether the audio is currently fading in
    private float fadeInDuration = 5.0f; // Adjust this value to control the duration of the fade-in
    private Coroutine fadeInCoroutine; // Store the reference to the fadeIn coroutine
    #endregion

    void Update()
    {
        if (!mainMenu.isPlaying && !main.gameObject.activeSelf)
        {

            #region Handling Breathing audio 
                if (playerStamina.currentStamina < 30f)
                {
                    if (!heavyBreathing.isPlaying && !fadingIn)
                    {
           

                        heavyBreathing.Play();
                        fadingOut = false; 

                        // If a fadeIn coroutine is already running, stop it
                        if (fadeInCoroutine != null)
                        {
                            StopCoroutine(fadeInCoroutine);
                        }

                        // Start fading in the audio
                        fadeInCoroutine = StartCoroutine(FadeInAudio());
                    }
                }
                else
                {
                    if (heavyBreathing.isPlaying && !fadingOut)
                    {
                        // Stop the fadeIn coroutine if it's still running
                        if (fadeInCoroutine != null)
                        {
                            StopCoroutine(fadeInCoroutine);
                            fadeInCoroutine = null;
                        }

                        // Start fading out the audio
                        fadingIn = false;
                        StartCoroutine(FadeOutAudio());
                    }
                }
            #endregion

            #region Audio functionality when player is in Scene 2
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                if (!playerMovement.isMoving)
                {
                    if (walkingOnCave.isPlaying)
                    {
                        walkingOnCave.Stop();
                        runningOnCave.Stop();
                    }
                }
                else if (playerMovement.speed == playerMovement.walkSpeed && !walkingOnCave.isPlaying && playerMovement.isGrounded)
                {
                    runningOnCave.Stop();
                    walkingOnCave.Play();
                    walkingOnCave.pitch = 1.2f;
                }
                else if (playerMovement.speed == playerMovement.runSpeed && !runningOnCave.isPlaying && playerMovement.isGrounded)
                {
                    walkingOnCave.Stop();
                    runningOnCave.Play();
                }

            }
            #endregion

            #region Audio functionality when player is in Scene 1 
            else if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                if (playerMovement.speed == playerMovement.walkSpeed && !walkingOnLand.isPlaying)
                {
                    runningOnLand.Stop();
                    walkingOnLand.Play();
                    walkingOnLand.pitch = 1.2f;
                }
                else if (playerMovement.speed == playerMovement.runSpeed && !runningOnLand.isPlaying)
                {
                    walkingOnLand.Stop();
                    runningOnLand.Play();
                }
                else if (!playerMovement.isMoving)
                {
                    if (walkingOnLand.isPlaying)
                    {
                        walkingOnLand.Stop();
                    }
                }
            }
            #endregion
        }
    }

    #region Handling FadingOut of Breathing clip
    IEnumerator FadeOutAudio()
    {
        fadingOut = true;
        float startVolume = heavyBreathing.volume;

        for (float t = 0; t < fadeOutDuration; t += Time.deltaTime)
        {
            // Calculate the new volume based on time and duration
            float newVolume = Mathf.Lerp(startVolume, 0f, t / fadeOutDuration);
            heavyBreathing.volume = newVolume;
            yield return null;
        }

        
        heavyBreathing.volume = 0f;
        heavyBreathing.Stop();
        fadingOut = false; 
    }
    #endregion

    #region Handling FadingIn of Breathing clip
    IEnumerator FadeInAudio()
    {
        fadingIn = true;

        for (float t = 0; t < fadeInDuration; t += Time.deltaTime)
        {
     
            float newVolume = Mathf.Lerp(0f, 0.15f, t / fadeInDuration);
            heavyBreathing.volume = newVolume;
            yield return null;
        }

        
        heavyBreathing.volume = 0.15f;
        fadingIn = false; 
    }
    #endregion
}


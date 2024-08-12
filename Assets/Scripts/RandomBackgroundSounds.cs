
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomBackgroundSounds : MonoBehaviour
{
    #region Audioclips that needs to be handled
    [SerializeField] AudioSource audio1;
    [SerializeField] AudioSource audio2;
    [SerializeField] AudioSource audio3;
    [SerializeField] AudioSource mainMenuAudio;
    [SerializeField] AudioSource caveCollapse1;
    #endregion

    #region Variables that manage the time delay between clips
    private float minDelay = 30.0f; // Minimum time between audio clips (in seconds)
    private float maxDelay = 60.0f; // Maximum time between audio clips (in seconds)
    private bool canPlayAudio = false;
    private float initialDelay = 60.0f; // Initial delay before playing audio(1 minute)
    #endregion

    void Start()
    {
        InvokeRepeating("PlayRandomAudio", initialDelay, Random.Range(minDelay, maxDelay));
        //plays random audio between the random range of delays after waiting initialdelay
    }
    #region Method to select and play random audio clips
    void PlayRandomAudio()
    {
        if (canPlayAudio && !mainMenuAudio.isPlaying && !caveCollapse1.isPlaying)
        {
            int randomAudioIndex = Random.Range(1, 4);

            switch (randomAudioIndex)
            {
                case 1:
                    audio1.Play();
                    break;
                case 2:
                    audio2.Play();
                    break;
                case 3:
                    audio3.Play();
                    break;
            }
        }
    }
    #endregion

    #region Method to check if audio can be played
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            canPlayAudio = true;
        }
        else
        {
            canPlayAudio = false;
        }
    }
    #endregion
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public GameObject MenuAudioPlayer;

    private void Start()
    {
        //Play the menu audio at the mainmenu

        StartPlayingAudio();

    }

    #region Method to handle the mainmenuaudio
    void StartPlayingAudio()
    {
        if (MenuAudioPlayer != null)
        {
            AudioSource audioSource = MenuAudioPlayer.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play();

            }
        }
    }
    #endregion

    #region Method to handle quitgame

    //1st one is to quit the playing mode in editor,2nd one is quit the game while playing
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
    #endregion
}

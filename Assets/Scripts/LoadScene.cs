//not in use anymore

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    //loading the mainmenuscene
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}

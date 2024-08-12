using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
/// <summary>
/// handles the settings variables + handles the saving of settings choosen(not working 100%)
/// </summary>
public class SettingsMenu : MonoBehaviour
{
    //creating mastermixer and dropdown for resoultion
    public AudioMixer audioMixer;
    [SerializeField] GameObject inventory;
    public TMP_Dropdown resolutionDropDown;
    [SerializeField] GameObject UI;

    Resolution[] resolutions;

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
            inventory.gameObject.SetActive(false);
        //take all possible resolution and clear the resoultion dropdown 
        resolutions = Screen.resolutions;
        resolutionDropDown.ClearOptions();

        // create a list of options because we need to add list of resolution to dropdown. and set 1 index value for the dropdown to 0
        List<string>options = new List<string>();
        int currentResolutionIndex = 0;

        // from all resolution possible create a string variable taking resoultion height and width.And add it in options list
        for(int i = 0; i < resolutions.Length; i++) 
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            //depending on the user's screen width and height set the current index value in the dropdown for user
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        // add the list of resolution strin to options and set the current resoultion from dropdown and refresh the current displayed option on
        //dropdown
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }

    //setting the volume based on audiomixer variable
    public void SetVolume(float volume)
    {
        //Debug.Log(volume);
        audioMixer.SetFloat("volume", volume);
    }

    //setting the quality  based on the available quality level for the project
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
      
        UI.gameObject.SetActive(false);
        UI.gameObject.SetActive(true);
        Debug.Log("GGGG");
    }

    //make fullscreen based on bool value from checkbox
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    //finally call the setresoultion from event and set it to fullscreen default
    public void setResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        UI.gameObject.SetActive(false);
        UI.gameObject.SetActive(true);
        Debug.Log("HHHH");
    }
    //not in use
    private void LoadSettings()
    {
        float volume;
        if (PlayerPrefs.HasKey("Volume") && audioMixer != null)
        {
            audioMixer.GetFloat("volume", out volume);
            SetVolume(volume);
        }

        // Load quality
        if (PlayerPrefs.HasKey("Quality"))
        {
            int quality = PlayerPrefs.GetInt("Quality");
            SetQuality(quality);
        }

        // Load fullscreen
        if (PlayerPrefs.HasKey("Fullscreen"))
        {
            bool fullscreen = PlayerPrefs.GetInt("Fullscreen") == 1;
            SetFullScreen(fullscreen);
        }

        // Load resolution
        if (PlayerPrefs.HasKey("Resolution"))
        {
            int resolutionIndex = PlayerPrefs.GetInt("Resolution");
            setResolution(resolutionIndex);
        }
    }
    private void SaveSettings()
    {
       // Save volume
        float volume;
        audioMixer.GetFloat("volume", out volume);
        PlayerPrefs.SetFloat("Volume", volume);

        // Save quality
        int quality = QualitySettings.GetQualityLevel();
        PlayerPrefs.SetInt("Quality", quality);

        // Save fullscreen
        int fullscreen = Screen.fullScreen ? 1 : 0;
        PlayerPrefs.SetInt("Fullscreen", fullscreen);

        // Save resolution
        PlayerPrefs.SetInt("Resolution", resolutionDropDown.value);
    }
    public void OnSettingChanged()
    {
        SaveSettings();
    }

    // Event handlers for settings changes
    public void OnVolumeChanged(float newVolume)
    {
        SetVolume(newVolume);
        OnSettingChanged();
    }

    public void OnQualityChanged(int newQualityIndex)
    {
        
        SetQuality(newQualityIndex);
        OnSettingChanged();

 

    }

    public void OnFullScreenChanged(bool isFullScreen)
    {
        SetFullScreen(isFullScreen);
        OnSettingChanged();
    }

    public void OnResolutionChanged(int resolutionIndex)
    {
        if (resolutionIndex != resolutionDropDown.value)
        {
            setResolution(resolutionIndex);
            OnSettingChanged();
  

        }
    }

}

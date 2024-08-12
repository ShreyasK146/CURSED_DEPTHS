
using UnityEngine;


public class FlashLightController : MonoBehaviour
{
    #region Variables to handle the flashlight functionality
    public GameObject flashlight;
    [SerializeField] DifficultyElements flashlightEnergy;
    public float currentEnergy;
    public float depletionRate = 1.0f;
    public AudioSource flashLightTurnOn;
    public AudioSource flashLightTurnOff;
    public bool turnedOn;
    #endregion

    void Start()
    {
        //starting values for flashlight
        turnedOn = false;
        flashlight.SetActive(false);
        currentEnergy = DifficultyElements.maxEnergy;
    }

    #region Method to handle flashlight functionality
    void Update()
    {
        //Debug.Log(DifficultyElements.maxEnergy);
        //if flashlight is not on and button f is pressed then play turnon audio and turn on light 
        if (!turnedOn && Input.GetButtonDown("F"))
        {
            flashLightTurnOn.Play();
            flashlight.SetActive(true);
            turnedOn = true;
        }
        //if flashlight on and button f is pressed then play turnoff audio and turn off light 
        else if (turnedOn && Input.GetButtonDown("F"))
        {
            flashLightTurnOff.Play();
            flashlight.SetActive(false);
            turnedOn = false;
        }
        //if flashlight is on battery needs to depleted over time and upon reaching battery less than 50% have the intensity of light reduced

        if (turnedOn)
        {
            currentEnergy -= depletionRate * Time.deltaTime;
            if (currentEnergy <= 50)
            {
                flashlight.GetComponent<Light>().intensity = 0.5f;
            }
            if (currentEnergy <= 0)
            {
                currentEnergy = 0;
                flashlight.SetActive(false);
                turnedOn = false;
            }
        }
    }
    #endregion
}

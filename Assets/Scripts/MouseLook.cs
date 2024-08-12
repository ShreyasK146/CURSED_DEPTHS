
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{

    #region Variables to handle the mouse functionality
    public float mouseSensitivity = 400f;
    [SerializeField] private GameObject canvasForText;
    [SerializeField] private GameObject canvasForInventory;
    public Transform playerBody;
    private float xRotation = 0;
    public Slider sensitivitySlider;
    #endregion 

    #region Initial state of mouse
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 400f);
        sensitivitySlider.value = mouseSensitivity;

    }
    #endregion

    #region Method to handle the mouse functionality
    void Update()
    {
        //to retrieve/calculate the horizontal and vertical movement of the mouse and multiply it by sens and time.deltatime to make sure it's independent
        //of the framerate for smooth movement
        if (!canvasForInventory.activeSelf && !canvasForText.activeSelf)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            //adjusts the xRotation based on the vertical mouse movement, controlling the vertical rotation of the camera. 
            xRotation = xRotation - mouseY;

            //clamping needs to be done because player should not be able to see its back and all
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // applies the xRotation to the local rotation of the gameobject which is camera transform, controlling its vertical rotation.
            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

            //rotates the player  based on the horizontal mouse movement.
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    #endregion

    #region Method to update mouse sens based on UI 
    public void UpdateMouseSensitivity(float newSensitivity)
    {
        mouseSensitivity = newSensitivity;
        PlayerPrefs.SetFloat("MouseSensitivity", newSensitivity);
        PlayerPrefs.Save();
    }
    #endregion

}


using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CreditRolling : MonoBehaviour
{
    #region Variables to roll the credits
    public TextMeshProUGUI creditText;
    public float scrollSpeed = 30f;
    public float startPositionY;
    public float endPositionY = 600f; 
    private bool isScrolling = false; 
    private bool creditEnd = false;
    #endregion

    #region Intial position of creditText
    private void Start()
    {
        isScrolling = true;
        startPositionY = -Screen.height / 2;
        creditText.rectTransform.anchoredPosition = new Vector2(creditText.rectTransform.anchoredPosition.x, startPositionY);
    }
    #endregion

    #region Method to scroll the credits
    private void Update()
    {
        if (PlayerPrefs.HasKey("BookCollected") && SceneManager.GetActiveScene().buildIndex == 1)
        {
            
            if (isScrolling)
            {
                Vector2 position = creditText.rectTransform.anchoredPosition;
                position.y += scrollSpeed * Time.deltaTime;
                //keep updating the y posn of the time so that it looks like scrolling
                creditText.rectTransform.anchoredPosition = position;

                if (position.y >= endPositionY)
                {
                    // Stop scrolling when endPositionY is reached.
                    isScrolling = false;
                    creditEnd = true;
                }
            }
            if(creditEnd)
            {
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("MainMenuScene");
            }
        }
       
    }
    #endregion
}
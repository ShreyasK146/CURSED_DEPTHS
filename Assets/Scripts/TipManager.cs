
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TipManager : MonoBehaviour
{

    #region Variables to handle the tip display
    public Canvas tipCanvas;
    public TextMeshProUGUI tipText;
    public float tipDisplayDuration = 3.0f;
    private float tipTimer = 0.0f;
    public List<TipData> tips = new List<TipData>();
    private bool tipShowed = false;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject initialMessage;
    #endregion

    void Start()
    {
        tipText.gameObject.SetActive(false);
    }

    #region Method that handles tip display
    public void ShowTip(string locationName)
    {

        if (!canvas.gameObject.activeSelf)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                if (!initialMessage.gameObject.activeSelf)
                {
                    TipData tipdata = tips.Find(tip => tip.locaitonName == locationName);
                    if (tipdata != null)
                    {
                        tipText.text = tipdata.tipMessage;
                        tipCanvas.gameObject.SetActive(true);
                        tipText.gameObject.SetActive(true);
                        tipTimer = 0.0f;
                    }
                }
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                TipData tipdata = tips.Find(tip => tip.locaitonName == locationName);
                if (tipdata != null)
                {
                    tipText.text = tipdata.tipMessage;
                    tipCanvas.gameObject.SetActive(true);
                    tipText.gameObject.SetActive(true);
                    tipTimer = 0.0f;
                }
            }


        }
        else
        {
            if (canvas.gameObject.activeSelf)
            {
                return;
            }

        }

    }
    #endregion

    #region Method to handle the total time that tip is displayed and to check if inventory is full
    void Update()
    {
        if (tipText.gameObject.activeSelf)
        {
            tipTimer += Time.deltaTime;
            if (tipTimer > tipDisplayDuration)
            {
                tipCanvas.gameObject.SetActive(false);
                tipText.gameObject.SetActive(false);
            }
        }
        if (InventoryManagement.Instance.Items.Count > 25 && !tipShowed)
        {
            ShowTip("InventoryFull");
            tipShowed = true;
        }
        if (InventoryManagement.Instance.Items.Count <= 25 && tipShowed)
        {
            tipShowed = false;
        }

    }
    #endregion
}

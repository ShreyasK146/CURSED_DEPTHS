using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveditems : MonoBehaviour
{
    #region Method to delete the necessary Playerrefs when game starts
    void Start()
    {
        if (PlayerPrefs.HasKey("FirstRunCave"))
        {
            PlayerPrefs.DeleteKey("FirstRunCave");
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.HasKey("BookCollected"))
        {
            PlayerPrefs.DeleteKey("BookCollected");
            PlayerPrefs.Save();
        }

    }
    #endregion

}

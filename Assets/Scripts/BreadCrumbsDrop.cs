using UnityEngine;
using UnityEngine.SceneManagement;

public class BreadCrumbsDrop : MonoBehaviour
{
    #region Variables
    [SerializeField] GameObject breadcrumbs;
    [SerializeField] Transform player;
    private int count = 0;
    #endregion

    #region Method for Player to drop breadcrumbs
    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (Input.GetButtonDown("R") && count < 200)
            {
                Vector3 offset = player.forward * 0.5f;
                Vector3 spawnPosition = player.transform.position + offset;
                Instantiate(breadcrumbs, spawnPosition, Quaternion.identity);
                count++;
            }
        }  
    }
    #endregion
}

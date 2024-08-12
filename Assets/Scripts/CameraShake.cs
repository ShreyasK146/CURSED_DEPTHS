using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraShake : MonoBehaviour
{
    private float shakeMagnitude = 0.01f;
    private Vector3 originalPos;

    [SerializeField] CaveCollapse cave;
    private void Start()
    {
        originalPos = transform.localPosition;
    }
    private void Update()
    {
        if(cave.caveCollapsing && SceneManager.GetActiveScene().buildIndex == 2)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeMagnitude;
        }
    }


}

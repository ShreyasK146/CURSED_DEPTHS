//not in use

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobModifier : MonoBehaviour
{
    public Transform cameraTransform;
    public float bobIntensity = 0.5f; // Adjust the intensity of the counter-bob.

    private Vector3 originalCameraPosition;

    private void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        originalCameraPosition = cameraTransform.localPosition;
    }

    private void Update()
    {
        // Apply the counter-bob to compensate for the animation's head bob.
        Vector3 counterBob = -cameraTransform.localPosition * bobIntensity;
        cameraTransform.localPosition = originalCameraPosition + counterBob;
    }
}

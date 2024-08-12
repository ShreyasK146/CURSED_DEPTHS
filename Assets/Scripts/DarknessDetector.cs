
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DarknessDetector : MonoBehaviour
{
    #region Variables to handle the darkness and light
    public float darknessThreshold = 10.0f; // Time threshold to consider staying in darkness
    private float darknessTimer = 0.0f;
    private bool isInDarkness = false;
    public FlashLightController flashLightController;
    public DamageEffectUI damageEffectUI;
    public List<Light>lights = new List<Light>();
    #endregion

    #region If the player is in darkness increase the timer and handle the event else start regening the health
    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "InsideTheCatacombs")
        {

            
            if (IsPlayerInDarkness())
            {
                darknessTimer += Time.deltaTime;

                if (darknessTimer >= darknessThreshold && isInDarkness)
                {
                    // Player has stayed in darkness for the required time
                    HandleDarknessEvent();
                }
                isInDarkness = true;
            }


            else
            {
                darknessTimer = 0.0f;
                isInDarkness = false;
                damageEffectUI.RegenDamageEffect();
            }
        }
        
    }
    #endregion

    #region Handles darkness event
    private void HandleDarknessEvent()
    {
        damageEffectUI.TakeDamageEffect();
    }
    #endregion

    #region Check if player is in darkness
    private bool IsPlayerInDarkness()
    {
        return !(flashLightController.turnedOn && flashLightController.currentEnergy > 0) && !InLightRange();
    }
    #endregion

    #region Checks if player is near any lightsource
    private bool InLightRange()
    {
        if(lights!=null)
        {
            foreach(Light light in lights) 
            {
                float distanceToPlayer = Vector3.Distance(transform.position, light.transform.position);
                if (distanceToPlayer <= light.range)
                {
                    return true;
                }
            }

        }
        return false;
    }
    #endregion
    
}

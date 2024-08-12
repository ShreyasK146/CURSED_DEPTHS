using UnityEngine;
using UnityEngine.UI;

public class DamageEffectUI : MonoBehaviour
{
    public PlayerHealthController playerHealthController;
    public UIController uiController;
    public Image damageImage;

    private void Start()
    {
        damageImage = GetComponent<Image>();
    }

    #region Take Damage and update UI when player health is more than 0
    public void TakeDamageEffect()
    {
        if (!uiController.IsGamePaused() && playerHealthController.currentHealth > 0)
        {
            Color color = damageImage.color;
            color.a += 0.001f;
            playerHealthController.currentHealth -= playerHealthController.healthDepletionRate;
            damageImage.color = color;
        }
        else if (playerHealthController.currentHealth <= 0)
        {
            Color color = damageImage.color;
            color.a = 1f;
            damageImage.color = color;
            playerHealthController.currentHealth = 0;
           
        }
    }
    #endregion

    #region Regen Health and update UI when player health is less than maxhealth
    public void RegenDamageEffect()
    {
        if (playerHealthController.currentHealth <= 0)
        {
            // Player is "dead" prevent further regeneration
            return;
        }

        if (!uiController.IsGamePaused() && playerHealthController.currentHealth < playerHealthController.maxHealth)
        {
            Color color = damageImage.color;
            color.a -= 0.0001f;
            playerHealthController.currentHealth += playerHealthController.healthRegenRate;
            damageImage.color = color;
        }
        else if (playerHealthController.currentHealth >= playerHealthController.maxHealth)
        {
            Color color = damageImage.color;
            color.a = 0f;
            damageImage.color = color;
            playerHealthController.currentHealth = 1f;
        }
    }
    #endregion

}

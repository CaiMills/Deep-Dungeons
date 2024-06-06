using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStaminaManager : MonoBehaviour
{
    [SerializeField] private Image staminaBar;
    [SerializeField] public float currentStamina = 100;
    [SerializeField] private float maxStamina = 100;
    [SerializeField] private float staminaRegenTimer = 1f;
    [SerializeField] private float staminaResetTimer;
    [SerializeField] private float staminaRegenAmount = 5f;

    /// <summary>
    /// Removes the staminaAmount from the currentStamina
    /// </summary>
    public void RemoveStamina(float staminaAmount)
    {
        currentStamina -= staminaAmount;
        staminaBar.fillAmount = currentStamina / maxStamina;
    }

    private void Update()
    {
        /// <summary>
        /// this regens stamina overtime if the currentStamina value is less then the maxStamina value
        /// </summary>
        if (currentStamina < maxStamina)
        {
            staminaResetTimer += Time.deltaTime;
            
            if (staminaResetTimer >= staminaRegenTimer)
            {
                staminaResetTimer = 0f;
                currentStamina += staminaRegenAmount;
                staminaBar.fillAmount = currentStamina / maxStamina;
            }
        }
    }
}

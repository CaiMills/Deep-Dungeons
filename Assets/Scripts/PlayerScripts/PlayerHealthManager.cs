using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] public float currentHealth = 100;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] GameObject gameOverScreen = null;

    Animator animator;
    TopDownCharacterController topDownCharacterController;

    private float gameOverFadeOutTime = 3.0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        topDownCharacterController = GetComponent<TopDownCharacterController>();
    }

    /// <summary>
    /// Removes the damageAmount from the currentHealth
    /// </summary>
    public void PlayerDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar.fillAmount = currentHealth / maxHealth;
        StartCoroutine(Hurting());
    }

    /// <summary>
    /// Adds the healingAmount from the currentHealth
    /// </summary>
    public void PlayerHealing(float healingAmount)
    {
        currentHealth += healingAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    private void Update()
    {
        #region Player Death

        if (currentHealth <= 0)
        {
            topDownCharacterController.canMove = false;
            animator.SetBool("IsDead?", true);
            gameOverScreen.SetActive(true);
            gameOverFadeOutTime -= Time.deltaTime;
        }

        if (gameOverFadeOutTime <= 0)
        {
            gameOverScreen.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        #endregion
    }

    public IEnumerator Hurting()
    {
        animator.SetBool("IsTakingDamage?", true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("IsTakingDamage?", false);
    }
}

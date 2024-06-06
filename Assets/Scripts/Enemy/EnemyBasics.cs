using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasics : MonoBehaviour
{
    Animator animator;
    PlayerHealthManager healthManager;
    TopDownCharacterController topDownCharacterController;

    [SerializeField] GameObject healingStonePrefab;

    float currentHealth;
    [SerializeField] float maxHealth;
    [SerializeField] float attackDamage;

    private float collisionDamage = 10;
    private bool isCollided = false;
    private float collisionDamageTimer = 1;
    private float collisionDamageResetTimer;

    private void Awake()
    {
        healthManager = GameObject.Find("Player").GetComponent<PlayerHealthManager>();
        topDownCharacterController = GameObject.Find("Player").GetComponent<TopDownCharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void FixedUpdate()
    {
        /// <summary>
        /// Starts a constant timer whenever the player is collided with the enemy, applying damage each second
        /// </summary>
        if (isCollided == true)
        {
            if (healthManager.currentHealth > 0)
            {
                collisionDamageResetTimer += Time.deltaTime;

                if (collisionDamageResetTimer >= collisionDamageTimer)
                {
                    collisionDamageResetTimer = 0;
                    healthManager.PlayerDamage(collisionDamage);
                }
            }
        }
    }

    /// <summary>
    /// This is a calculation that allows the enemy to take damage
    /// </summary>
    public void EnemyReceivingDamage(float damage)
    {
        currentHealth -= damage;
        StartCoroutine(EnemyHurting());

        if (currentHealth <= 0 && gameObject != null)
        {
            animator.SetBool("IsAttacking", false);
            Instantiate(healingStonePrefab, transform.position, Quaternion.identity);
            Destroy(transform.root.gameObject);
        }
    }

    /// <summary>
    /// This hurts the player if they collide with the enemy
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && (topDownCharacterController.isRolling == false))
        {
            healthManager.PlayerDamage(collisionDamage);
            isCollided = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && (topDownCharacterController.isRolling == false))
        {
            isCollided = false;
        }
    }

    public IEnumerator EnemyHurting()
    {
        animator.SetBool("IsTakingDamage?", true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("IsTakingDamage?", false);
    }
}

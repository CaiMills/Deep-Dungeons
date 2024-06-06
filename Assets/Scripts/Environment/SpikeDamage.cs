using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    PlayerHealthManager healthManager;
    TopDownCharacterController topDownCharacterController;

    private float spikeDamage = 10f;
    private bool isCollided = false;
    private float collisionDamageTimer;
    private float collisionDamageResetTimer = 1;

    private void Start()
    {
        healthManager = GameObject.Find("Player").GetComponent<PlayerHealthManager>();
        topDownCharacterController = GameObject.Find("Player").GetComponent<TopDownCharacterController>();
    }

    private void FixedUpdate()
    {
        if (isCollided == true)
        {
            if(healthManager.currentHealth > 0)
            {
                collisionDamageTimer += Time.deltaTime;

                if (collisionDamageTimer >= collisionDamageResetTimer)
                {
                    collisionDamageTimer = 0;
                    healthManager.PlayerDamage(spikeDamage);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && (topDownCharacterController.isRolling == false))
        {
            healthManager.PlayerDamage(spikeDamage);
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
}

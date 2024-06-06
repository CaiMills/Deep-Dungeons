using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingStone : MonoBehaviour
{
    PlayerHealthManager healthManager;

    private float healingAmount = 10;

    private void Awake()
    {
        healthManager = GameObject.Find("Player").GetComponent<PlayerHealthManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            healthManager.PlayerHealing(healingAmount);
            Destroy(gameObject);
        }
    }
}

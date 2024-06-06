using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitDamage : MonoBehaviour
{
    PlayerHealthManager playerHealthManager;
    private float pitDamage = 100000.0f;

    private void Start()
    {
        playerHealthManager = GameObject.Find("Player").GetComponent<PlayerHealthManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerHealthManager.PlayerDamage(pitDamage);
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}

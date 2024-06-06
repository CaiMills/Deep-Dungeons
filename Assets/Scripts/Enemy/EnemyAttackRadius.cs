using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRadius : MonoBehaviour
{
    public ChaserEnemyAI enemyAI;
    TopDownCharacterController topDownCharacterController;

    private float Damage;

    private void Awake()
    {
        /*enemyAI = GameObject.Find("Chaser Enemy").GetComponent<ChaserEnemyAI>();*/
        topDownCharacterController = GameObject.Find("Player").GetComponent<TopDownCharacterController>();
    }

    private void Update()
    {
        Damage = enemyAI.enemyAttackDamage;
    }

    /// <summary>
    /// This checks if the Attack Radius child hits an enemy, and if it does, to apply damage to them
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHealthManager>() && (topDownCharacterController.isRolling == false))
        {
            PlayerHealthManager healthManager = collision.GetComponent<PlayerHealthManager>();
            healthManager.PlayerDamage(Damage);
        }
    }
}

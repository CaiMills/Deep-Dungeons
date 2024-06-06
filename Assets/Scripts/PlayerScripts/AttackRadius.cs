using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRadius : MonoBehaviour
{
    PlayerAttackingManager attackingManager;

    private float Damage;

    private void Awake()
    {
        attackingManager = GameObject.Find("Player").GetComponent<PlayerAttackingManager>();
    }

    private void Update()
    {
        Damage = attackingManager.attackDamage;
    }

    /// <summary>
    /// This checks if the Attack Radius child hits an enemy, and if it does, to apply damage to them
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyBasics>() != null)
        {
            EnemyBasics enemyBasics = collision.GetComponent<EnemyBasics>();
            enemyBasics.EnemyReceivingDamage(Damage);
        }
    }
}

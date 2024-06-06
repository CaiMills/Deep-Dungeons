using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaserEnemyAI : MonoBehaviour
{
    GameObject player;
    NavMeshAgent Agent;
    Animator animator;

    private float enemyChaseDistance = 6;

    public GameObject attackArea;

    public float enemyAttackDamage = 1;
    private float enemyAttackDistance = 2;
    private bool isAttacking = false;
    private float attackCooldown;
    private float attackSpeed = 3f;

    private Vector3 lastMovedDirection;

    void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        /*attackArea = this.gameObject.transform.GetChild(1).gameObject;*/
        animator = GameObject.Find("Chaser Enemy/Enemy Sprite").GetComponent<Animator>();
    }

    private void Update()
    {
        if (animator == null) 
        { 
            animator = GetComponentInChildren<Animator>(); 
        }
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        #region Movement Animation
        // check if there is some movement direction, if there is something, then set animator flags and make speed = 1
        if (Agent.velocity.magnitude != 0)
        {
            animator.SetFloat("Horizontal", Agent.velocity.x);
            animator.SetFloat("Vertical", Agent.velocity.y);
            animator.SetFloat("Speed", Agent.velocity.magnitude);
        }
        else
        {
            //Update the animator too, and return
            animator.SetFloat("Speed", 0);
        }
        #endregion

        if (distance <= enemyChaseDistance)
        {
            PlayerChasing();
        }

        if (distance > enemyChaseDistance)
        {
            StopChasing();
        }

        if (distance <= enemyAttackDistance && attackCooldown == 0)
        {
            Attack();
        }

        if (isAttacking)
        {
            attackCooldown += Time.deltaTime;

            if (attackCooldown >= attackSpeed)
            {
                attackCooldown = 0;
                animator.SetBool("IsAttacking?", false);
                attackArea.SetActive(false);

                isAttacking = false;
            }
        }
    }

    void PlayerChasing()
    {
        Agent.SetDestination(player.transform.position);
    }

    private void StopChasing()
    {
        Agent.SetDestination(transform.position);
    }

    private void Attack()
    {
        isAttacking = true;
        animator.SetBool("IsAttacking?", true);
        attackArea.SetActive(true);
    }
}

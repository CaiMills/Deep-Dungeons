using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArcherEnemyAI : MonoBehaviour
{
    GameObject player;
    NavMeshAgent Agent;
    public EnemyShooting enemyShooting;
    Animator animator;

    private bool isAttacking = false;

    [SerializeField] private float enemyDistanceRun;

    void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
/*      enemyShooting = GameObject.Find("Archer Enemy/Enemy Sprite").GetComponent<EnemyShooting>();*/
        animator = GameObject.Find("Archer Enemy/Enemy Sprite").GetComponent<Animator>();
    }

    void Update()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        if (enemyShooting == null)
        {
            enemyShooting = GetComponentInChildren<EnemyShooting>();
        }

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


        if (distance < enemyDistanceRun)
        {
            RunningAway();
        }

        if (distance >= enemyDistanceRun)
        {
            StopRunning();
        }

        if (isAttacking == true)
        {
            enemyShooting.Attack();
        }
    }

    /// <summary>
    /// Set the nav agents destination to the opposite of the players current position
    /// </summary>
    private void RunningAway()
    {
        isAttacking = false;

        Vector3 directionToPlayer = transform.position - player.transform.position;

        Vector3 newPosition = transform.position + directionToPlayer;

        Agent.SetDestination(newPosition);
    }

    /// <summary>
    /// Sets the nav agents destination to where this gameobject
    /// </summary>
    private void StopRunning()
    {
        isAttacking = true;
        Agent.SetDestination(transform.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject player;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] Transform projectilePosition;

    private float projectileTimer;
    [SerializeField] private float projectileCooldown;
    [SerializeField] private float distanceMax;

    private void Start()
    {
        /*player = GameObject.FindGameObjectWithTag("Player");*/
    }

    public void Attack()
    {
        //this means that I can set a distance the player needs to be within before the enemy starts shooting
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < 7.5)
        {
            projectileTimer += Time.deltaTime;

            if (projectileTimer > projectileCooldown)
            {
                projectileTimer = 0;
                ShootProjectile();
            }
        }
    }

    private void ShootProjectile()
    {
        Instantiate(projectilePrefab, projectilePosition.position, Quaternion.identity);
    }
}

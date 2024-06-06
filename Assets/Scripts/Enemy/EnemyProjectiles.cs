using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectiles : MonoBehaviour
{
    PlayerHealthManager healthManager;
    TopDownCharacterController topDownCharacterController;

    public GameObject player;
    Rigidbody2D rb;

    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileSpeedDropOff;
    [SerializeField] private float projectileSpeedMin;
    [SerializeField] private float projectileDamage;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        healthManager = GameObject.Find("Player").GetComponent<PlayerHealthManager>();
        topDownCharacterController = GameObject.Find("Player").GetComponent<TopDownCharacterController>();
    }

    private void Start()
    {
        /// <summary>
        /// Applys force onto the projectile in the direction the player is at when the projectile is spawned
        /// </summary>
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2 (direction.x, direction.y).normalized * projectileSpeed;

        /// <summary>
        /// The rotates the projectile towards the player
        /// </summary>
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 270, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        //causes the projectile to lose speed overtime
        projectileSpeed -= projectileSpeed * projectileSpeedDropOff * Time.deltaTime;

        if (projectileSpeed < projectileSpeedMin)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && (topDownCharacterController.isRolling == false))
        {
            healthManager.PlayerDamage(projectileDamage);
            Destroy(gameObject);
        }

    }
}

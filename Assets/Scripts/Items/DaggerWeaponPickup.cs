using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerWeaponPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerAttackingManager>() != null)
        {
            PlayerAttackingManager playerAttackingManager = collision.GetComponent<PlayerAttackingManager>();
            playerAttackingManager.currentWeapon = PlayerAttackingManager.CurrentWeapon.Dagger;

            Destroy(gameObject);
        }
    }
}

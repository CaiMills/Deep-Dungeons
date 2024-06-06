using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerAttackingManager : MonoBehaviour
{
    GameObject weaponRadiusManager;
    PlayerStaminaManager staminaManager;
    public GameObject attackArea = default;
    Animator animator;

    public bool isAttacking = false;

    public enum CurrentWeapon
    {
        Sword,
        Greatsword,
        Dagger,
    }
    public CurrentWeapon currentWeapon;

    [SerializeField] private string[] equippedWeaponName = { "Sword", "Greatsword", "Dagger" };
    [SerializeField] public float attackDamage;
    [SerializeField] private float attackSpeed;
    private float attackCooldown;
    private float staminaCost;

    private void Awake()
    {
        weaponRadiusManager = GameObject.Find("Player/Weapon Radius Manager");
        //this finds the Weapon Radius Manager, which is a child of the Player Object, which is what the / stands for
        staminaManager = GetComponent<PlayerStaminaManager>();

        attackArea = weaponRadiusManager.transform.GetChild(1).gameObject;
        animator = GetComponent<Animator>();
        currentWeapon = CurrentWeapon.Sword;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && staminaManager.currentStamina > 0 && attackCooldown == 0)
        {
            Attack();
            staminaManager.RemoveStamina(staminaCost);

            #region Attack Animations
            animator.SetTrigger("IsAttacking?");

            switch (currentWeapon)
            {
                case CurrentWeapon.Sword:
                    {
                        animator.SetBool("IsSwordEquipped?", true);
                    }
                    break;
                case CurrentWeapon.Greatsword:
                    {
                        animator.SetBool("IsGreatswordEquipped?", true);
                    }
                    break;
                case CurrentWeapon.Dagger:
                    {
                        animator.SetBool("IsDaggerEquipped?", true);
                    }
                    break;
            }
            #endregion
        }

        /// <summary>
        /// When isAttacking is true, it begins the attack process, including a cooldown and activating the attackArea, all of which is customisable for each weapon
        /// </summary>
        if (isAttacking)
        {
            attackCooldown += Time.deltaTime;

            if (attackCooldown >= attackSpeed)
            {
                attackCooldown = 0;
                attackArea.SetActive(false);

                #region Attack Animations
                switch (currentWeapon)
                {
                    case CurrentWeapon.Sword:
                        {
                            animator.SetBool("IsSwordEquipped?", false);
                        }
                        break;
                    case CurrentWeapon.Greatsword:
                        {
                            animator.SetBool("IsGreatswordEquipped?", false);
                        }
                        break;
                    case CurrentWeapon.Dagger:
                        {
                            animator.SetBool("IsDaggerEquipped?", false);
                        }
                        break;
                }

                animator.ResetTrigger("IsAttacking?");
                #endregion

                isAttacking = false;
            }
        }
    }

    private void FixedUpdate()
    {
        /// <summary>
        /// This sets the stats of the weapon depending on the currently equipped weapon (default being the sword)
        /// </summary>
        switch (currentWeapon)
        {
            case CurrentWeapon.Sword:
                {
                    attackArea = weaponRadiusManager.transform.GetChild(1).gameObject;
                    attackDamage = 20f;
                    attackSpeed = 0.3f;
                    staminaCost = 10f;
                }
                break;
            case CurrentWeapon.Greatsword:
                {
                    attackArea = weaponRadiusManager.transform.GetChild(2).gameObject;
                    attackDamage = 40f;
                    attackSpeed = 0.4f;
                    staminaCost = 20f;
                }
                break;
            case CurrentWeapon.Dagger:
                {
                    attackArea = weaponRadiusManager.transform.GetChild(0).gameObject;
                    attackDamage = 5f;
                    attackSpeed = 0.15f;
                    staminaCost = 5f;
                }
                break;
        }
    }

        private void Attack()
        {
            isAttacking = true;
            attackArea.SetActive(true);
        }
    }
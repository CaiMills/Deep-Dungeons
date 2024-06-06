using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class TopDownCharacterController : MonoBehaviour
{
    #region Framework Stuff
    //Reference to attached animator
    private Animator animator;

    //Reference to attached rigidbody 2D
    private Rigidbody2D rb;

    //The direction the player is moving in
    public Vector2 playerDirection;

    //The speed at which they're moving
    private float playerSpeed = 1f;

    [Header("Movement parameters")]
    //The maximum speed the player can move
    [SerializeField] private float playerMaxSpeed = 150f;
    #endregion

    /// <summary>
    /// This enum allows for certain actions to happen depending on the players state, with enum being easily expandible allowing for any number of states.
    /// </summary>
    public enum PlayerState
    {
        Normal,
        Rolling,
    }
    public PlayerState playerState;

    [Header("Rolling parameters")]
    private float rollSpeed;
    [SerializeField] private float rollSpeedDropOff = 5f;
    [SerializeField] private float rollSpeedMin = 10f;
    [SerializeField] private float rollStaminaCost = 20f;
    [SerializeField] private float rollCooldown = 1f;
    private float lastTimeRollWasUsed;
    private Vector3 rollDirection;
    private Vector3 lastMovedDirection;
    public bool isRolling = false;

    [Header("Other")]
    PlayerStaminaManager staminaManager;

    public Transform attackRadius;
    bool isWalking;

    public bool canMove;

    /// <summary>
    /// When the script first initialises this gets called, use this for grabbing componenets
    /// </summary>
    private void Awake()
    {
        //Get the attached components so we can use them later
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        staminaManager = GetComponent<PlayerStaminaManager>();

        canMove = true;

        playerState = PlayerState.Normal;
    }

    /// <summary>
    /// When the update loop is called, it runs every frame, ca run more or less frequently depending on performance. Used to catch changes in variables or input.
    /// </summary>
    private void Update()
    {
        switch (playerState)
        {
            case PlayerState.Normal:

                #region Player Movement
                // read input from WASD keys
                playerDirection.x = Input.GetAxis("Horizontal");
                playerDirection.y = Input.GetAxis("Vertical");

                if (playerDirection != Vector2.zero && canMove == true)
                {
                    lastMovedDirection = playerDirection;
                    isWalking = true;

                    /// <summary>
                    /// This rotates the Attack Radius child with the player
                    /// </summary>
                    Vector3 vector3 = Vector3.left * lastMovedDirection.x + Vector3.down * lastMovedDirection.y;
                    attackRadius.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
                }

                else if (playerDirection.x == 0f || playerDirection.y == 0f)
                {
                    isWalking = false;
                }

                    #region Movement Animation
                    // check if there is some movement direction, if there is something, then set animator flags and make speed = 1
                    if (playerDirection.magnitude != 0)
                {
                    animator.SetFloat("Horizontal", playerDirection.x);
                    animator.SetFloat("Vertical", playerDirection.y);
                    animator.SetFloat("Speed", playerDirection.magnitude);

                    //And set the speed to 1, so they move!
                    playerSpeed = 1f;
                }
                else
                {
                    //Was the input just cancelled (released)? If so, set
                    //speed to 0
                    playerSpeed = 0f;

                    //Update the animator too, and return
                    animator.SetFloat("Speed", 0);
                }
                #endregion

                #endregion

                #region Dodge Roll
                /// <summary>
                /// This initiates the players dodge roll ability
                /// it only works if 1) the space button is pressed, 2) the roll isnt currently on cooldown, and 3) the current stamina left is more than 0
                /// </summary>
                if (Input.GetKeyDown(KeyCode.Space) && Time.time > lastTimeRollWasUsed + rollCooldown && staminaManager.currentStamina > 0)
                {
                    rollDirection = lastMovedDirection;
                    rollSpeed = 20f;
                    playerState = PlayerState.Rolling;
                    staminaManager.RemoveStamina(rollStaminaCost);
                    lastTimeRollWasUsed = Time.time;
                    isRolling = true;

                    #region Rolling Animation
                    animator.SetFloat("Horizontal", rollDirection.x);
                    animator.SetFloat("Vertical", rollDirection.y);
                    animator.SetTrigger("IsRolling?");
                }
                else
                {
                    animator.ResetTrigger("IsRolling?");
                    #endregion
                }

                break;
            case PlayerState.Rolling:
                //decreases rollSpeed overtime
                rollSpeed -= rollSpeed * rollSpeedDropOff * Time.deltaTime;

                if (rollSpeed < rollSpeedMin)
                {
                    playerState = PlayerState.Normal;
                    isRolling = false;
                }
                break;
                #endregion
        }
    }

    /// <summary>
    /// When a fixed update loop is called, it runs at a constant rate, regardless of pc perfornamce so physics can be calculated properly
    /// </summary>
    private void FixedUpdate()
    {
        switch (playerState)
        {
            case PlayerState.Normal:

                //Set the velocity to the direction they're moving in, multiplied
                //by the speed they're moving
                rb.velocity = playerDirection * (playerSpeed * playerMaxSpeed) * Time.fixedDeltaTime;

                /// <summary>
                /// This rotates the Attack Radius child with the player
                /// </summary>
                if (isWalking)
                {
                    Vector3 vector3 = Vector3.left * playerDirection.x + Vector3.down * playerDirection.y;
                    attackRadius.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
                }

                break;
            case PlayerState.Rolling:
                rb.velocity = rollDirection * rollSpeed;
                break;
        }
    }
}

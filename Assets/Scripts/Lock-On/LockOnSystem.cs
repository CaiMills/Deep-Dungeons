/*using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LockOnSystem : MonoBehaviour
{
    public GameObject[] validEnemies; //keeps a list of every enemy in the scene
    public GameObject closestEnemy; //this keeps track of which enemy is the closest
    public GameObject selectedEnemy; //this is used to hold the current closestEnemy value at the time the lock-on is started
    public GameObject lockOnTarget; //this is a variable used to create a prefab of the target lock on indicator

    Animator animator;

    [SerializeField] public GameObject targetPrefab;
    private bool isLockedOn = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// When the lock on button is pressed, it will do all of the following
    /// </summary>
    private void LockOn()
    {
        if (Input.GetKeyDown(KeyCode.R) && validEnemies.Length > 0)
        //when the button is pressed, and there is a valid enemy on screen
        {
            if (validEnemies.Length <= 0)
            {
                DestroyImmediate(lockOnTarget, true);
                //deletes he prefab clone
                isLockedOn = false;
                animator.ResetTrigger("IsLockedOn?");
            }

            if (isLockedOn == true)
            {
                DestroyImmediate(lockOnTarget, true);
                isLockedOn = false;
                animator.ResetTrigger("IsLockedOn?");
            }

            else
            {
                selectedEnemy = closestEnemy;
                //makes the selectedEnemy variable be the same as the current closestEnemy
                //this is so the target reticle can be moved, as it needs to be moved each frame, but i can't create the prefab on the closestEnemy variable
                //as its constantly changing as well, meaning that the target would automatically switch to the new closest enemy, so I use this variable as the
                //equivelent of a static closestEnemy variable
                isLockedOn = true;

            }
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        validEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        //if the object has the tag "Enemy" then its tracked by the validEnemies variable (more tags can be added at anytime)
    }

    // Update is called once per frame
    private void Update()
    {
        closestEnemy = ClosestEnemy();
        //this constantly updates what the closest enemy is by running the script

        LockOn();
        if (isLockedOn == true)
        {
            DestroyImmediate(lockOnTarget, true);
            lockOnTarget = Instantiate(targetPrefab, selectedEnemy.transform.position, Quaternion.identity);
            //creates the m_targetPrefab on the enemies position
            animator.SetTrigger("IsLockedOn?");
        }
    }

    /// <summary>
    /// For every enemy on the screen, the code will compare the distances of each of the enemies with the current lowest, and if the code finds an enemy thats closer then the
    /// current closest, then it takes its place as the closest
    /// </summary>
    GameObject ClosestEnemy()
    {
        GameObject closestHere = gameObject;
        float leastDistance = Mathf.Infinity;
        //Mathf.Infinity means that the current distance is the largest it could possibly be, so anything below that will be considered closer

        foreach (var enemy in validEnemies)
        {
            float distanceHere = Vector3.Distance(transform.position, enemy.transform.position);
            //this gets the distance from the enemy

            if (distanceHere < leastDistance)
            {
                leastDistance = distanceHere;
                closestHere = enemy;
            }
        }
        return closestHere;
    }
}
*/
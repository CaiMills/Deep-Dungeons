using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    /// <summary>
    /// This simply spawns a enemy prefab (chosen via the editor) on the objects location
    /// </summary>
    private void Start()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}

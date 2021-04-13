using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnCoin : MonoBehaviour
{
    public GameObject coinPrefab;
    EnemyMovement enemyMovement;

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public void SpawnCoin()
    {
        GameObject coinSpawn = Instantiate(coinPrefab);
        coinSpawn.transform.position = enemyMovement.transform.position;
    }
}

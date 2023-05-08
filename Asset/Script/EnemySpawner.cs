using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnAreaWidth = 10f;
    public float spawnAreaHeight = 10f;
    
    private int numberOfEnemies;

    private void Start()
    {
        switch (GameManager.Instance.currentDifficulty)
        {
            case GameManager.Difficulty.Easy:
                numberOfEnemies = 5;
                break;
            case GameManager.Difficulty.Normal:
                numberOfEnemies = 15;
                break;
            case GameManager.Difficulty.Hard:
                numberOfEnemies = 30;
                break;
        }

        for (int i = 0; i < numberOfEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        float randomX = Random.Range(transform.position.x - spawnAreaWidth / 2, transform.position.x + spawnAreaWidth / 2);
        float randomY = Random.Range(transform.position.y - spawnAreaHeight / 2, transform.position.y + spawnAreaHeight / 2);
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}

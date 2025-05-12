using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private EnemyPool mushroomPool;
    [SerializeField] private EnemyPool fEyePool;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float timeBetweenSpawns = 5f;

    void Start()
    {
        StartCoroutine(SpawnEnemyCoroutine());
    }
    private IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);

            int enemyType = Random.Range(0, 2);
            GameObject enemy;
            Transform spawnPoint;

            if (enemyType == 0)
            {
                enemy = mushroomPool.GetPooledEnemy();
                spawnPoint = spawnPoints[0];
            }
            else
            {
                enemy = fEyePool.GetPooledEnemy();
                spawnPoint = spawnPoints[1];
            }

            enemy.transform.position = spawnPoint.position;
            enemy.transform.rotation = Quaternion.identity;
        }
    }
}

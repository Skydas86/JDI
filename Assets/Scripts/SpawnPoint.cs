using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject[] enemis;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float timeBetweenSpawns=5f;
    void Start()
    {
        StartCoroutine(SpawnEnemyCoroutine());
    }
    private IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            int index = Random.Range(0, enemis.Length);
            GameObject enemy = enemis[index];
            Transform spawnPoint = spawnPoints[index];
            Instantiate(enemy, spawnPoint.position, Quaternion.identity);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject checkPointPrefab;

    [SerializeField] int checkPointSpawnDelay = 10;

    [SerializeField] float spawnRadius = 10;

    [SerializeField] GameObject[] powerUpPrefab;

    [SerializeField] int powerUpSpawnDelay = 12;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCheckPointRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

    IEnumerator SpawnCheckPointRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(checkPointSpawnDelay);

            UnityEngine.Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;
            Instantiate(checkPointPrefab, randomPosition, Quaternion.identity);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(powerUpSpawnDelay);

            UnityEngine.Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;

            int random = Random.Range(0, powerUpPrefab.Length);
            Instantiate(powerUpPrefab[random], randomPosition, Quaternion.identity);
        }
    }
}

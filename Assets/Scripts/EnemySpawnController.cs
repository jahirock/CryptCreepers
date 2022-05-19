using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnNewEnemy());
    }

    IEnumerator SpawnNewEnemy()
    {
        while(true)
        {
            //Esperar 3 segundos antes de crear un nuevo enemigo
            yield return new WaitForSeconds(3);

            //Spawnea enemigos dependiendo la probabilidad.
            float random = Random.Range(0.0F, 1.0F);
            if(random < GameManager.Instance.difficulty * 0.1)
            {
                Instantiate(enemyPrefab[0]); //Enemigo fuerte
            }
            else 
            {
                Instantiate(enemyPrefab[1]); //Enemigo debil
            }
        }
    }
}

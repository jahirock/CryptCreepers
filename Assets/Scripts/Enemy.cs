using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform player;

    [SerializeField] int health = 1;

    [SerializeField] float speed = 1;

    [SerializeField] int scorePoint = 100;
    
    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        //Se obtienen los objetos que tienen el tag SpawnPoint
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        //Se selecciona un spawn aleatorio
        int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        //Inicia la posicion del enemigo
        transform.position = spawnPoints[randomSpawnPoint].transform.position;
    }

    void Update()
    {
        if(player != null)
        {
            UnityEngine.Vector2 direction = player.position - transform.position;
            //Se normaliza el vector de la direccion para que siempre tenga la misma velocidad.
            transform.position += (UnityEngine.Vector3)direction.normalized * Time.deltaTime * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TrakeDamage();
        }
    }

    public void TrakeDamage()
    {
        health--;

        if(health <= 0)
        {
            GameManager.Instance.Score += scorePoint;
            Destroy(gameObject);
        }
    }
}

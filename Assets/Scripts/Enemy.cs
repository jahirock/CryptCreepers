using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform player;

    [SerializeField] int health = 1;

    [SerializeField] float speed = 1;
    
    void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        UnityEngine.Vector2 direction = player.position - transform.position;
        //Se normaliza el vector de la direccion para que siempre tenga la misma velocidad.
        transform.position += (UnityEngine.Vector3)direction.normalized * Time.deltaTime * speed;
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
            Destroy(gameObject);
        }
    }
}

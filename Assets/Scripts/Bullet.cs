using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 5;

    [SerializeField] int health = 3;
    public bool powerShot = false;

    void Start()
    {
        Destroy(gameObject, 5);
    }

    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TrakeDamage();
            if(!powerShot)
            {
                Destroy(gameObject);
            }

            health--;
            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

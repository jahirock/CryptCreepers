using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    UnityEngine.Vector3 moveDirection;
    
    float h;
    float v;

    //SerializeField para que aparezca la variable en el editor de unity
    [SerializeField] float speed = 3;

    [SerializeField] int health = 10;

    [SerializeField] Transform aim;

    [SerializeField] Camera cam;

    UnityEngine.Vector2 facingDirection;

    [SerializeField] Transform bulletPrefab;

    bool gunLoaded = true;

    [SerializeField] float fireRate = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Obtiene los botones de movimiento WASD y flechas de mov (-1 a 1)
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        moveDirection.x = h;
        moveDirection.y = v;

        //Se cambia la posicion normalizando con deltaTime
        transform.position += moveDirection * Time.deltaTime * speed;


        //Movimiento de la mira
        //Transforma la posicion del mouse en pantalla a la posicion en el mundo y le resta la posicion del jugador para obtener la direccion a la que esta mirando
        facingDirection = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        aim.position = transform.position + (UnityEngine.Vector3)facingDirection.normalized;

        //Si click izquierdo
        if(Input.GetMouseButton(0) && gunLoaded)
        {
            gunLoaded = false;
            //Se obtiene el angulo 
            float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
            //Se crea para pasarselo al instantiate
            Quaternion targetRotation = Quaternion.AngleAxis(angle, UnityEngine.Vector3.forward);

            //Crea la bala
            Instantiate(bulletPrefab, transform.position, targetRotation);

            //Para que deje disparar de nuevo despues de un tiempo
            StartCoroutine(ReloadGun());
        }
    }

    public void TrakeDamage()
    {
        health--;

        if(health <= 0)
        {
            print("Game Over");
            //Desactiva el objeto del jugador
            gameObject.SetActive(false);
        }
    }

    IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(1 / fireRate);
        gunLoaded = true;
    }
}

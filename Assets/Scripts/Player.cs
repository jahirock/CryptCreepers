using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    UnityEngine.Vector3 moveDirection;
    
    float h;
    float v;
    bool gunLoaded = true;
    bool powerShotEnabled = false;
    UnityEngine.Vector2 facingDirection;
    CameraController camController;

    //SerializeField para que aparezca la variable en el editor de unity
    public float speed = 3;

    [SerializeField] int health = 10;

    [SerializeField] Transform aim;

    [SerializeField] Camera cam;

    [SerializeField] Transform bulletPrefab;

    [SerializeField] float fireRate = 1;

    [SerializeField] float powerShotDelay = 3;

    [SerializeField] bool invulnerable = false;

    [SerializeField] float invulnerableTime = 3;

    [SerializeField] Animator anim;

    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] float blinkRate = 1;

    [SerializeField] AudioClip itemClip;

    public int Health {
        get => health;
        set {
            health = value;
            UIManager.Instance.UpdateUIHealth(health);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = health;
        camController = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        ReadInput();
    
        //Se cambia la posicion normalizando con deltaTime
        transform.position += moveDirection * Time.deltaTime * speed;

        //Movimiento de la mira
        //Transforma la posicion del mouse en pantalla a la posicion en el mundo y le resta la posicion del jugador para obtener la direccion a la que esta mirando
        facingDirection = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        aim.position = transform.position + (UnityEngine.Vector3)facingDirection.normalized;

        //Si click izquierdo
        if(Input.GetMouseButton(0) && gunLoaded && health > 0)
        {
            Shoot();
        }

        //Se envia la velocidad de movimiento al animador para que cambie de animacion cuando este corriendo
        anim.SetFloat("Speed", moveDirection.magnitude);

        UpdatePlayerGraphics();
    }

    void ReadInput()
    {
        //Obtiene los botones de movimiento WASD y flechas de mov (-1 a 1)
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        moveDirection.x = h;
        moveDirection.y = v;
    }

    void Shoot()
    {
        gunLoaded = false;
        //Se obtiene el angulo 
        float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
        //Se crea para pasarselo al instantiate
        Quaternion targetRotation = Quaternion.AngleAxis(angle, UnityEngine.Vector3.forward);

        //Crea la bala
        Transform bulletClone = Instantiate(bulletPrefab, transform.position, targetRotation);
        
        //Si esta activo el powerShot se lo indica a la bala
        if(powerShotEnabled) {
            bulletClone.GetComponent<Bullet>().powerShot = true;
        }
        //Para que deje disparar de nuevo despues de un tiempo
        StartCoroutine(ReloadGun());
    }

    void UpdatePlayerGraphics()
    {
        //Segun la posicion de la mira con respecto al jugador se voltea el sprite.
        if(aim.position.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else if (aim.position.x < transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("PowerUp"))
        {
            //Reproducir el audio de los items
            AudioSource.PlayClipAtPoint(itemClip, transform.position);

            switch(other.GetComponent<PowerUp>().powerUpType)
            {
                case PowerUp.PowerUpType.FireRateIncrease:
                    //Incrementar cadencia del disparo
                    fireRate++;
                    break;
                case PowerUp.PowerUpType.PowerShot:
                    //Activar el powerShot
                    powerShotEnabled = true;
                    StartCoroutine(PowerShotEnd());
                    break;
            }

            Destroy(other.gameObject, 0.1F);
        }
    }

    public void TrakeDamage()
    {
        if(invulnerable) return;

        Health--;

        invulnerable = true;

        camController.Shake();

        StartCoroutine(MakeVulnerableAgain());

        if(Health <= 0)
        {
            print("Game Over");
            GameManager.Instance.gameOver = true;
            UIManager.Instance.ShowGameOverScreen();
            //Desactiva el objeto del jugador
            //gameObject.SetActive(false);
        }
    }

    IEnumerator MakeVulnerableAgain()
    {
        StartCoroutine(BlinkRoutine());
        yield return new WaitForSeconds(invulnerableTime);
        invulnerable = false;
    }

    IEnumerator BlinkRoutine()
    {
        int t = 10;
        while(t > 0)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(t * blinkRate);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(t * blinkRate);
            t--;
        }
    }

    IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(1 / fireRate);
        gunLoaded = true;
    }

    IEnumerator PowerShotEnd()
    {
        yield return new WaitForSeconds(powerShotDelay);
        powerShotEnabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHP; // maksymalne hp jakie moze miec gracz
    public int damageMax;


    private int hp; // hp które ma gracz aktualnie

    private bool isPlayerAlive = true;

    public float movementSpeed;
    public float attackSpeed; // attackSpeed = 3 to znaczy ze na 1 sekunde wykonamy 3 ataki

    private float _attackSpeedTimer;

    public GameObject explosion;
    public GameObject takeDamageExplosion;

    public GameObject bullet;
    public Transform shootPosition;

    public Sprite mainSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    public SpriteRenderer spriteRenderer;
    void Start()
    {
        _attackSpeedTimer = 0;
        hp = maxHP;
    }
    void Update()
    {
        if(isPlayerAlive) // isPlayerAlive == true
        {
            Shooting();
            Movement();
        }

        if(hp <= 0 && isPlayerAlive)
        {
            OnDeath();
        }
    }
    public void Movement()
    {
        spriteRenderer.sprite = mainSprite;
        if (Input.GetKey(KeyCode.W)) //kiedy gracz wcisnie W, wykonaj te polecenia
        {
            Vector2 newPosition; // nowa pozycja
            // transform.position.x to aktualna pozycja x gracza
            // transform.position.y to aktualna pozycja y gracza
            // transform.position to aktualna pozycja gracza
            newPosition.x = transform.position.x;
            newPosition.y = transform.position.y + movementSpeed * Time.deltaTime;
            transform.position = newPosition;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector2 newPosition;
            newPosition.x = transform.position.x;
            newPosition.y = transform.position.y - movementSpeed * Time.deltaTime;
            transform.position = newPosition;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector2 newPosition;
            newPosition.x = transform.position.x - movementSpeed * Time.deltaTime; ;
            newPosition.y = transform.position.y;
            transform.position = newPosition;

            spriteRenderer.sprite = leftSprite;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector2 newPosition;
            newPosition.x = transform.position.x + movementSpeed * Time.deltaTime;
            newPosition.y = transform.position.y;
            transform.position = newPosition;

            spriteRenderer.sprite = rightSprite;
        }
    }
    public void Shooting()
    {
        if (_attackSpeedTimer < 1 / attackSpeed) // 1 / attackSpeed = 1 / 2 = 0.5 
        {
            _attackSpeedTimer += Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space) && _attackSpeedTimer >= 1 / attackSpeed) // 1 / attackSpeed = 1 / 2 = 0.5
        {
            _attackSpeedTimer = 0;
            GameObject bulletClone = bullet;
            Instantiate(bulletClone, shootPosition.position, Quaternion.identity); // funkcja pojawiajaca rzeczy

            Bullet bulletScript = bulletClone.GetComponent<Bullet>();
            bulletScript.bulletDamage = damageMax;
        }
    }
    public void OnDeath() // void = pustka 
    {
        SpawnParticles(explosion, 1f);
        isPlayerAlive = false;
        spriteRenderer.enabled = false;
    }
    public void TakeDamage(int damage)
    {
        SpawnParticles(takeDamageExplosion, 2f);
        hp -= damage;
    }
    public void SpawnParticles(GameObject particles, float time)
    {
        GameObject cloneObject = particles;
        cloneObject = Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(cloneObject, time);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyBullet")
        {
            TakeDamage(1);
        }
        else if (collision.gameObject.tag == "Asteroid")
        {
            Asteroid asteroidScript;
            asteroidScript = collision.gameObject.GetComponent<Asteroid>();
            TakeDamage(asteroidScript.damage);
            asteroidScript.Die();
        }
    }
}

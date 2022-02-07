using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed;
    public float attackSpeed; // attackSpeed = 3 to znaczy ze na 1 sekunde wykonamy 3 ataki

    private float _attackSpeedTimer;

    public GameObject bullet;
    public Transform shootPosition;

    public Sprite mainSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    public SpriteRenderer spriteRenderer;
    void Start()
    {
        _attackSpeedTimer = 0;
    }
    void Update()
    {
        spriteRenderer.sprite = mainSprite;
        if (_attackSpeedTimer < 1 / attackSpeed) // 1 / attackSpeed = 1 / 2 = 0.5 
        {
            _attackSpeedTimer += Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.W)) //kiedy gracz wcisnie W, wykonaj te polecenia
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
        if(Input.GetKey(KeyCode.Space) && _attackSpeedTimer >= 1 / attackSpeed) // 1 / attackSpeed = 1 / 2 = 0.5
        {
            _attackSpeedTimer = 0;
            GameObject bulletClone = bullet;
            Instantiate(bulletClone, shootPosition.position, Quaternion.identity); // funkcja pojawiajaca rzeczy
        }
    }
}

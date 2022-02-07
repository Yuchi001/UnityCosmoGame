using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 3;

    private const float maxY = 6.5f;
    void Start()
    {
        
    }
    void Update()
    {
        Vector2 newPosition;
        newPosition.x = transform.position.x;
        newPosition.y = transform.position.y + bulletSpeed * Time.deltaTime;
        transform.position = newPosition;

        if(transform.position.y > maxY)
        {
            Destroy(gameObject); // gameObject odnosi sie do obiektu na ktorym znajduje sie dany skrypt
        }
    }
}

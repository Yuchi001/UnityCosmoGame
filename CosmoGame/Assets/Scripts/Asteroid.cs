using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float movementSpeed;
    public float maxScale;
    public float minScale;
    public int maxDamage;

    public bool split;
    [HideInInspector] public int damage;

    private const float minY = -6.5f;

    private void Start()
    {
        if(split)
        {
            float scale;
            scale = Random.Range(minScale, maxScale);
            transform.localScale = new Vector2(scale, scale + 0.1f);
        }
        damage = maxDamage * (int)transform.localScale.x + maxDamage; // damage = 1 * 0.5 + 1  // float s = 1.1 => int s = 1
    }
    void Update()
    {
        if(transform.position.y < minY)
        {
            Die();
        }
        Movement();
    }
    private void Movement()
    {
        Vector2 newPos;
        newPos.x = transform.position.x;
        newPos.y = transform.position.y - movementSpeed * Time.deltaTime;
        transform.position = newPos;
    }
    public void Die()
    {
        if(split)
        {
            for(int i = -45; i < 45; i+=90)
            {
                Quaternion newRotation = Quaternion.identity; // Vector3
                newRotation.z = i;
                GameObject asteroid = Instantiate(gameObject, transform.position, newRotation);
                Asteroid asteroidScript = asteroid.GetComponent<Asteroid>();
                asteroidScript.split = false;

                Debug.Log(asteroid);
            }
        }
        Destroy(gameObject);
    }
}

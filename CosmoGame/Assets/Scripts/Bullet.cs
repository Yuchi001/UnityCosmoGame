using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public int bulletDamage;

    public GameObject explosionParticles;

    private const float bulletSpeed = 3;
    private const float maxY = 6.5f;

    void Update()
    {
        Vector2 newPosition;
        newPosition.x = transform.position.x;
        newPosition.y = transform.position.y + bulletSpeed * Time.deltaTime;
        transform.position = newPosition;

        if (transform.position.y > maxY)
        {
            OnHit(0, false);
        }
    }

    public void OnHit(float time, bool showParticles = true)
    {
        if(showParticles)
            SpawnParticles(explosionParticles, 3f);

        Destroy(gameObject, time); // gameObject odnosi sie do obiektu na ktorym znajduje sie dany skrypt
    }
    public void SpawnParticles(GameObject particles, float time)
    {
        GameObject cloneObject = particles;
        cloneObject = Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(cloneObject, time);
    }
}

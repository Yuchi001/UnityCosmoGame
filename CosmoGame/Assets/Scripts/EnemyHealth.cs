using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int hp;
    public GameObject explosionParticles;
    void Update()
    {
        if(hp <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        SpawnParticles(explosionParticles, 2f);
        Destroy(gameObject);
    }
    public void SpawnParticles(GameObject particles, float time)
    {
        GameObject cloneObject = particles;
        cloneObject = Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(cloneObject, time);
    }
    public void TakeDamageFromObject(GameObject go, bool destroyCollisionObject = false)
    {
        int damage;
        Bullet bullet;
        bullet = go.gameObject.GetComponent<Bullet>();
        damage = bullet.bulletDamage;
        hp -= damage; // hp = hp - damage;
        if (destroyCollisionObject)
            bullet.OnHit(0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerBullet")
        {
            TakeDamageFromObject(collision.gameObject, true);
        }
    }
}

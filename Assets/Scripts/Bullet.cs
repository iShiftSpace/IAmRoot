using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletlife;
    public float bulletDamage;
    public Vector2 dir;
    Rigidbody2D rb2d;
    PlayerShooting shoot;

    public bool isEnemy;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        //rb2d.velocity = rb2d.velocity *speed;
       rb2d.AddForce(dir * bulletSpeed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        bulletlife -= Time.deltaTime;
        if (bulletlife <= 0) DestroyBullet();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") && !isEnemy)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.Damage(bulletDamage);
            DestroyBullet();
        }

        if (collision.CompareTag("Player") && isEnemy)
        {
            PlayerMovement.instance.Damage(((int)bulletDamage));
            
            DestroyBullet();
        }

       
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public float health;
    PlayerMovement player;

    [SerializeField]
    float normalSpeed;
    float slowSpeed;

    float startMovingAwayTime = 2f;
    float currentMovingAwayTime;
    Vector2 currentPosition;

    public SpriteRenderer flashSprie;
    public float reloadTime;
    public float startReloadTime;
    public int attackDamage;

    public bool isStatic;
    bool shouldFollow;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        currentMovingAwayTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) Death();
        currentPosition = transform.position;
        currentMovingAwayTime -= Time.deltaTime;
        if (currentMovingAwayTime > 0) shouldFollow = false;
        else shouldFollow = true;
        if(reloadTime <=0 && !isStatic)
        {
            if(shouldFollow)transform.position = Vector2.MoveTowards(currentPosition, player.transform.position, normalSpeed * Time.deltaTime);
            else if(!shouldFollow)transform.position = Vector2.MoveTowards(currentPosition, player.transform.position, (normalSpeed)*-1 * Time.deltaTime);

        }
    }

    private void FixedUpdate()
    {
        reloadTime -= Time.deltaTime;

    }

    public void Damage(float damage)
    {
        flashSprie.DOFade(255, 0.01f);
        flashSprie.DOFade(0, 0.1f);
        health -= damage;
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            if(reloadTime <= 0 && !isStatic)
            {
                PlayerMovement player = FindObjectOfType<PlayerMovement>();
                reloadTime = startReloadTime;
                player.Damage(attackDamage);
                currentMovingAwayTime = startMovingAwayTime;
            }

        }

      
    }
}

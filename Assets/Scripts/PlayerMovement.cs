using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using CodeMonkey.Utils;
using DG.Tweening;
public class PlayerMovement : MonoBehaviour
{
    public int health;

    Rigidbody2D body;

    float horizontal;
    float vertical;

    public float runSpeed = 10.0f;
    public float carrySpeed;
    public float startSpeed;


    
    public Transform playerSprite;
    
    public SpriteRenderer flashSprite;
    public float angle;
    public float brotherDistance;
    public float currentBrotherDistance;
    public float brotherDistanceMax;
    public bool isCarrying;
    public bool canCarry;
    bool facingLeft;
    bool facingRight;

   

    LineRenderer lineRenderer;

    [HideInInspector]
    
    public static PlayerMovement instance;

    // Start is called before the first frame update
    void Start()
    {
       
        //UIManager.instance.playerHealth.maxValue = health;
        instance = this;
        body = GetComponent< Rigidbody2D> ();
        lineRenderer = GetComponent<LineRenderer>();
        startSpeed = runSpeed;
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
      
   
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        Vector3 localScale = Vector3.one;
        if (angle > 90 || angle < -90) 
        {
            if(facingRight)
            {
                facingLeft = true;
                facingRight = false;
                playerSprite.localScale = new Vector3(playerSprite.localScale.x * -1, playerSprite.localScale.y);
            }
            
        }
        else 
        {
            if (facingLeft)
            {
                facingLeft = false;
                facingRight = true;
                playerSprite.localScale = new Vector3(playerSprite.localScale.x * -1, playerSprite.localScale.y);
            }
                
        }
       
        if (Input.GetKeyDown(KeyCode.E))
        {
            
        }

     
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

   

    public void Damage(int damage)
    {
        flashSprite.DOFade(255, 0.01f);
        flashSprite.DOFade(0, 0.1f);
        health -= damage;
        //UIManager.instance.playerHealth.value = health;
    }
}

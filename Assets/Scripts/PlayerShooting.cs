using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    GameObject crosshair;

    public Transform gun;
    public Transform gunEndPoint;
    public GameObject bullet;

    public float bulletSpeed;
    public float freeFireRate;
    public float currentFireRate;
    public float startFireRate;
    public float carryingFireRate;
    public float startDamage;
    public float currentDamage;
    public Vector3 mouseOffset;

    float distanceBetweenBrother;
    PlayerMovement player;
    Vector3 aimDirection;
    // Start is called before the first frame update
    void Start()
    {
        // Random.RandomRange()
        player = FindObjectOfType<PlayerMovement>();
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleAiming();
        HandleShooting();
        
    }

    void HandleAiming()
    {
        Vector3 mousePos = UtilsClass.GetMouseWorldPosition();
        crosshair.transform.position = mousePos;
        aimDirection = ((mousePos - transform.position) + mouseOffset).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        player.angle = angle;

        gun.eulerAngles = new Vector3(0, 0, angle);
        
        Vector3 localScale = Vector3.one;
        if(angle > 90 || angle < -90 )
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = +1f;
        }
        gun.localScale = localScale;
    }

    void HandleShooting()
    {
        currentFireRate -= Time.deltaTime;

        if(player.isCarrying)
        {
            startFireRate = carryingFireRate;
        }
        else
        {
            startFireRate = freeFireRate;
        }

        if(Input.GetMouseButton(0))
        {
            if(currentFireRate <=0)
            {
                UtilsClass.ShakeCamera(1f, .2f);
                GameObject newBullet = Instantiate(bullet, gunEndPoint.position, Quaternion.Euler(gun.localEulerAngles));
                Bullet bullets = newBullet.GetComponent<Bullet>();
                bullets.dir = aimDirection;
                bullets.bulletDamage = CalculateDamage();
                currentFireRate = startFireRate;
            }
           
        }
    }

    float CalculateDamage()
    {
        if (player.isCarrying)
        {
            currentDamage = startDamage;
        }
        else
        {
            currentDamage = ((startDamage - (startDamage*((player.currentBrotherDistance / player.brotherDistanceMax)))));
        }

        return currentDamage;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingMiniGamePlayer : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;

    public Transform[] lanes;
    int currentLane;
    public bool canAct;

    public SpawnObjects currentSpawnObject;
    public float runSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        currentLane = 0;
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");
        if (canAct) Sort();
        transform.position = new Vector2(lanes[currentLane].position.x, transform.position.y);

        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(currentLane > 0)
            {
                currentLane -= 1;
            }
        } 
        
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(currentLane < lanes.Length-1)
            {
                currentLane += 1;
            }
        }

    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(body.velocity.x, vertical * runSpeed);
    }

    public void Sort()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentSpawnObject.direction = -1;
            currentSpawnObject.isSorted = true;
            canAct = false;

        }
        else if (Input.GetMouseButtonDown(1))
        { currentSpawnObject.direction = 1;
            currentSpawnObject.isSorted = true;
            canAct = false;

        }

    }
}

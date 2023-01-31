using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingMiniGamePlayer : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;

    public bool canAct;

    public SpawnObjects currentSpawnObject;
    public float runSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");
        if (canAct) Sort();
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

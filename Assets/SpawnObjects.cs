using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public float speed;
    public int direction;
    public bool isSorted;
    public string side;
    SortingMiniGamePlayer player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<SortingMiniGamePlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isSorted)transform.Translate(0,-1 * (speed * Time.deltaTime), 0);
        else if(isSorted)transform.Translate(direction * (10 * Time.deltaTime), 0, 0);

    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player.currentSpawnObject = this;
            player.canAct = true;
        }
        else if (collision.CompareTag(side))
        {
            
        }
        else
        {
            GameplayManager.instance.livesCounter--;
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            player.currentSpawnObject = null;
            player.canAct = false;
        }
    }
}

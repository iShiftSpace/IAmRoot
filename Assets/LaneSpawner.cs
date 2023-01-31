using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneSpawner : MonoBehaviour
{
    public GameObject[] spawnObjects;
    public GameObject spawnPos;
    public float startspawnTime;
    float currentTime;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = startspawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        startspawnTime = Mathf.Clamp(startspawnTime, 0.35f, 2f);
        currentTime -= Time.deltaTime;
        if(currentTime < 0)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        int rand = Random.Range(0, spawnObjects.Length);
        Instantiate(spawnObjects[rand], spawnPos.transform.position, Quaternion.identity);
        startspawnTime -= 0.01f;
        currentTime = startspawnTime;
    }
}

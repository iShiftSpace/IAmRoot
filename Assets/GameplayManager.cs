using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{

    public TMPro.TextMeshProUGUI Lives;
    public int livesCounter;
    public static GameplayManager instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        livesCounter = 5;
    }

    // Update is called once per frame
    void Update()
    {
        Lives.text = "Lives: "+ livesCounter;
    }
}

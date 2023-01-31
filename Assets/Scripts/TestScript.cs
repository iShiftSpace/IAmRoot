using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TestScript : MonoBehaviour
{
    public DialogueBase dialogue;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) TriggerDialogue();
    }
    public void TriggerDialogue()
    {
        DialogueManager.instance.EnqueueDialogue(dialogue);


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}


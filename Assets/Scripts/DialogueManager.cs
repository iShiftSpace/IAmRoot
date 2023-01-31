using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class DialogueManager : MonoBehaviour
{
    private readonly List<char> PunctuationCharacters = new List<char>
    {
        '.',
        ',',
        '?',
        '!'

    };
    public static DialogueManager instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Fix This" + gameObject.name);


        }
        else
        {
            instance = this;
        }
    }


    public GameObject[] dialogueBox;
    public PlayerMovement movement;
    public PlayerShooting shooting;
    public Rigidbody2D rb2d;
   // public Text dialogueName;
    public Text[] dialogueText;
    //public Image dialoguePotrait;
    public float delay = 0.001f;
    public int currentDialogueBox;
    public int currentText;

    //Options stuff
    private bool IsDialogueOption;
    public GameObject dialogueOptionUI;
    public bool InDialogue;
    public GameObject[] OptionButtons;
    private int OptionsAmount;
    public Text QuestionText;

    private bool IsCurrentlyTyping;
    private string CompleteText;


    public Queue<DialogueBase.Info> dialogueInfo = new Queue<DialogueBase.Info>();//FIFO Collection

    public void EnqueueDialogue(DialogueBase db)
    {
        if (InDialogue) return;
        InDialogue = true;
        dialogueBox[currentDialogueBox].SetActive(false);

        dialogueInfo.Clear();
        rb2d.velocity = new Vector2(0, 0);
        movement.enabled = false;
        shooting.enabled = false;
        //SoundManager.instance.FadeVolume();




        if (db is DialogueOptions)
        {
            IsDialogueOption = true;
            DialogueOptions dialogueOptions = db as DialogueOptions;
            OptionsAmount = dialogueOptions.optionsInfo.Length;
            QuestionText.text = dialogueOptions.questiontext;
            for (int i = 0; i < OptionButtons.Length; i++)
            {
                OptionButtons[i].SetActive(false);
            }
            for (int i = 0; i < OptionsAmount; i++)
            {
                OptionButtons[i].SetActive(true);
                OptionButtons[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = dialogueOptions.optionsInfo[i].ButtonName;
                UnityEventHandler myEventHandler = OptionButtons[i].GetComponent<UnityEventHandler>();
                myEventHandler.eventHandler = dialogueOptions.optionsInfo[i].myEvent;
                if(dialogueOptions.optionsInfo[i].nextDialogue != null)
                {
                    myEventHandler.myDialogue = dialogueOptions.optionsInfo[i].nextDialogue;
                }
                else
                {
                    myEventHandler.myDialogue = null;
                }
            }
        }
        else
        {
            IsDialogueOption = false;
        }
        foreach(DialogueBase.Info Info in db.dialogueInfo)
        {

            dialogueInfo.Enqueue(Info);

        }
        DequeueDialogue();
    }

    public void DequeueDialogue()
    {

        if (IsCurrentlyTyping == true)
        {
            completetext();
            StopAllCoroutines();
            IsCurrentlyTyping = false;
            return;

        }
        if (dialogueInfo.Count == 0 )
        {
            EndDialogue();
            return;
        }



        dialogueBox[currentDialogueBox].SetActive(false);
        dialogueText[currentText].enabled = false;


        DialogueBase.Info Info = dialogueInfo.Dequeue();
        currentDialogueBox = Info.character.id;
        currentText = Info.character.id;
        dialogueBox[currentDialogueBox].SetActive(true);
        dialogueText[currentText].enabled = true;

        CompleteText = Info.mytext;
        // dialogueName.text = Info.myName;
        

      //dialogueName.text = Info.character.myName;
        dialogueText[currentText].text = Info.mytext;
        // dialoguePotrait.sprite = Info.potrait;
        //dialoguePotrait.sprite = Info.character.myPotrait;
        dialogueText[Info.character.id].text = "";
        StartCoroutine(TypeText(Info));
    }

    private bool CheckPunctuation(char c)
    {
        if(PunctuationCharacters.Contains(c))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    IEnumerator TypeText(DialogueBase.Info info)
    {
        IsCurrentlyTyping = true;
        
        foreach(char c in info.mytext.ToCharArray())
        {
            yield return new WaitForSeconds(delay);
            dialogueText[info.character.id].text += c;
            
            if(CheckPunctuation(c))
            {
                yield return new WaitForSeconds(0.25f);
            }
        }

        IsCurrentlyTyping = false;

    }
    private void completetext()
    {
        dialogueText[currentText].text = CompleteText;

    }
    public void EndDialogue()
    {
        dialogueBox[currentDialogueBox].SetActive(false);
        dialogueText[currentText].enabled = false;
        movement.enabled = true;
        shooting.enabled = true;
        //SoundManager.instance.UnFadeVolume();

        OptionLogic();
    }
    private void OptionLogic()
    {
        if (IsDialogueOption == true)
        {
            dialogueOptionUI.SetActive(true);
            
        }
        else
        {
            InDialogue = false;
        }
       
    }

    public void CloseOptions()
    {
        dialogueOptionUI.SetActive(false);
    }
}




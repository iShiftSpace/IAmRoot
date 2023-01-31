using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialouge", menuName = "Dialogues")]
public class DialogueBase : ScriptableObject
{
    [System.Serializable]
    public class Info
    {
        public CharacterProfile character;
        //public string myName;
        //public Sprite potrait;
        [TextArea(4, 8)]
        public string mytext;
    }
    [Header("Insert Dialogue Info Here")]
    public Info[] dialogueInfo;
}

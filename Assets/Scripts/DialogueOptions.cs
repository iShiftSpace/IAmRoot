using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "New Dialogue Options", menuName = "Dialogue Options")]
public class DialogueOptions : DialogueBase
{

    [TextArea(2, 10)]
    public string questiontext;
    [System.Serializable]
    public class Options
    {
        public string ButtonName;
        public DialogueBase nextDialogue;
        public UnityEvent myEvent;
        
    }
    public Options[] optionsInfo;

}

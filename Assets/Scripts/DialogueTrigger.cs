using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    [SerializeField] DialogueManager Manager;
    public bool Falando;
    public void TriggerDialogue()
    {
        Manager.StartDialogue(dialogue);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.F) && Falando == false)
        {
            TriggerDialogue();
            Falando = true;
        }

        //if(Manager.)
    }
}

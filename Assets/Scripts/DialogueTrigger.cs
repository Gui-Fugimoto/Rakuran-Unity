using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    [SerializeField] DialogueManager Manager;
    public bool Falando;
    public bool Conversando;
    public void TriggerDialogue()
    {
        Manager.StartDialogue(dialogue);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.F) && Falando == false && Conversando == true)
        {
            TriggerDialogue();
            Falando = true;
        }

        if(Manager.DialogoFim == true)
        {
            StartCoroutine(CooldownToStart());
        }
    }

    IEnumerator CooldownToStart()
    {
        yield return new WaitForSeconds(0.5f);
        Falando = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            Conversando = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Conversando = false;
        }
    }
}

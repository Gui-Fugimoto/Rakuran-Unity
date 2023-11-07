using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    [SerializeField] DialogueManager Manager;
    public bool Falando;
    public bool Conversando;

    public GameObject Portrait;
    public Sprite portraitChar;

    public QuestObjectiveTrigger qTrigger;
    public void TriggerDialogue()
    {
        Manager.StartDialogue(dialogue);
    }

    private void Start()
    {
        qTrigger = gameObject.GetComponent<QuestObjectiveTrigger>();
        Manager = FindObjectOfType<DialogueManager>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.E) && Falando == false && Conversando == true)
        {
            TriggerDialogue();
            Falando = true;
            if(portraitChar != null)
            {
                Portrait.GetComponent<Image>().sprite = portraitChar;
                Portrait.SetActive(true);

            }
        }

        if(Manager.DialogoFim == true)
        {
            StartCoroutine(CooldownToStart());

            if(qTrigger != null)
            {
                qTrigger.Talk();

                Debug.Log("am im being called twice");
            }


            Portrait.SetActive(false);
            //Manager.DialogoFim = false;


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

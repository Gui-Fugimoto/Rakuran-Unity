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
    public bool MoveOn;

    public GameObject Portrait;
    public Sprite portraitChar;

    public QuestObjectiveTrigger qTrigger;
    public DialogueSwitch Dswitch;
    public bool fabianoOnce = true;
    public void TriggerDialogue()
    {
        Manager.StartDialogue(dialogue);
    }

    private void Start()
    {
        qTrigger = gameObject.GetComponent<QuestObjectiveTrigger>();
        Dswitch = gameObject.GetComponent<DialogueSwitch>();
        Falando = false;
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E) && Falando == false && Conversando == true)
        {
            Manager = FindObjectOfType<DialogueManager>();
            TriggerDialogue();
            Falando = true;
            if(Portrait != null)
            {
                Portrait.GetComponent<Image>().sprite = portraitChar;
                Portrait.SetActive(true);

            }
        }
        
        

        if(Manager != null && Manager.DialogoFim == true)
        {

            StartCoroutine(CooldownToStart());
            
            if (qTrigger != null && fabianoOnce)
            {
                fabianoOnce = false;
                qTrigger.Talk();

                Debug.Log("am im being called twice");
            }
            //
            //if (Dswitch != null && fabianoOnce)
            //{
            //    Dswitch.carryOn();
            //    fabianoOnce = false;
            //}

            if (Portrait != null)
            {
                Portrait.SetActive(false);
            }

        }

    }

    IEnumerator CooldownToStart()
    {
        
        yield return new WaitForSeconds(0.1f);

        Manager.DialogoFim = false;
        Manager = null;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    Queue<string> sentences;
    public bool DialogoFim;
    public TMP_Text Nome;
    public TMP_Text Fala;
    private bool proxFala;
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();
        DialogoFim = false;
        proxFala = false;
        StartCoroutine(PressCoolDown());

        Nome.text = dialogue.name;

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentece();

    }

    public void DisplayNextSentece()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        Fala.text = sentence;
    }

    void EndDialogue()
    {
        DialogoFim = true;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.F) && DialogoFim == false && proxFala == true)
        {
            DisplayNextSentece();
            proxFala = false;
            StartCoroutine(PressCoolDown());
        } 
    }

    IEnumerator PressCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        proxFala = true;
    }

}

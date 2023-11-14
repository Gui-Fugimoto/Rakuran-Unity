using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSwitch : MonoBehaviour
{
    public List<DialogueTrigger> dialogues = new List<DialogueTrigger>();
    [SerializeField] int ActvationOrder = 0;
    public DialogueRecord record;

    private void Start()
    {
        ActvationOrder = record.FaladorPassaMal;
        dialogues[ActvationOrder].enabled = true;

        for(int i = 0; i <= dialogues.Count; i++)
        {
            if (i != ActvationOrder)
            {
                dialogues[i].enabled = false;
            }

            if (i == ActvationOrder)
            {
                i++;
            }
        }
    }

    private void Update()
    {
        if(ActvationOrder > dialogues.Count) 
        {
            ActvationOrder = dialogues.Count - 1;
        }


    }

    public void carryOn()
    {
        if (ActvationOrder == dialogues.Count - 1)
        {
            dialogues[ActvationOrder].enabled = true;
            dialogues[ActvationOrder].MoveOn = true;
        }

        if (dialogues[ActvationOrder].MoveOn == false)
        {
            dialogues[ActvationOrder].enabled = false;
            ActvationOrder = ActvationOrder + 1;
            dialogues[ActvationOrder].enabled = true;
            record.FaladorPassaMal = ActvationOrder;
        }
       
    }
    


}

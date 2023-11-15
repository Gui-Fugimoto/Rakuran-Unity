using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectiveType
{
    Talk,
    Deliver,
    UseItem,
    WalkRegion,
    Defeat
    
}
public class QuestObjectiveTrigger : MonoBehaviour
{
    public ObjectiveType objectiveType;
    public QuestObject quest;
    public QuestStage stage;
    public QuestManager questManager;
    public Inventory playerInventory;
    private DialogueTrigger dTrigger;
    private EnemyLife enemyHPcontrol;
    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        playerInventory = FindObjectOfType<Inventory>();
        dTrigger = GetComponent<DialogueTrigger>();
        if(objectiveType == ObjectiveType.Defeat)
        {
            enemyHPcontrol = GetComponent<EnemyLife>();
        }
    }
    private void Awake()
    {
        questManager = FindObjectOfType<QuestManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (objectiveType)
        {
            case ObjectiveType.Talk:
                break;

            case ObjectiveType.Deliver:
                break;

            case ObjectiveType.UseItem:
                break;

            case ObjectiveType.WalkRegion:

                break;

            case ObjectiveType.Defeat:
                //Defeat();
                break;
        }
            

    }

    public void Talk()
    {
        if(stage.isDone == false)
        {
            if (objectiveType == ObjectiveType.Deliver)
            {
                OnDeliver();
            }
            else
            {
                stage.isDone = true;
                questManager.AdvanceQuestStage(quest);
                Debug.Log("Quest avançou stage");
            }
            
            
        }
        

        //this.enabled = false;
    }

    public void OnDefeat()
    {
        if (stage.isDone == false)
        {           
            stage.isDone = true;
            questManager.AdvanceQuestStage(quest);
            Debug.Log("Quest avançou stage");
            
        }
    }

    public void OnDeliver()
    {
        for(int i = 0; i < quest.qDeliverItems.Count; i++)
        {
            if (playerInventory.itens.Contains(quest.qDeliverItems[i]))
            {
                playerInventory.RemoveItem(quest.qDeliverItems[i]);
                quest.qDeliverItems[i] = null;
                
            }
            
        }
        for (int i = 0; i < quest.qDeliverItems.Count; i++)
        {
            if(quest.qDeliverItems[i] == null)
            {
                quest.qDeliverItems.Remove(quest.qDeliverItems[i]);
            }
        }
        playerInventory.saveFile.Invsave = new List<ItemParameter>(playerInventory.itens);
        if(quest.qDeliverItems.Count <= 1 && (quest.qDeliverItems[0] == null || quest.qDeliverItems == null))
        {
            stage.isDone = true;
            questManager.AdvanceQuestStage(quest);
        }
        else
        {
            dTrigger.fabianoOnce = true;
        }
    }
}

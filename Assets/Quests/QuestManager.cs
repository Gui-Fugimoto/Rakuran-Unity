using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<QuestObject> Quests;
    public int currentStageIndex = 0;
    public GameObject player;
    private Inventory inventory;

    
    void Start()
    {
        QuestsOnStart();
        inventory = player.GetComponent<Inventory>();
    }

    


    void QuestsOnStart()
    {
        foreach (QuestObject quest in Quests)
        {
            //currentStageIndex = quest.stageIndex;
            QuestStage currentStage = quest.qStage[quest.stageIndex];
            quest.qSpawnList = currentStage.sSpawnList;
            quest.qReceiveItems = currentStage.sReceiveItems;
            
            if(currentStage.addedDescription == false)
            {
                quest.qDescription = quest.qDescription + currentStage.sDescription.ToString();
                currentStage.addedDescription = true;
            }
            if(currentStage.deliveredItem == false)
            {
                quest.qDeliverItems = new List<ItemParameter>(currentStage.sDeliverItems);
                currentStage.deliveredItem = true;
            }
            SpawnQuestObjects(quest);
        }
        
    }
    public void AdvanceQuestStage(QuestObject quest)
    {
        currentStageIndex = quest.stageIndex;
        
        if (quest.qStage[currentStageIndex].isDone && currentStageIndex < quest.qStage.Count - 1)
        {
            quest.stageIndex++;
            Debug.Log("MAs e agora?/");
            ManageQuests(quest);

            
        }
    }
    public void ManageQuests(QuestObject quest)
    {
        //escolhe a quest e o stage dela                

        QuestStage currentStage = quest.qStage[quest.stageIndex];



        quest.qDescription = quest.qDescription + " " + currentStage.sDescription.ToString();
        quest.qSpawnList = currentStage.sSpawnList;
        quest.qDeliverItems = new List<ItemParameter>(currentStage.sDeliverItems);
        quest.qReceiveItems = currentStage.sReceiveItems;

        currentStage.addedDescription = true;
        SpawnQuestObjects(quest);
        GiveItemToPlayer(quest);
        if (quest.stageIndex > 0)
        {
            //Pegou quest
            //faz aparecer no menu
            Debug.Log("Quest Start" + quest.qDescription);
        }

    }

    void SpawnQuestObjects(QuestObject quest)
    {
        if(quest.qSpawnList != null)
        {
            for (int i = 0; i < quest.qSpawnList.Count; i++)
            {

                Debug.Log("SpawnOnStart");
                Instantiate(quest.qSpawnList[i]);

            }
        }
        

    }
    void GiveItemToPlayer(QuestObject quest)
    {
        if(quest.qReceiveItems != null)
        {
            foreach (ItemParameter item in quest.qReceiveItems)
            {
                inventory.AddItem(item);
            }
            
        }
    }


   

    public void QuestResetAll()
    {
        foreach (QuestObject quest in Quests)
        {
            quest.stageIndex = 0;
            quest.qDescription = null;
            for (int i = 0; i < quest.qStage.Count; i++)
            {
                quest.qStage[i].isDone = false;
                quest.qStage[i].addedDescription = false;
                quest.qStage[i].deliveredItem = false;
            }
        }
    }
}

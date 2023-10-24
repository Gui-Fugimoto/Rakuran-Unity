using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<QuestObject> Quests;
    public int questIndex;
    public int questStageIndex;
    public int currentStageIndex;
    

    void Start()
    {
        questIndex = 0;
        //ManageQuestObjects();
    }

    

    public void ManageQuests(QuestObject quest)
    {
        //escolhe a quest e o stage dela                

        QuestStage currentStage = quest.qStage[quest.stageIndex];
        

        
        quest.qDescription = quest.qDescription + " " + currentStage.sDescription.ToString();
        ManageQuestObjects(quest);
        
        if (quest.stageIndex > 0)
        {
            //Pegou quest
            //faz aparecer no menu
            Debug.Log("Quest Start" + quest.qDescription);              
        }
            
              
     

    }
    //chama por evento
    public void AdvanceQuestStage(QuestObject quest)
    {
        currentStageIndex = quest.stageIndex;
        
        if (currentStageIndex < quest.qStage.Count - 1)
        {
            quest.stageIndex++;
            ManageQuests(quest);
        }
    }

    void ManageQuestObjects(QuestObject quest)
    {
        for(int i = 0; i <= quest.qSpawnList.Count; i++)
        {
            //Quests[questIndex].qSpawnList[i].SetActive(true);
            Debug.Log("sasageyo?");
            Instantiate(quest.qSpawnList[i]);
        }

        for (int i = 0; i <= quest.qDespawnList.Count; i++)
        {
            quest.qDespawnList[i].SetActive(false);
        }

    }
}

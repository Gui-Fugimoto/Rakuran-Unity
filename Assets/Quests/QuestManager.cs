using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<QuestObject> Quests;
    public int questIndex;
    public int questStageIndex;
    public int currentStageIndex = 0;
    

    void Start()
    {
        questIndex = 0;
        //ManageQuestObjects();
    }

    

    
    //chama por evento
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
        quest.qDespawnList = currentStage.sDespawnList;
        ManageQuestObjects(quest);

        if (quest.stageIndex > 0)
        {
            //Pegou quest
            //faz aparecer no menu
            Debug.Log("Quest Start" + quest.qDescription);
        }




    }

    void ManageQuestObjects(QuestObject quest)
    {
        for(int i = 0; i <= quest.qSpawnList.Count; i++)
        {
            Debug.Log("sasageyo?");
            Instantiate(quest.qSpawnList[i]);
            
        }

        for (int n = 0; n <= quest.qDespawnList.Count; n++)
        {
            //quest.qDespawnList[n].SetActive(false);
            //Destroy(quest.qDespawnList[n]);
        }

    }
}

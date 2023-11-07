using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<QuestObject> Quests;
    public int questIndex;
    public int questStageIndex;
    public int currentStageIndex = 0;

    public List<RemoveByQuest> removableObjects = new List<RemoveByQuest>();

    void Start()
    {
        questIndex = 0;
        QuestsOnStart();
        
    }

    void Update()
    {
        
    }


    void QuestsOnStart()
    {
        foreach (QuestObject quest in Quests)
        {
            QuestStage currentStage = quest.qStage[quest.stageIndex];
            quest.qDescription = quest.qDescription + currentStage.sDescription.ToString();
            quest.qSpawnList = currentStage.sSpawnList;
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
        SpawnQuestObjects(quest);

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

    void FindRemovableObjects()
    {
        RemoveByQuest[] removeByQuestScripts = FindObjectsOfType<RemoveByQuest>();

        removableObjects.AddRange(removeByQuestScripts);

        foreach (QuestObject qts in Quests)
        {
            foreach (RemoveByQuest rmbObj in removableObjects)
            {

            }
        }
        
    }

    
    void DespawnAllByStage()
    {
        foreach (QuestObject qts in Quests)
        {

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<QuestObject> Quests;
    public int questIndex;
    public int questStageIndex;
    public bool once;

    void Start()
    {
        questIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //chama o manage quest 1 vez pra cada quest na lista
        foreach (var quest in Quests)
        {
            ManageQuests(quest);
        }
    }

    void ManageQuests(QuestObject quest)
    {
        //escolhe a quest e o stage dela
        quest = Quests[questIndex];
        int questStageIndex = quest.stageIndex;

        QuestStage currentStage = quest.qStage[questStageIndex];
        

        if (!once)
        {
            quest.qDescription = quest.qDescription + " " + currentStage.sDescription.ToString();
            if(quest.stageIndex > 0)
            {
                //Pegou quest
                //faz aparecer no menu
                Debug.Log("Quest Start" + quest.qDescription);
               
            }
            once = true;
        }

        
     

    }

    public void AdvanceQuestStage(QuestObject currentQuest)
    {
        currentQuest = Quests[questIndex];
        int currentStageIndex = currentQuest.stageIndex;
        once = false;
        if (currentStageIndex < currentQuest.qStage.Count - 1)
        {
            currentQuest.stageIndex++;
        }
    }
}

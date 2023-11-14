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
    

    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
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
            stage.isDone = true;
            questManager.AdvanceQuestStage(quest);           
            Debug.Log("Quest avançou stage");
        }
        

        //this.enabled = false;
    }

    public void OnDefeat()
    {

    }
}

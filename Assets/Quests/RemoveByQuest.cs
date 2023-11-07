using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveByQuest : MonoBehaviour
{
    public QuestObject quest;
    public QuestStage stageToDestroy;
    

    private void Start()
    {        
        if (quest.stageIndex >= stageToDestroy.stageNo)
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (quest.stageIndex >= stageToDestroy.stageNo)
        {
            Destroy(gameObject);
        }
    }
}

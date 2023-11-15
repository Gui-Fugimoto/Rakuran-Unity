using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestStage", menuName = "New Quest Stage")]
public class QuestStage : ScriptableObject
{
    public int stageNo;
    public string sDescription;
    public List<GameObject> sSpawnList;
    public List<ItemParameter> sDeliverItems;
    public List<ItemParameter> sReceiveItems;

    public bool addedDescription = false;
    public bool isDone;
    public bool deliveredItem = false;
    
}

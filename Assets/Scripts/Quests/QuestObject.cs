using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Quest", menuName = "New Quest")]
public class QuestObject : ScriptableObject
{
    public string qName;

    public List<QuestStage> qStage;
    public int stageIndex;
    public string qDescription;
    public List<string> qDialog;
    public List<GameObject> qObjectives;
    public List<GameObject> qNPCs;
    
}



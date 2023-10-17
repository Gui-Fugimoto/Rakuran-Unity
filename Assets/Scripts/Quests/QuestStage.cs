using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestStage", menuName = "New Quest Stage")]
public class QuestStage : ScriptableObject
{
    public int stageNo;
    public string sDescription;
    public List<string> sDialog;
    public List<GameObject> sObjectives;
    public List<GameObject> sNPCs;
    public bool sCompleted;
    
}

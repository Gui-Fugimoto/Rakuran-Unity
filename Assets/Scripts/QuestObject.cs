using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Quest", menuName = "New Quest")]
public class QuestObject : ScriptableObject
{
    public string qName;

    public List<QuestStage> qStage;

    public string qDescription;
    public List<string> qDialog;
    public List<GameObject> qObjectives;
    public List<GameObject> qNPCs;
    public List<GameObject> qTarget;
}

[CreateAssetMenu(fileName = "QuestStage", menuName = "New Quest Stage")]
public class QuestStage : ScriptableObject
{
    public string sDescription;
    public List<string> sDialog;
    public List<GameObject> sObjectives;
    public List<GameObject> sNPCs;
    public List<GameObject> sTarget;
}

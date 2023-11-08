using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestSceneControl : MonoBehaviour
{
    public string scene;
    private QuestObjectiveTrigger qobt;
    void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        qobt = GetComponent<QuestObjectiveTrigger>();
        qobt.enabled = true;
        if (currentSceneName != scene)
        {
            Destroy(gameObject);
        }
    }    
}

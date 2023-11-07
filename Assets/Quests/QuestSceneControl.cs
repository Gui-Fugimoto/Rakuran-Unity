using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestSceneControl : MonoBehaviour
{
    public string scene;
    void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName != scene)
        {
            Destroy(gameObject);
        }
    }    
}

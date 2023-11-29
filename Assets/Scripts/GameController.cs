using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public SaveFile Save;

    public string Name;
    [TextArea(3, 7)]
    public string HaiKai;
   
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}

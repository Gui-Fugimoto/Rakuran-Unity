using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public SaveFile Save;
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}

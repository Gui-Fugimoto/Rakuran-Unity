using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public SaveFile Save;
    public GameObject Fadein;

    public string Name;
    [TextArea(3, 7)]
    public string HaiKai;
   
    public void GameOver()
    {
        Fadein.SetActive(true);
        StartCoroutine(PlayerIsDead());
    }

    IEnumerator PlayerIsDead()
    {
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene("GameOver");
    }
}

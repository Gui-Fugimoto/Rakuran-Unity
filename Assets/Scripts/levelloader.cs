using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelloader : MonoBehaviour
{
    public SaveFile save;
    public int ScenetoLoad;
    public Button loadButton;
    public GameObject Continue;
    public AsyncOperation asyncLoad;
    public GameObject FadeOut;

    void Start()
    {
        ScenetoLoad = save.CScene;
        Continue.SetActive(false);
        StartCoroutine(LoadScene());
        asyncLoad.allowSceneActivation = false;

    }

    private void Update()
    {
        if (asyncLoad.progress >= 0.7)
        {
            Continue.SetActive(true);
        }
    }

    IEnumerator LoadScene()
    {
        asyncLoad = SceneManager.LoadSceneAsync(save.CScene);
        while (asyncLoad.isDone == false)
        {
            Debug.Log(asyncLoad.progress);

            yield return null;
        }
    }

    public void TransitionToLoadedScene()
    {
        if (asyncLoad.progress <= 0.9)
        {
            FadeOut.SetActive(true);   
            StartCoroutine(waitforfade());
        }
    }

    IEnumerator waitforfade()
    {
        yield return new WaitForSeconds(0.6f);
        asyncLoad.allowSceneActivation = true;
    }


}
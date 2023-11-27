using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelloader : MonoBehaviour
{
    public SaveFile save;
    public Image loadingSprite;
    public Button loadButton;
    private AsyncOperation asyncLoad;

    void Start()
    {
        LoadSceneAsync();
        loadButton.interactable = false;
    }

    public void LoadSceneAsync()
    {
        // Use SceneManager to load the scene asynchronously by name
        asyncLoad = SceneManager.LoadSceneAsync(save.CScene);
        asyncLoad.completed += operation =>
        {
            // Enable the button when loading is complete
            loadButton.interactable = true;
        };
    }

    public void TransitionToLoadedScene()
    {
        if (asyncLoad != null && asyncLoad.isDone)
        {
            // Check if the scene is fully loaded before transitioning
            SceneManager.LoadScene(save.CScene);
        }
        else
        {
            Debug.LogWarning("Scene is not fully loaded yet.");
            // You can add a loading message or animation to inform the user here
        }
    }


}

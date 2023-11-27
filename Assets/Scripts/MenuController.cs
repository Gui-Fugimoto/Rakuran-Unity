using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public SaveFile Save;
    public GameObject ContinueButton;
    public ItemParameter firstWeapon;
    public ItemParameter secondWeapon;
    public void PlayGame()
    {
        SceneManager.LoadScene(5);
    }

    public void NewGame()
    {
        Save.CPpos = new Vector3(0, 0, 0);
        Save.Invsave.Clear();
        Save.CScene = 1;
        Save.Arma1 = firstWeapon;
        Save.Arma2 = secondWeapon;
        Save.QuickSlot = null;
        Save.QuickSlot1 = null;
        Save.QuickSlot2 = null;
        Save.QuickSlot3 = null;
        SceneManager.LoadScene(5);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    private void Update()
    {
        if (Save.CScene == 0)
        {
            ContinueButton.GetComponent<Button>().interactable = false;
            ContinueButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.5f);
        }

        if (Save.CScene > 0)
        {
            ContinueButton.GetComponent<Button>().interactable = true;
            ContinueButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0, 0, 0, 1);
        }
    }

    IEnumerator LoadScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(Save.CScene);

        while (operation.isDone == false)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log(progress);

            yield return null;
        }
    }
}
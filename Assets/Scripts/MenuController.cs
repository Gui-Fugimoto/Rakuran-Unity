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
    public void PlayGame()
    {
        SceneManager.LoadScene(Save.CScene);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
        Save.CPpos = new Vector3 (0,0,0);
        Save.Invsave.Clear();
        Save.CScene = 0;
        Save.Arma1 = firstWeapon;
        Save.Arma2 = null;
        Save.QuickSlot = null;
        Save.QuickSlot1 = null;
        Save.QuickSlot2 = null;
        Save.QuickSlot3 = null;
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
}
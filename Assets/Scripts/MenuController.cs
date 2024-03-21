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
    public GameObject FadeOut;
    private TeleportParkour FadeOutRef;
    public List<ItemParameter> AlqGuide =  new List<ItemParameter>();
    public GameObject ContinueButton;
    public ItemParameter firstWeapon;
    public ItemParameter secondWeapon;
    public REALSaver REALSaver;

    private void Start()
    {
        Time.timeScale = 1f;
        FadeOutRef =  FadeOut.GetComponent<TeleportParkour>();
        REALSaver.LoadGame();
    }

    public void PlayGame()
    {
        FadeOut.SetActive(true);
        FadeOutRef.IsSceneTrans = true;
        StartCoroutine(WaitforFadeOut());
    }

    public void NewGame()
    {
        #region saveReset
        Save.CPpos = new Vector3(0, 0, 0);
        Save.Invsave = new List<ItemParameter>(AlqGuide);
        Save.CScene = 1;
        Save.Arma1 = firstWeapon;
        Save.Arma2 = secondWeapon;
        Save.QuickSlot = null;
        Save.QuickSlot1 = null;
        Save.QuickSlot2 = null;
        Save.QuickSlot3 = null;
        #endregion

        FadeOut.SetActive(true);
        FadeOutRef.IsSceneTrans = true;
        
        StartCoroutine(WaitforFadeOut());
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

   IEnumerator WaitforFadeOut()
   {
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadSceneAsync(5);
    }
}
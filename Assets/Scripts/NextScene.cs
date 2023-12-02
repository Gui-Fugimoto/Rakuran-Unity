using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    // Start is called before the first frame update
    public SaveFile save;
    public GameController controller;
    public GameObject FadeOut;
    private TeleportParkour FadeOutSc;
    public PlayerCombat WeaponFinder;
    public quickslotRef fodase;
    public int NextSceneIndex;
    public Inventory inventory;
    public Vector3 NextSceneSpawnPos;
    
    void Start()
    {
        controller = FindObjectOfType<GameController>();
        FadeOutSc = FadeOut.GetComponent<TeleportParkour>();
        save = controller.Save;
        inventory = FindObjectOfType<Inventory>();
        fodase = FindObjectOfType<quickslotRef>();
        WeaponFinder = FindObjectOfType<PlayerCombat>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("passouPlayer");
            FadeOut.SetActive(true);
            FadeOutSc.IsSceneTrans = true;
            StartCoroutine(SceneChange());
            
        }
    }

    IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(0.6f);
        save.Invsave = new List<ItemParameter>(inventory.itens);
        
        save.Arma1 = WeaponFinder.mainWeapon.item;
        save.Arma2 = WeaponFinder.offhandWeapon.item;
        save.CScene = SceneManager.GetActiveScene().buildIndex;
        save.QuickSlot = fodase.QuickSlot.item;
        save.QuickSlot1 = fodase.QuickSlot1.item;
        save.QuickSlot2 = fodase.QuickSlot2.item;
        save.QuickSlot3 = fodase.QuickSlot3.item;

        SceneManager.LoadSceneAsync(5);

        save.CScene = NextSceneIndex;
        save.CPpos = NextSceneSpawnPos;
    }
}

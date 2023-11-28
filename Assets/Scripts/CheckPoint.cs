using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CheckPoint : MonoBehaviour
{
    public SaveFile Save;
    public bool Activate;
    public Inventory pInv;
    public PlayerCombat WeaponFinder;
    public quickslotRef fodase;
    [SerializeField] KeyCode Interact;
    
    private void Start()
    {
        Save = FindObjectOfType<GameController>().Save;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            pInv = other.GetComponent<Inventory>();
            WeaponFinder = other.GetComponentInChildren<PlayerCombat>();
            fodase = FindObjectOfType<quickslotRef>();
            Activate = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        pInv = null;
        WeaponFinder = null;
        //QSlot = null;
        Activate = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Interact) && Activate == true)
        {
            Save.CPpos = gameObject.transform.position;
            Save.Invsave = new List<ItemParameter>(pInv.itens);
            Save.Arma1 = WeaponFinder.mainWeapon.item;
            Save.Arma2 = WeaponFinder.offhandWeapon.item;
            Save.CScene = SceneManager.GetActiveScene().buildIndex;
            
            Save.QuickSlot = fodase.QuickSlot.item;
            Save.QuickSlot1 = fodase.QuickSlot1.item;
            Save.QuickSlot2 = fodase.QuickSlot2.item;
            Save.QuickSlot3 = fodase.QuickSlot3.item;
            
            
            Debug.Log("cena salva" + Save.CScene);
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    public SaveFile Save;
    public bool Activate;
    public Inventory pInv;
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
            Activate = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        pInv = null;
        Activate = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Interact) && Activate == true)
        {
            Save.CPpos = gameObject.transform.position;
            Save.Invsave = new List<ItemParameter>(pInv.itens);
            Save.CScene = SceneManager.GetActiveScene().buildIndex;
            Debug.Log("cena salva" + Save.CScene);
        }
    }
}

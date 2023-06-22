using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInventory : MonoBehaviour
{
    // Start is called before the first frame update
    #region Variáveis

    public List<ItemParameter> itens = new List<ItemParameter>();
    public int inventorySize = 25;
    public GameObject InventoryUI;
    public GameObject ChestUI;
    public bool Aberto;
    public bool PlayerPerto;
    public bool Grab;
    [SerializeField] KeyCode Interact;

    #endregion

    public delegate void MudouItem();
    public MudouItem MudouItemCallback;


    // Update is called once per frame
    private void Start()
    {
        ChestUI.SetActive(false);
    }

    void Update()
    {
        
    }


    public void AddItem(ItemParameter item)
    {
        if(itens.Count >= inventorySize)
        {
            Debug.Log("Sem espaço!");
        }
        
        itens.Add(item);

        if (MudouItemCallback != null)
            MudouItemCallback.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && Input.GetKeyDown(Interact))
        {
            ToggleInventory();
        }
    }


   private void OnTriggerExit(Collider other)
   {
       if(other.tag == "Player" && Aberto == true)
       {
           InventoryUI.SetActive(false);
           ChestUI.SetActive(false);
           Aberto = false;
       }
   }

    void ToggleInventory()
    {
        if(Aberto == false)
        {
            InventoryUI.SetActive(true);
            ChestUI.SetActive(true);
            Aberto = true;
        }
    }

    public void RemoveItem(ItemParameter item)
    {
        itens.Remove(item);

        if (MudouItemCallback != null)
            MudouItemCallback.Invoke();
    }
}


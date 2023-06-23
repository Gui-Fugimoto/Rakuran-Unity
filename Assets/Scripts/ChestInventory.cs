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
        if(Input.GetKeyDown(Interact) && PlayerPerto == true) 
        {
            ToggleInventory();

        }
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerPerto = true;
        }
    }


   private void OnTriggerExit(Collider other)
   {
       if(other.tag == "Player")
       {
           InventoryUI.SetActive(false);
           ChestUI.SetActive(false);
            PlayerPerto = false;
       }
   }

    void ToggleInventory()
    {
      InventoryUI.SetActive(true);
      ChestUI.SetActive(true);
     
    }

    public void RemoveItem(ItemParameter item)
    {
        itens.Remove(item);

        if (MudouItemCallback != null)
            MudouItemCallback.Invoke();
    }
}


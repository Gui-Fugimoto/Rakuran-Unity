using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    #region Variáveis

    public List<ItemParameter> itens = new List<ItemParameter>();
    public SaveFile saveFile;
    public int inventorySize = 12;
    public GameObject InventoryUI;
    public GameObject WeaponEquip;
    public GameObject Inspector;
    public bool Aberto;
    public bool isInventoryOpen;
    
    public delegate void MudouItem();
    public MudouItem MudouItemCallback;
    public GameObject QuickSlots;
    public Pause pauseRef;
    
    #endregion

    // Update is called once per frame
    private void Start()
    {
        saveFile = FindObjectOfType<GameController>().Save;
        itens = new List <ItemParameter>(saveFile.Invsave);
        pauseRef = FindObjectOfType<Pause>(); 
        InventoryUI.SetActive(false);
        WeaponEquip.SetActive(false);
        MudouItemCallback.Invoke();
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || (Input.GetKeyDown(KeyCode.Escape) && Aberto == true))
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

    public void ToggleJUSTInventory()
    {
        if (Aberto == false)
        {
            Aberto = true;
            pauseRef.IsMenuOverwritten = true;
            InventoryUI.SetActive(true);
            MudouItemCallback.Invoke();

        }
        else
        {
            pauseRef.IsMenuOverwritten = false;
            InventoryUI.SetActive(false);
            Inspector.SetActive(false);
            Aberto = false;

        }
    }

    void ToggleInventory()
    {
        if(Aberto == false)
        {
            Aberto = true;
            pauseRef.IsMenuOverwritten = true;
            InventoryUI.SetActive(true);
            WeaponEquip.SetActive(true);
            MudouItemCallback.Invoke();
            
        }
        else
        {
            pauseRef.IsMenuOverwritten = false;
            InventoryUI.SetActive(false);
            WeaponEquip.SetActive(false);
            Inspector.SetActive(false);
            Aberto = false;
            
        }
    }

    public void RemoveItem(ItemParameter item)
    {
        itens.Remove(item);

        if (MudouItemCallback != null)
            MudouItemCallback.Invoke();

    }

}

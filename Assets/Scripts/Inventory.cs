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

    #endregion

    // Update is called once per frame
    private void Start()
    {
        saveFile = FindObjectOfType<GameController>().Save;
        itens = new List <ItemParameter>(saveFile.Invsave);
        InventoryUI.SetActive(false);
        WeaponEquip.SetActive(false);
        MudouItemCallback.Invoke();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
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

    void ToggleInventory()
    {
        if(Aberto == false)
        {
            Aberto = true;
            InventoryUI.SetActive(true);
            WeaponEquip.SetActive(true);
            MudouItemCallback.Invoke();
            
        }
        else
        {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    #region Variáveis

    public List<ItemParameter> itens = new List<ItemParameter>();
    public int inventorySize = 10;
    public GameObject InventoryUI;
    public bool Aberto;

    #endregion

    public delegate void MudouItem();
    public MudouItem MudouItemCallback;

    // Update is called once per frame
    private void Start()
    {
        InventoryUI.SetActive(false);
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
            InventoryUI.SetActive(true);
            Aberto = true;
        }
        else
        {
            InventoryUI.SetActive(false);
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

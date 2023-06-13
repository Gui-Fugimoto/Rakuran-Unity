using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    #region Vari�veis

    public List<ItemParameter> itens = new List<ItemParameter>();
    public int inventorySize = 10;
    public GameObject InventoryUI;
    public GameObject quickslotsUI;
    public bool Aberto;
    public bool isInventoryOpen;
    public List<ItemParameter> quickslots = new List<ItemParameter>();
    public int quickslotCount = 4;

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
            Debug.Log("Sem espa�o!");
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
    public void MoveToQuickslot(ItemParameter item)
    {
        if (quickslots.Count >= quickslotCount)
        {
            Debug.Log("No space in quickslots!");
            return;
        }

        itens.Remove(item);
        quickslots.Add(item);

        if (MudouItemCallback != null)
            MudouItemCallback.Invoke();
    }

    public void ConsumeQuickslot(int index)
    {
        if (index < 0 || index >= quickslots.Count)
            return;

        ItemParameter item = quickslots[index];
        quickslots.RemoveAt(index);
        // Consume the item here

        if (MudouItemCallback != null)
            MudouItemCallback.Invoke();
    }
}

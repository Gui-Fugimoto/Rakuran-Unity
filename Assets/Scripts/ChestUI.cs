using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestUI : MonoBehaviour
{
    [SerializeField] ChestInventory inventory;

    public Transform itensParent;

    InventorySlot[] slots;
    void Start()
    {
        inventory.MudouItemCallback += UpdateUI;

        slots = itensParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.itens.Count)
            {
                slots[i].AddItem(inventory.itens[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}

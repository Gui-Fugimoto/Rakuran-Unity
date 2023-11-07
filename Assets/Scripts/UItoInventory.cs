using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UItoInventory : MonoBehaviour
{
    public Inventory Inventario;

    private void Start()
    {
        Inventario = FindObjectOfType<Inventory>();
    }

    public void AddItem(ItemParameter item)
    {
        Inventario.AddItem(item);
    }
}

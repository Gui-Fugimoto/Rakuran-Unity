using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UItoInventory : MonoBehaviour
{
    public Inventory Inventario;

    public void AddItem(ItemParameter item)
    {
        Inventario.AddItem(item);
    }
}

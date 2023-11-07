using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UItoChest : MonoBehaviour
{
    public ChestInventory Inventario;

    private void Start()
    {
        Inventario = FindObjectOfType<ChestInventory>();
    }
    public void AddItem(ItemParameter item)
    {
        Inventario.AddItem(item);
    }
}

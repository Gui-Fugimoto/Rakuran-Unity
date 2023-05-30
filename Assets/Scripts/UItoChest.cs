using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UItoChest : MonoBehaviour
{
    public ChestInventory Inventario;

    public void AddItem(ItemParameter item)
    {
        Inventario.AddItem(item);
    }
}

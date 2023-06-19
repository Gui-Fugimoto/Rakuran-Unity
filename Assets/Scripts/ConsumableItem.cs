using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableItem : MonoBehaviour
{
    
    public Image Icon;
    ItemParameter item;
    public DraggableItem child;
    void Start()
    {
        Icon.enabled = false;
    }

    public void AddItem(ItemParameter Item)
    {
        item = Item;
        child.Item = Item;

        Icon.sprite = item.Icon;
        Icon.enabled = true;

    }

    public void ClearSlot()
    {
        item = null;

        Icon.sprite = null;
        Icon.enabled = false;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventorySlot : MonoBehaviour
{
    public Image Icon;
    public ItemParameter item;
    public DraggableItem child;
    void Start()
    {   
        Icon.enabled = false;
    }

    public void AddItem(ItemParameter novoItem)
    {
        item = novoItem;
        child.Item = novoItem;

        Icon.sprite = item.Icon;
        Icon.enabled = true;
        
    }

    public void ClearSlot()
    {
        item = null;

        Icon.sprite = null;
        Icon.enabled = false;
    }

   private void FixedUpdate()
   {
       if (item != null)
       {
           Icon.enabled = true;
       }
   }
}

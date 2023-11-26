using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngridientDisplayer : MonoBehaviour
{
    public Image Icon;
    public ItemParameter Item;
    public Inventory inventory;
    public PotionCrafting Daddy;
    
    // Start is called before the first frame update
    void Start()
    {
        Icon.enabled = false;
        inventory = FindObjectOfType<Inventory>();
    }

    public void AddItem(ItemParameter item)
    {
        Icon.enabled = true;
        Icon.sprite = item.Icon;
        Item = item;
    }

    public void ClearSlot()
    {
        Icon.sprite = null;
        Icon.enabled = false;
    }

    public void ClearUnused() 
    {
        Icon.sprite = null;
        inventory.AddItem(Item);
        Item = null;
    }

}

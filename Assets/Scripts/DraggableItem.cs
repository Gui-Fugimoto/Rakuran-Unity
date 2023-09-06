using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform parentAfterDrag;
    public ItemParameter Item;
    public Inventory inventory;
    public ChestInventory chestInventory;
    public bool OnQuickSlot;
   // public bool InInventory;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
       if(OnQuickSlot == false)
        {
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
        }
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (OnQuickSlot == false)
        {
            transform.position = Input.mousePosition;
        }
            
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(OnQuickSlot == false)
        {
            transform.SetParent(parentAfterDrag);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CraftingCanvas")
        {
            if(collision.GetComponent<PotionCrafting>().ingUsadsos < collision.GetComponent<PotionCrafting>().ingTotal)
            {
                collision.SendMessage("NewItenAdded", Item);
                StartCoroutine(Remove());
            }
        }

        if(collision.tag == "Chest" )
        {
            if(chestInventory == null)
            {
                collision.SendMessage("AddItem", Item);
                StartCoroutine(Remove());
            }

        }

        if (collision.tag == "Inventory")
        {
            if (inventory == null)
            {
                collision.SendMessage("AddItem", Item);
                StartCoroutine(ChestRemove());
            }

        }
        
        if (collision.tag == "QuickSlot" && Item.Consumivel == true)
        {
            collision.SendMessage("AddItem", Item);
            StartCoroutine(Remove());
        }
    }

    IEnumerator Remove()
    {
        yield return new WaitForSeconds(0.01f);
        inventory.RemoveItem(Item);
    }

    IEnumerator ChestRemove()
    {
        yield return new WaitForSeconds(0.01f);
        chestInventory.RemoveItem(Item);
    }
}

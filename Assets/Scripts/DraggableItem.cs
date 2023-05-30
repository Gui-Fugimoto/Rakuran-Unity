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
   // public bool InChest;
   // public bool InInventory;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
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
    }

    IEnumerator Remove()
    {
        yield return new WaitForSeconds(0.2f);
        inventory.RemoveItem(Item);
    }

    IEnumerator ChestRemove()
    {
        yield return new WaitForSeconds(0.2f);
        chestInventory.RemoveItem(Item);
    }
}

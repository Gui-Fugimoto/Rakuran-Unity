using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform parentAfterDrag;
    public ItemParameter Item;
    public Inventory inventory;
    int teste = 1;
    
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
            collision.SendMessage("NewItenAdded", Item);
            StartCoroutine(Remove());
        }
    }

    IEnumerator Remove()
    {
        yield return new WaitForSeconds(0.2f);
        inventory.RemoveItem(Item);
    }
}

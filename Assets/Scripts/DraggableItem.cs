using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform parentAfterDrag;
    public ItemParameter Item;
    public Inventory inventory;
    [SerializeField] bool IsInvSlot;
    [SerializeField] bool IsWeaponEquip;
    public ChestInventory chestInventory;
    [SerializeField] bool IsChestSlot;
    [SerializeField] bool MightOverlap;
    [SerializeField] bool onDestination;
    public EquipWeapon WeaponEquipRef;
    public bool OnQuickSlot;

    private void Start()
    {
        if (IsInvSlot == true)
        {
            inventory = FindObjectOfType<Inventory>();
        }

        if (IsChestSlot == true)
        {
            chestInventory = FindObjectOfType<ChestInventory>();
        }

        if(IsWeaponEquip == true)
        {
            WeaponEquipRef = GetComponentInParent<EquipWeapon>();
        }
    }

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

        if (onDestination == false)
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
        
        transform.SetParent(parentAfterDrag);

        onDestination = false;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CraftingCanvas" && onDestination == false && MightOverlap == false)
        {
            if(collision.GetComponent<PotionCrafting>().ingUsadsos < collision.GetComponent<PotionCrafting>().ingTotal)
            {
                collision.SendMessage("NewItenAdded", Item);
                StartCoroutine(Remove());
            }
        }

        if(collision.tag == "Chest" && onDestination == false && MightOverlap == false)
        {
            if(chestInventory == null)
            {
                collision.SendMessage("AddItem", Item);
                onDestination = true;
                StartCoroutine(Remove());

            }


        }

        if (collision.tag == "Inventory" && onDestination == false)
        {
            if (inventory == null)
            {
                collision.SendMessage("AddItem", Item);
                onDestination = true;
               
                if (WeaponEquipRef != null)
                {
                    WeaponEquipRef.ClearSlot();
                }
               
                StartCoroutine(ChestRemove());
              
            }


        }


        if (collision.tag == "QuickSlot" && Item.Consumivel == true && OnQuickSlot == false && onDestination == false)
        {
            collision.SendMessage("AddItem", Item);
            onDestination = true;
            StartCoroutine(Remove());
        }

        if(collision.tag == "WeaponSlot" && Item.weaponType != WeaponType.None && onDestination == false)
        {
            collision.SendMessage("AddItem", Item);
            onDestination = true;
            StartCoroutine(Remove());
        }

        if(collision.tag == "ForgeSlot" && Item.Forge != Forge.None)
        {
            collision.SendMessage("AddItem", Item);
            onDestination = true;
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

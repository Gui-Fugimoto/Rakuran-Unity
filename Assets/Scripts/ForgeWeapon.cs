using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ForgeWeapon : MonoBehaviour
{
    public Image Icon;
    [SerializeField] Collider2D Box;
    public ItemParameter item;
    public DraggableItem child;
    public Inventory inventory;
    [SerializeField] ForgeResultDisplayer Sword;
    [SerializeField] ForgeResultDisplayer Spear;
    [SerializeField] ForgeResultDisplayer Hammer;

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
        Box.enabled = false;

        if(item.Forge == Forge.TreeZephir)
        {
            Sword.DisplayItem();
            Hammer.DisplayItem();
        }

    }

    public void ClearSlot()
    {
        item = null;

        Icon.sprite = null;
        Icon.enabled = false;
        Box.enabled = true;

        Sword.ClearUnused();
        Hammer.ClearUnused();
        Spear.ClearUnused();
    }

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ForgeResultDisplayer : MonoBehaviour
{
    public Image Icon;
    [SerializeField] Collider2D Box;
    public ItemParameter Arma1;
    //public ItemParameter Arma2;
    public ForgeWeapon ForgeControl;
    public DraggableItem child;
    public Inventory inventory;
    [SerializeField] bool craftable;

    void Start()
    {
        Icon.enabled = false;
        craftable = false;  
    }

    public void DisplayItem()
    {
        Icon.enabled = true;
        Icon.sprite = Arma1.Icon;
        craftable = true;

    }

    public void ClearSlot()
    {
        if(craftable == true)
        {
            inventory.AddItem(Arma1);

            Icon.sprite = null;
            Icon.enabled = false;
            Box.enabled = true;
            craftable = false;

            ForgeControl.ClearSlot();
        }
    }

    public void ClearUnused()
    {
        Icon.sprite = null;
        Icon.enabled = false;
        Box.enabled = true;
        craftable = false;
    }
}

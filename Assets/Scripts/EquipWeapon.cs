using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class EquipWeapon : MonoBehaviour
{
    public Image Icon;
    [SerializeField] Collider2D Box;
    ItemParameter item;
    public DraggableItem child;
    public Inventory inventory;
    public TMP_Text Arma;
    public TMP_Text Dano;
   
    void Start()
    {
        Icon.enabled = false;
        Arma.text = null;
        Dano.text = null;
    }

    public void AddItem(ItemParameter Item)
    {
        item = Item;
        child.Item = Item;

        Icon.sprite = item.Icon;
        Icon.enabled = true;
        Box.enabled = false;
        Arma.text = $"{Item.weaponType}";
        Dano.text = $"{Item.damage}";

    }

    public void ClearSlot()
    {
        inventory.AddItem(item);
        
        item = null;

        Icon.sprite = null;
        Icon.enabled = false;
        Box.enabled = true;
        Arma.text = null;
        Dano.text = null;
    }
}

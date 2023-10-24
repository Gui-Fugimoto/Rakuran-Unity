using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableItem : MonoBehaviour
{
    
    public Image Icon;
    [SerializeField] Collider2D Box;
    [SerializeField] GameObject ThrowPotion;
    [SerializeField] Vector3 Position;
    [SerializeField] Transform PlayerPos;
    [SerializeField] potionThrow potionPass;
    [SerializeField] PlayerController controller;
    ItemParameter item;
    public DraggableItem child;
    public PlayerHealthController Health;
    [SerializeField] KeyCode ConsumeKey;
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

    }

    private void Update()
    {
        Position = PlayerPos.position;
        if (Input.GetKeyDown(ConsumeKey))
        {
            Consume();
        }

    }
    public void Consume()
    {
        if(item.Veneno > item.Vida)
        {
            potionPass = ThrowPotion.GetComponent<potionThrow>();
            potionPass.Item = item;
            Object.Instantiate(ThrowPotion, Position, Quaternion.identity);
            ClearSlot();
        }
        if (item.Veneno < item.Vida)
        {
            Health.ConsumeItem(item);
            ClearSlot();
        }
    }

    public void ClearSlot()
    {
        item = null;

        Icon.sprite = null;
        Icon.enabled = false;
        Box.enabled = true;
    }
}


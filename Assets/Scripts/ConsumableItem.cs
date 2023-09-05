using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableItem : MonoBehaviour
{
    
    public Image Icon;
    [SerializeField] Collider2D Box;
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
        if (Input.GetKeyDown(ConsumeKey))
        {
            Consume();
        }
    }
    public void Consume()
    {
        Health.ConsumeItem(item);
        ClearSlot();
        
    }

    public void ClearSlot()
    {
        item = null;

        Icon.sprite = null;
        Icon.enabled = false;
        Box.enabled = true;
    }
}


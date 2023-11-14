using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public ItemParameter item;
    public DraggableItem child;
    public PlayerHealthController Health;
    [SerializeField] KeyCode ConsumeKey;
    public int Order;
    public SaveFile saveFile;

    private void FixedUpdate()
    {
       if(item != null)
        {
            Debug.Log("ola eu sou o " + gameObject.name + " meu item é "  + item.name);
        }
       if(item == null)
        {
            Debug.Log("ola eu sou o " + gameObject.name + " não tenho item");
        }

    }

    void Start()
    {
        Icon.enabled = false;
        child = GetComponentInChildren<DraggableItem>();
        controller = FindObjectOfType<PlayerController>();
        Health = FindObjectOfType<PlayerHealthController>();
        saveFile = FindObjectOfType<GameController>().Save;

        switch (Order) 
        {
            case 0:
               if(saveFile.QuickSlot == null)
                {
                    Debug.Log("estou vazio");
                    break;
                }
               AddItem(saveFile.QuickSlot);
                Debug.Log("meu item é" + saveFile.QuickSlot3);
                break;

            case 1:
                if (saveFile.QuickSlot1 == null)
                {
                    Debug.Log("estou vazio");
                    break;
                }
                AddItem(saveFile.QuickSlot1);
                Debug.Log("meu item é" + saveFile.QuickSlot3);
                break;

             case 2:
                if (saveFile.QuickSlot2 == null)
                {
                    Debug.Log("estou vazio");
                    break;
                }
                AddItem(saveFile.QuickSlot2);
                Debug.Log("meu item é" + saveFile.QuickSlot3);
                break;

             case 3:
                if (saveFile.QuickSlot3 == null)
                {
                    Debug.Log("estou vazio");
                    break;
                }
                
                AddItem(saveFile.QuickSlot3);
                Debug.Log("meu item é" + saveFile.QuickSlot3);
                break;
                
                

        }

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
    
    public void SaveQuickslot()
    {
        Debug.Log("salvou" + item.name);
        switch (Order)
        {
            case 0:
               
                if(item == null)
                {
                    saveFile.QuickSlot = null;
                    break;
                }

                else
                {
                    saveFile.QuickSlot = item;
                    break;
                }

            case 1:

                if (item == null)
                {
                    saveFile.QuickSlot1 = null;
                    break;
                }

                else
                {
                    saveFile.QuickSlot1 = item;
                    break;
                }

               
            
            case 2:

                if (item == null)
                {
                   saveFile.QuickSlot2 = null;
                    break;
                }

                else
                {
                    saveFile.QuickSlot2 = item;
                    break;
                }

            case 3:
                if (item == null)
                {
                    saveFile.QuickSlot3 = null;
                    break;
                }

                else
                {
                    saveFile.QuickSlot3 = item;
                    break;
                }

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


using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChestInventory : MonoBehaviour
{
    // Start is called before the first frame update
    #region Variáveis

    public List<ItemParameter> itens = new List<ItemParameter>();
    public int inventorySize = 25;
    public Inventory Inventory;
    public GameObject ChestUI;
    public bool Aberto = false;
    public bool PlayerPerto;
    [SerializeField] KeyCode Interact;
    public GameController GameC;
    public SaveFile SaveRef;
    [SerializeField] Pause pauseRef;


    #endregion

    public delegate void MudouItemBau();
    public MudouItemBau MudouItemBauCallback;



    // Update is called once per frame
    private void Start()
    {
        GameC = FindObjectOfType<GameController>();
        pauseRef = FindObjectOfType<Pause>();
        Inventory = FindObjectOfType<Inventory>();
        SaveRef = GameC.Save;
        itens = SaveRef.ChestSaver;
        ChestUI.SetActive(false);
    }

    void Update()
    {
        if (MudouItemBauCallback != null)
            MudouItemBauCallback.Invoke();

        if (Input.GetKeyDown(Interact) && PlayerPerto == true || (Input.GetKeyDown(KeyCode.Escape) && Aberto == true && PlayerPerto == true)) 
        {
            ToggleInventory();
        }
    }

    public void AddItem(ItemParameter item)
    {
        if(itens.Count >= inventorySize)
        {
            Debug.Log("Sem espaço!");
        }
        
        itens.Add(item);

        if (MudouItemBauCallback != null)
            MudouItemBauCallback.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerPerto = true;
        }
    }


   private void OnTriggerExit(Collider other)
   {
       if(other.tag == "Player")
       {
           if(Inventory.Aberto == true)
            {
                Inventory.ToggleJUSTInventory();
            }
           ChestUI.SetActive(false);
           Aberto = false;
           PlayerPerto = false;
           pauseRef.IsMenuOverwritten = false;
       }
   }

    void ToggleInventory()
    {
        if (Aberto == false && pauseRef.GameIsPaused == false)
        {
            Inventory.ToggleJUSTInventory();
            Aberto = true;
            pauseRef.IsMenuOverwritten = true;
            Inventory.Aberto = true;
            ChestUI.SetActive(true);
        }
        else
        {
            StartCoroutine(HoldOnSir());
            Inventory.ToggleJUSTInventory();
            ChestUI.SetActive(false);
            Aberto = false;
        }

    }

    IEnumerator HoldOnSir()
    {
        yield return new WaitForSeconds(0.2f);
        pauseRef.IsMenuOverwritten = false;
    }

    public void RemoveItem(ItemParameter item)
    {
        itens.Remove(item);

        if (MudouItemBauCallback != null)
            MudouItemBauCallback.Invoke();
    }
}


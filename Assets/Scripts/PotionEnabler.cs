using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionEnabler : MonoBehaviour
{
    public GameObject potionUI;
    public Inventory inventory;
    public Pause pauseRef;
    [SerializeField] KeyCode Interact;
    [SerializeField] bool Activate;
    [SerializeField] bool IsOpen;
    
    // Start is called before the first frame update
    void Start()
    {
        potionUI.SetActive(false);
        pauseRef = FindObjectOfType<Pause>();
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.tag == "Player") 
        {
            Activate = true;
            
        }

    }

    private void Update()
    {
        if(Input.GetKeyDown(Interact) && Activate == true || (Input.GetKeyDown(KeyCode.Escape) && IsOpen == true) && Activate == true)
        {
            OpenMenu();
        }

    }

    void OpenMenu()
    {
        if (IsOpen == false && pauseRef.GameIsPaused == false)
        {
            IsOpen = true;
            pauseRef.IsMenuOverwritten = true;
            potionUI.SetActive(true);
            inventory.ToggleJUSTInventory();
        }
        else
        {
            StartCoroutine(HoldOnSir());
            potionUI.SetActive(false);
            inventory.ToggleJUSTInventory();
            IsOpen = false;
            
        }
       
    }

    IEnumerator HoldOnSir()
    {
        yield return new WaitForSeconds(0.2f);
        pauseRef.IsMenuOverwritten = false;
    }


    private void OnTriggerExit(Collider other)
    {
        potionUI.SetActive(false);
        if(inventory.Aberto == true)
        {
            inventory.ToggleJUSTInventory();
        }
        Activate = false;
        pauseRef.IsMenuOverwritten = false;
    }


}  



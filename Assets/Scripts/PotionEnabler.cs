using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionEnabler : MonoBehaviour
{
    public GameObject potionUI;
    public GameObject inventoryUI;
    [SerializeField] KeyCode Interact;
    [SerializeField] bool Activate;
    
    // Start is called before the first frame update
    void Start()
    {
        potionUI.SetActive(false);
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
        if(Input.GetKeyDown(Interact) && Activate == true)
        {
            potionUI.SetActive(true);
            inventoryUI.SetActive(true);
        }

    }




    private void OnTriggerExit(Collider other)
    {
        potionUI.SetActive(false);
        inventoryUI.SetActive(false);
        Activate = false;
    }


}  



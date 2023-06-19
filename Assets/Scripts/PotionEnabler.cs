using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionEnabler : MonoBehaviour
{
    public GameObject potionUI;
    public GameObject inventoryUI;
    public bool Grab;
    
    // Start is called before the first frame update
    void Start()
    {
        potionUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Grab = true;
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            Grab = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Player" && Grab == true)
        {
            if (other.GetComponent<Inventory>().itens.Count < other.GetComponent<Inventory>().inventorySize)
            {
                potionUI.SetActive(true);
                inventoryUI.SetActive(true);
                Debug.Log("Pegou Iten");
            }
        }

    }
}



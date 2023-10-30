using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public SaveFile Save;
    public bool Activate;
    public Inventory pInv;
    [SerializeField] KeyCode Interact;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Save = other.GetComponent<PlayerController>().currentSave;
            pInv = other.GetComponent<Inventory>();
            Activate = true;
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Interact) && Activate == true)
        {
            Save.CPpos = gameObject.transform.position;
            Save.Invsave = pInv.itens;
        }
    }
}

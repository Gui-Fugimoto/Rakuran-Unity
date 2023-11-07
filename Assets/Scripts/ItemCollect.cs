using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{
    #region variáveis
    [SerializeField] bool Grab;
    [SerializeField] ItemParameter Item;
    [SerializeField] bool Collected;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Grab = true;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            Grab = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Grab == true && Collected == false)
        {
            if(other.GetComponent<Inventory>().itens.Count < other.GetComponent<Inventory>().inventorySize)
            {
                StartCoroutine(Die());
                other.gameObject.SendMessage("AddItem", Item);
                Debug.Log("Pegou Iten");
                Collected = true;
            }
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

}

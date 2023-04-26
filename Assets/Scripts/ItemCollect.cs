using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{
    #region variáveis
    [SerializeField] bool Grab;
    bool Foipego;
    [SerializeField] ItemParameter Item;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Grab = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && Grab == true)
        {

            if(other.GetComponent<Inventory>().itens.Count < other.GetComponent<Inventory>().inventorySize)
            {
                StartCoroutine(Die());
                other.gameObject.SendMessage("AddItem", Item);
                Debug.Log("Pegou Iten");
            }
            
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

}

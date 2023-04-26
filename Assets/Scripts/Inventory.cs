using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    #region Variáveis

    public List<ItemParameter> itens = new List<ItemParameter>();
    public int inventorySize = 3;

    #endregion

    public delegate void MudouItem();
    public MudouItem MudouItemCallback;

    // Update is called once per frame
    public void AddItem(ItemParameter item)
    {
        if(itens.Count >= inventorySize)
        {
            Debug.Log("Sem espaço!");
        }
        
        itens.Add(item);

        if (MudouItemCallback != null)
            MudouItemCallback.Invoke();
    }

    public void RemoveItem(ItemParameter item)
    {
        itens.Remove(item);

        if (MudouItemCallback != null)
            MudouItemCallback.Invoke();
    }
}

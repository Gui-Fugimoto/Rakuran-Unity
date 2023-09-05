using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IngredientExam : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public StatsDisplayer StatsDisplayer;
    public InventorySlot InventorySlot;
    public GameObject Examiner;
    
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Saiu");
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
       if(InventorySlot.item != null)
        {
            StatsDisplayer.Item = InventorySlot.item;
            StatsDisplayer.Self.SetActive(true);
        }
        Debug.Log("Entrou");
    }
}

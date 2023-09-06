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


    private void Start()
    {
        Examiner.SetActive(false);
    }
    public void OnPointerExit(PointerEventData eventData)
    {

        StatsDisplayer.EndInspect();
        Examiner.SetActive(false);
       
        Debug.Log("Saiu");
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
       if(InventorySlot.item != null)
        {
            StatsDisplayer.Self.SetActive(true);
            StatsDisplayer.OnInspect(InventorySlot.item);
        }
        Debug.Log("Entrou");
    }
}

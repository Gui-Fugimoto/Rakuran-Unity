using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class StatsDisplayer : MonoBehaviour
{
    public ItemParameter ItemSelf;
    public GameObject Self;
    public TMP_Text Name;
    public TMP_Text Description;

    //[SerializeField] Effect effect;

    private void Start()
    {
        Self.SetActive(false);
    }

    public void OnInspect(ItemParameter Item)
    {
        Name.text = $"{Item.name}";
        Description.text = $"{Item.Desc}";
    }

    public void EndInspect()
    {
        Name.text = null;
        Description.text = null;
    }

    private void Update()
    {


    }
}

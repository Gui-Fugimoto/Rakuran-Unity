using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "ItemParameter")]

public class ItemParameter : ScriptableObject
{
    [SerializeField] GameObject ItemMundo = default;
    [SerializeField] GameObject ItemInventario = default;

    //public GameObject ItemMundo => itemmundo;
    //public int DanoPesado => danoPesado;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionCrafting : MonoBehaviour
{
    [SerializeField] int vida;
    [SerializeField] int escudo;
    [SerializeField] int veneno;
    [SerializeField] int ingUsadsos;
    [SerializeField] int ingTotal;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void NewItenAdded(ItemParameter Item)
    {

        vida += Item.Vida;
        escudo += Item.Escudo;
        veneno += Item.Veneno;

        ingUsadsos++;
    }
}

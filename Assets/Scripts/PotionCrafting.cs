using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionCrafting : MonoBehaviour
{
    [SerializeField] int vida;
    [SerializeField] int escudo;
    [SerializeField] int veneno;
    public int ingUsadsos;
    public int ingTotal;
    public Inventory inventory;
    [SerializeField] ItemParameter PotCura;
    [SerializeField] ItemParameter PotEscudo;
    [SerializeField] ItemParameter PotVeneno;

    void NewItenAdded(ItemParameter Item)
    {

        vida += Item.Vida;
        escudo += Item.Escudo;
        veneno += Item.Veneno;

        ingUsadsos++;
    }

    public void FinishPotion()
    {
        if(vida > escudo && vida > escudo)
        {
            inventory.AddItem(PotCura);
            vida = 0;
            escudo = 0;
            veneno = 0;
        }
        if(escudo > vida && escudo > veneno)
        {
            inventory.AddItem(PotEscudo);
            vida = 0;
            escudo = 0;
            veneno = 0;
        }
        if(veneno > vida && veneno > escudo)
        {
            inventory.AddItem(PotVeneno);
            vida = 0;
            escudo = 0;
            veneno = 0;
        }
    }
}

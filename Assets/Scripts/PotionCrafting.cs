using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionCrafting : MonoBehaviour
{
    [SerializeField] float vida;
    [SerializeField] float escudo;
    [SerializeField] float veneno;
    public int ingUsadsos;
    public int ingTotal;
    public Inventory inventory;
    [SerializeField] ItemParameter PotCura;
    [SerializeField] ItemParameter PotEscudo;
    [SerializeField] ItemParameter PotVeneno;
    [SerializeField] Image HealthMeter;
    [SerializeField] Image PoisonMeter;
    [SerializeField] Image ShieldMeter;
    [SerializeField] KeyCode FinishPotionKey;

    private void Update()
    {
        if (Input.GetKeyUp(FinishPotionKey))
        {
            FinishPotion();
        }

        HealthMeter.fillAmount = vida/10;
        ShieldMeter.fillAmount = escudo/10;
        PoisonMeter.fillAmount = veneno/10;
    }

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
            ingUsadsos = 0;
        }
        if(escudo > vida && escudo > veneno)
        {
            inventory.AddItem(PotEscudo);
            vida = 0;
            escudo = 0;
            veneno = 0;
            ingUsadsos = 0;
        }
        if(veneno > vida && veneno > escudo)
        {
            inventory.AddItem(PotVeneno);
            vida = 0;
            escudo = 0;
            veneno = 0;
            ingUsadsos = 0;
        }
    }
}

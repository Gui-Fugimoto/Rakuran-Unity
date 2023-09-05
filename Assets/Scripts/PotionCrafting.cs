using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionCrafting : MonoBehaviour
{
    [SerializeField] float vida;
    [SerializeField] float veneno;
    [SerializeField] Effect Effect;
    public int ingUsadsos;
    public int ingTotal;
    public Inventory inventory;
    [SerializeField] ItemParameter PotCura;
    [SerializeField] ItemParameter PotEscudo;
    [SerializeField] ItemParameter PotVeneno;
    [SerializeField] Image HealthMeter;
    [SerializeField] Image PoisonMeter;
    [SerializeField] Image EffectIcon;
    [SerializeField] KeyCode FinishPotionKey;

    private void Start()
    {
        EffectIcon.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyUp(FinishPotionKey))
        {
            FinishPotion();
        }

        HealthMeter.fillAmount = vida/10;
        PoisonMeter.fillAmount = veneno/10;

    }

    void NewItenAdded(ItemParameter Item)
    {

        vida += Item.Vida;
        veneno += Item.Veneno;
        
        if(Item.Effect != Effect.None)
        {
            EffectIcon.enabled = true;
            Effect = Item.Effect;
            EffectIcon.sprite = Item.EffectIcon;
        }
        
        ingUsadsos++;
    }

    public void FinishPotion()
    {
        if(vida > veneno)
        {
            inventory.AddItem(PotCura);
            vida = 0;
            veneno = 0;
            ingUsadsos = 0;
        }
        if(veneno > vida)
        {
            inventory.AddItem(PotVeneno);
            vida = 0;
            veneno = 0;
            ingUsadsos = 0;
        }
    }
}

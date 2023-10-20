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
#region poções    
    [SerializeField] ItemParameter PotCuraSimples;
    [SerializeField] ItemParameter PotRecuperaSimples;
    [SerializeField] ItemParameter PotVenenoSimples;
    [SerializeField] ItemParameter PotVenenoOTSimples;
    [SerializeField] ItemParameter PotCuraMed;
    [SerializeField] ItemParameter PotRecuperaMed;
    [SerializeField] ItemParameter PotVenenoMed;
    [SerializeField] ItemParameter PotVenenoOTMed;
    #endregion
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
       if(Effect == Effect.None)
        {
            if (vida == 1 && veneno <= 0)
            {
                inventory.AddItem(PotCuraSimples);
                vida = 0;
                veneno = 0;
                Effect = Effect.None;
                EffectIcon.enabled = false;
                ingUsadsos = 0;
            }
            if (veneno == 1 && vida <= 0)
            {
                inventory.AddItem(PotVenenoSimples);
                vida = 0;
                veneno = 0;
                Effect = Effect.None;
                EffectIcon.enabled = false;
                ingUsadsos = 0;
            }

            if (vida >= 2 && veneno <= 0)
            {
                inventory.AddItem(PotCuraMed);
                vida = 0;
                veneno = 0;
                Effect = Effect.None;
                EffectIcon.enabled = false;
                ingUsadsos = 0;
            }
            if (veneno >= 2 && vida <= 0)
            {
                inventory.AddItem(PotVenenoMed);
                vida = 0;
                veneno = 0;
                Effect = Effect.None;
                EffectIcon.enabled = false;
                ingUsadsos = 0;
            }
           
            if (veneno > 0 && vida > 0)
            {
                Debug.Log("poção Falhou");
                vida = 0;
                veneno = 0;
                Effect = Effect.None;
                EffectIcon.enabled = false;
                ingUsadsos = 0;
            }
        }

       if (Effect == Effect.OverTime)
        {
            if (vida == 1 && veneno <= 0)
            {
                inventory.AddItem(PotRecuperaSimples);
                vida = 0;
                veneno = 0;
                Effect = Effect.None;
                EffectIcon.enabled = false;
                ingUsadsos = 0;
            }
            if (veneno == 1 && vida <= 0)
            {
                inventory.AddItem(PotVenenoOTSimples);
                vida = 0;
                veneno = 0;
                Effect = Effect.None;
                EffectIcon.enabled = false;
                ingUsadsos = 0;
            }

            if (vida >= 2 && veneno <= 0)
            {
                inventory.AddItem(PotRecuperaMed);
                vida = 0;
                veneno = 0;
                Effect = Effect.None;
                EffectIcon.enabled = false;
                ingUsadsos = 0;
            }
            if (veneno >= 2 && vida <= 0)
            {
                inventory.AddItem(PotVenenoOTMed);
                vida = 0;
                veneno = 0;
                Effect = Effect.None;
                EffectIcon.enabled = false;
                ingUsadsos = 0;
            }

            if (veneno > 0 && vida > 0)
            {
                Debug.Log("poção Falhou");
                vida = 0;
                veneno = 0;
                Effect = Effect.None;
                EffectIcon.enabled = false;
                ingUsadsos = 0;
            }
        }
        
    }
}

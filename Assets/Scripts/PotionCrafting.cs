using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
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
    [SerializeField] ItemParameter PotResist;
    [SerializeField] ItemParameter PotFraqueza;
    [SerializeField] ItemParameter PotSlow;
    [SerializeField] ItemParameter PotSpeed;
    [SerializeField] ItemParameter PotStun;
    [SerializeField] ItemParameter PotInvis;
    #endregion
    [SerializeField] IngridientDisplayer Display1;
    [SerializeField] IngridientDisplayer Display2;
    [SerializeField] IngridientDisplayer Display3;
    [SerializeField] KeyCode FinishPotionKey;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    private void Update()
    {
        //  if (Input.GetKeyUp(FinishPotionKey))
        //  {
        //      FinishPotion();
        //  }
        //
    }

    void NewItenAdded(ItemParameter Item)
    {

        vida += Item.Vida;
        veneno += Item.Veneno;

        if (Item.Effect != Effect.None)
        {
            Effect = Item.Effect;
        }

        switch (ingUsadsos)
        {
            case 0:
                Display1.AddItem(Item);
                ingUsadsos++;
                break;
            case 1:
                Display2.AddItem(Item);
                ingUsadsos++;
                break;
            case 2:
                Display3.AddItem(Item);
                ingUsadsos++;
                break;
        }
    }

    public void FinishPotion()
    {
        if (ingUsadsos == ingTotal)
        {
            #region Sem Efeito
            if (Effect == Effect.None)
            {
                if (vida == 1 && veneno <= 0)
                {
                    inventory.AddItem(PotCuraSimples);
                    vida = 0;
                    veneno = 0;
                    Effect = Effect.None;
                    Display1.ClearSlot();
                    Display2.ClearSlot();
                    Display3.ClearSlot();
                    ingUsadsos = 0;
                }
                if (veneno == 1 && vida <= 0)
                {
                    inventory.AddItem(PotVenenoSimples);
                    vida = 0;
                    veneno = 0;
                    Effect = Effect.None;
                    Display1.ClearSlot();
                    Display2.ClearSlot();
                    Display3.ClearSlot();
                    ingUsadsos = 0;
                }

                if (vida >= 2 && veneno <= 0)
                {
                    inventory.AddItem(PotCuraMed);
                    vida = 0;
                    veneno = 0;
                    Effect = Effect.None;
                    Display1.ClearSlot();
                    Display2.ClearSlot();
                    Display3.ClearSlot();
                    ingUsadsos = 0;
                }
                if (veneno >= 2 && vida <= 0)
                {
                    inventory.AddItem(PotVenenoMed);
                    vida = 0;
                    veneno = 0;
                    Effect = Effect.None;
                    Display1.ClearSlot();
                    Display2.ClearSlot();
                    Display3.ClearSlot();
                    ingUsadsos = 0;
                }

                if (veneno > 0 && vida > 0 || veneno < 0 && vida < 0)
                {
                    Debug.Log("poção Falhou");
                    vida = 0;
                    veneno = 0;
                    Effect = Effect.None;
                    Display1.ClearUnused();
                    Display2.ClearUnused();
                    Display3.ClearUnused();
                    ingUsadsos = 0;
                }
            }
            #endregion
            #region Overtime
            if (Effect == Effect.OverTime)
            {
                if (vida == 1 && veneno <= 0)
                {
                    inventory.AddItem(PotRecuperaSimples);
                    vida = 0;
                    veneno = 0;
                    Effect = Effect.None;
                    Display1.ClearSlot();
                    Display2.ClearSlot();
                    Display3.ClearSlot();
                    ingUsadsos = 0;
                }
                if (veneno == 1 && vida <= 0)
                {
                    inventory.AddItem(PotVenenoOTSimples);
                    vida = 0;
                    veneno = 0;
                    Effect = Effect.None;
                    Display1.ClearSlot();
                    Display2.ClearSlot();
                    Display3.ClearSlot();
                    ingUsadsos = 0;
                }

                if (vida >= 2 && veneno <= 0)
                {
                    inventory.AddItem(PotRecuperaMed);
                    vida = 0;
                    veneno = 0;
                    Effect = Effect.None;
                    Display1.ClearSlot();
                    Display2.ClearSlot();
                    Display3.ClearSlot();
                    ingUsadsos = 0;
                }
                if (veneno >= 2 && vida <= 0)
                {
                    inventory.AddItem(PotVenenoOTMed);
                    vida = 0;
                    veneno = 0;
                    Effect = Effect.None;
                    Display1.ClearSlot();
                    Display2.ClearSlot();
                    Display3.ClearSlot();
                    ingUsadsos = 0;
                }

                if (veneno > 0 && vida > 0 || veneno < 0 && vida < 0)
                {
                    Debug.Log("poção Falhou");
                    vida = 0;
                    veneno = 0;
                    Effect = Effect.None;
                    Display1.ClearUnused();
                    Display2.ClearUnused();
                    Display3.ClearUnused();
                    ingUsadsos = 0;
                }
            }
            #endregion
            #region Resist
            if (Effect == Effect.Resist)
            {
                if (vida >= 1 && veneno <= 0)
                {
                    inventory.AddItem(PotResist);
                    vida = 0;
                    veneno = 0;
                    Effect = Effect.None;
                    Display1.ClearSlot();
                    Display2.ClearSlot();
                    Display3.ClearSlot();
                    ingUsadsos = 0;
                }
                if (veneno >= 1 && vida <= 0)
                {
                    inventory.AddItem(PotFraqueza);
                    vida = 0;
                    veneno = 0;
                    Effect = Effect.None;
                    Display1.ClearSlot();
                    Display2.ClearSlot();
                    Display3.ClearSlot();
                    ingUsadsos = 0;
                }

                if (veneno > 0 && vida > 0 || veneno < 0 && vida < 0)
                {
                    Debug.Log("poção Falhou");
                    vida = 0;
                    veneno = 0;
                    Effect = Effect.None;
                    Display1.ClearUnused();
                    Display2.ClearUnused();
                    Display3.ClearUnused();
                    ingUsadsos = 0;
                }
            }
            #endregion
            #region Speed
            if (Effect == Effect.Speed)
            {
                if (vida >= 1 && veneno <= 0)
                {
                    inventory.AddItem(PotSpeed);
                    vida = 0;
                    veneno = 0;
                    Effect = Effect.None;
                    Display1.ClearSlot();
                    Display2.ClearSlot();
                    Display3.ClearSlot();
                    ingUsadsos = 0;
                }
                if (veneno >= 1 && vida <= 0)
                {
                    inventory.AddItem(PotSlow);
                    vida = 0;
                    veneno = 0;
                    Effect = Effect.None;
                    Display1.ClearSlot();
                    Display2.ClearSlot();
                    Display3.ClearSlot();
                    ingUsadsos = 0;
                }

                if (veneno > 0 && vida > 0 || veneno < 0 && vida < 0)
                {
                    Debug.Log("poção Falhou");
                    vida = 0;
                    veneno = 0;
                    Effect = Effect.None;
                    Display1.ClearUnused();
                    Display2.ClearUnused();
                    Display3.ClearUnused();
                    ingUsadsos = 0;
                }
            }
            #endregion

            #region Invis
            if (Effect == Effect.Invis)
            {
                if (vida >= 1 && veneno <= 0)
                {
                    Debug.Log(" kill me rn");
                    inventory.AddItem(PotInvis);
                    vida = 0;
                    veneno = 0;
                    Effect = Effect.None;
                    Display1.ClearSlot();
                    Display2.ClearSlot();
                    Display3.ClearSlot();
                    ingUsadsos = 0;
                }
                if (veneno >= 1 && vida <= 0)
                {
                    Debug.Log(" kill me rn");
                    inventory.AddItem(PotStun);
                    vida = 0;
                    veneno = 0;
                    Effect = Effect.None;
                    Display1.ClearSlot();
                    Display2.ClearSlot();
                    Display3.ClearSlot();
                    ingUsadsos = 0;
                }

                if (veneno > 0 && vida > 0 || veneno < 0 && vida < 0)
                {
                    Debug.Log("poção Falhou");
                    vida = 0;
                    veneno = 0;
                    Effect = Effect.None;
                    Display1.ClearUnused();
                    Display2.ClearUnused();
                    Display3.ClearUnused();
                    ingUsadsos = 0;
                }
                #endregion
            }

            #region Invis
            if (Effect == Effect.Invis)
            {
                if (vida >= 1 && veneno <= 0)
                {
                    Debug.Log(" kill me rn");
                    inventory.AddItem(PotInvis);
                    vida = 0;
                    veneno = 0;
                    Effect = Effect.None;
                    Display1.ClearSlot();
                    Display2.ClearSlot();
                    Display3.ClearSlot();
                    ingUsadsos = 0;
                }
                if (veneno >= 1 && vida <= 0)
                {
                    Debug.Log(" kill me rn");
                    inventory.AddItem(PotStun);
                    vida = 0;
                    veneno = 0;
                    Effect = Effect.None;
                    Display1.ClearSlot();
                    Display2.ClearSlot();
                    Display3.ClearSlot();
                    ingUsadsos = 0;
                }

                if (veneno > 0 && vida > 0 || veneno < 0 && vida < 0)
                {
                    Debug.Log("poção Falhou");
                    vida = 0;
                    veneno = 0;
                    Effect = Effect.None;
                    Display1.ClearUnused();
                    Display2.ClearUnused();
                    Display3.ClearUnused();
                    ingUsadsos = 0;
                }

                #endregion
            }
        }
    }
}

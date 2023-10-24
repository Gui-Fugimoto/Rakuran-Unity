using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class StatsDisplayer : MonoBehaviour
{
    public Image HealthMeter;
    public Image PoisonMeter;
    public float Vida;
    public float Veneno;
    public Image ItemIcon;
    public ItemParameter ItemSelf;
    public Image EffectSprite;
    public GameObject Self;
    //[SerializeField] Effect effect;

    private void Start()
    {
        Self.SetActive(false);
    }

    public void OnInspect(ItemParameter Item)
    {
        Vida = Item.Vida;
        Veneno = Item.Veneno;
        ItemIcon.sprite = Item.Icon;
        EffectSprite.sprite = Item.EffectIcon;
        EffectSprite.color = new Color(1f, 1f, 1f, 1f);
       
        if(Item.Effect == Effect.None)
        {
            EffectSprite.color = new Color(1f, 1f, 1f, 0f);
        }
    }

    public void EndInspect()
    {
        Vida = 0;
        Veneno = 0;
        EffectSprite.color = new Color(1f,1f,1f,0f);
        ItemIcon.sprite = null;
    }

    private void Update()
    {
        HealthMeter.fillAmount = Vida/10;
        PoisonMeter.fillAmount = Veneno/10;
    }
}

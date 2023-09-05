using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplayer : MonoBehaviour
{
    public Image HealthMeter;
    public Image PoisonMeter;
    public Image ItemIcon;
    public ItemParameter Item;
    public Image Effect;
    public GameObject Self;
    void Start()
    {
        Self.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        HealthMeter.fillAmount = Item.Vida / 5; 
        PoisonMeter.fillAmount = Item.Veneno / 5;
        Effect.sprite = Item.EffectIcon;
        ItemIcon.sprite = Item.Icon;
    }
}

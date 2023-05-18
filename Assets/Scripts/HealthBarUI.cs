using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] PlayerHealthController playerHPScript;
    [SerializeField] private Image healthBarSprite;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBarSprite.fillAmount = playerHPScript.currentHP / playerHPScript.maxHP;
    }
}

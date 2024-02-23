using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField] public float currentHp;
    [SerializeField] public float maxHp;
    [SerializeField] private Image healthBarSprite;
    [HideInInspector] public EnemyLife enemyLifeScript;
    

    // Update is called once per frame
    void Update()
    {
        if(enemyLifeScript != null)
        {
            PickHealthInfo();
            healthBarSprite.fillAmount = currentHp / maxHp;
        }
       
    }

    void PickHealthInfo()
    {
        currentHp = enemyLifeScript.vida;
        maxHp = enemyLifeScript.vidaMax;
    }
}

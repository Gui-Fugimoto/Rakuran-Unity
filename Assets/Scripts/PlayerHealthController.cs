using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public float currentHP;
    public float maxHP;
    public GameController gameC;
    // Start is called before the first frame update
    void Awake()
    {
        currentHP = maxHP;
        gameC = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDeath();

        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        GetComponentInParent<SimpleFlash>().Flash();
    }
     public void ConsumeItem(ItemParameter consumed)
    {
        if(consumed.Vida >= 00 && currentHP != maxHP)
        {
            currentHP += consumed.Vida;
        }
        if(consumed.Veneno >= 00)
        {
            currentHP -= consumed.Veneno;
        }
    }
    
    void PlayerDeath()
    {
        if (currentHP <= 0)
        {
            gameC.GameOver();
            
        }
    }
}

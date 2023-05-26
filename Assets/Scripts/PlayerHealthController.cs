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

    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        GetComponentInParent<SimpleFlash>().Flash();
    }

    void PlayerDeath()
    {
        if (currentHP <= 0)
        {
            gameC.GameOver();
            
        }
    }
}

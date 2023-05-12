using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] int vida;
    void Start()
    {
        
    }
    void Update()
    {
        if(vida <= 0)
        {
            Debug.Log("morreu");
        }
    }
    public void Damage(int str)
    {
        vida -= str;
        Debug.Log("tomou dano, vida atual " + (vida));

        GetComponentInParent<SimpleFlash>().Flash();
    }
}

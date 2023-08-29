using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] float vida;
    void Start()
    {
        
    }
    void Update()
    {
        if(vida <= 0)
        {
            Debug.Log("morreu");
            Destroy(gameObject, 0.1f);
        }
    }
    public void Damage(float str)
    {
        vida -= str;
        Debug.Log("tomou dano, vida atual " + (vida));

        GetComponentInParent<SimpleFlash>().Flash();
    }
}

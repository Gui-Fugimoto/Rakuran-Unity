using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] float vida;
    [SerializeField] bool damageCD = false;
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
        if (!damageCD)
        {
            damageCD = true;
            vida -= str;
            Debug.Log("tomou dano, vida atual " + (vida));
            GetComponentInParent<SimpleFlash>().Flash();
            StartCoroutine(ResetCooldown());
        }
        
    }

    IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        damageCD = false;
    }
}

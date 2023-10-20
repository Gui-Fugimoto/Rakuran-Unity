using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] float vida;
    [SerializeField] bool damageCD = false;
    [SerializeField] float Count;
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
    
    public void DamageOT(float str)
    {
        InvokeRepeating("OvertimePoison", 0.0f, 1f);
        Count = str;
    }

    void OvertimePoison()
    {
        if (Count > 0)
        {
            vida -= 1;
            GetComponentInParent<SimpleFlash>().Flash();
            Count -= 1;
        }

        if (Count == 0)
        {
            CancelInvoke("OvertimePoison");
        }

    }

    IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        damageCD = false;
    }
}

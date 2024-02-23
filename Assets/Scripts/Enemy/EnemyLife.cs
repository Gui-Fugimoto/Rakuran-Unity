using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class EnemyLife : MonoBehaviour
{
    public float vida;
    public float vidaMax;
    [SerializeField] bool damageCD = false;
    [SerializeField] float Count;
    [SerializeField] float Resist;
    [SerializeField] NavMeshAgent Enemy;
    private QuestObjectiveTrigger qTrigger;
    private bool once = true;
    void Start()
    {
        Enemy = GetComponent<NavMeshAgent>();
        qTrigger = GetComponent<QuestObjectiveTrigger>();
        vidaMax = vida;
    }
    void Update()
    {
        if(vida <= 0)
        {
            Debug.Log("morreu");
            if(once == true && qTrigger != null)
            {
                once = false;
                qTrigger.OnDefeat();
            }
            
            Destroy(gameObject, 0.1f);
        }
        if(vida > vidaMax)
        {
            vida = vidaMax;
        }
    }
    public void Damage(float str)
    {
        if (!damageCD)
        {
            damageCD = true;
            vida -= (str - Resist) ;
            Debug.Log("tomou dano, vida atual " + (vida));
            StartCoroutine(ResetCooldown());
            GetComponentInParent<SimpleFlash>().Flash();
            
        }
        
    }
    
    public void DamageOT(float str)
    {
        InvokeRepeating("OvertimePoison", 0.0f, 1f);
        Count = str;
    }

    public void ResistPerda()
    {
        StartCoroutine(ResistDebuff());
    }

    public void Slow()
    {
        StartCoroutine(SlowDebuff());
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

    IEnumerator ResistDebuff()
    {
        Resist = -2;
        yield return new WaitForSeconds(30f);
        Resist = 0;
    }

    IEnumerator SlowDebuff()
    {
        Enemy.speed = Enemy.speed / 2;
        yield return new WaitForSeconds(10f);
        Enemy.speed = Enemy.speed * 2;
    }
}

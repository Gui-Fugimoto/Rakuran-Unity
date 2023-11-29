using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventHitbox : MonoBehaviour
{
    public EnemyNavMeshAgent enNavMeshAgent;
    public EnemySimples enSimples;
    public EnemyGranadier enGranadier;
    void Start()
    {
        enNavMeshAgent = GetComponentInParent<EnemyNavMeshAgent>();
        enSimples = GetComponent<EnemySimples>();
        enGranadier = GetComponent<EnemyGranadier>();
    }

    
    public void AnimEnableHitbox()
    {
        if(enNavMeshAgent != null)
        {
            enNavMeshAgent.EnableHitbox();
        }
        if(enSimples != null)
        {
            enSimples.EnableHitbox();
        }
        if (enGranadier != null)
        {
            enGranadier.InstTiro();
        }

    }
    public void AnimEnableSpecialHitbox()
    {
        if (enNavMeshAgent != null)
        {
            enNavMeshAgent.EnableSpecialHitbox();
        }
        if (enSimples != null)
        {
            enSimples.EnableSpecialHitbox();
        }
        if (enGranadier != null)
        {
            enGranadier.InstRocketPrefab();
        }
    }
    public void AnimDisableHitbox()
    {
        if (enNavMeshAgent != null)
        {
            enNavMeshAgent.DisableHitbox();
        }
        if (enSimples != null)
        {
            enSimples.DisableHitbox();
        }
        if (enGranadier != null)
        {
            enGranadier.DisableHitbox();
        }
    }

    public void AnimDisableSpecialHitbox()
    {
        if (enNavMeshAgent != null)
        {
            enNavMeshAgent.DisableSpecialHitbox();
        }
        if (enSimples != null)
        {
            enSimples.DisableSpecialHitbox();
        }
        if (enGranadier != null)
        {
            enGranadier.SpecialExit();
        }
    }

    public void DisableHitboxSimple()
    {
        if (enNavMeshAgent != null)
        {
            enNavMeshAgent.KeepDisableHitbox();
        }
        if (enSimples != null)
        {
            enSimples.KeepDisableHitbox();
        }
        if (enGranadier != null)
        {
            enGranadier.KeepDisableHitbox();
        }
    }
}

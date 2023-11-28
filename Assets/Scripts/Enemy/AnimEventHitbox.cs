using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventHitbox : MonoBehaviour
{
    public EnemyNavMeshAgent enNavMeshAgent;
    void Start()
    {
        enNavMeshAgent = GetComponentInParent<EnemyNavMeshAgent>();
    }

    
    public void AnimEnableHitbox()
    {
        enNavMeshAgent.EnableHitbox();
    }
    public void AnimEnableSpecialHitbox()
    {
        enNavMeshAgent.EnableSpecialHitbox();
    }
    public void AnimDisableHitbox()
    {
        enNavMeshAgent.DisableHitbox();
    }

    public void AnimDisableSpecialHitbox()
    {
        enNavMeshAgent.DisableSpecialHitbox();
    }
}

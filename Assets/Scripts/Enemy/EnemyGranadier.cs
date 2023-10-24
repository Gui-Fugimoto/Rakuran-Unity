using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGranadier : EnemyNavMeshAgent
{
    public GameObject rocketTargetPrefab;
  

    protected new IEnumerator SpecialAttack() 
    {
      
        if (!isAttacking && timeSinceLastAttack >= attackCooldown)
        {
            CDcontrol = 0f;
            anim.SetBool("Attack", true);
            anim.SetBool("Walk", false);
            yield return new WaitForSeconds(1f);
            GameObject followObject = Instantiate(rocketTargetPrefab, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(5f);
            //animação nova
        }


    }
}

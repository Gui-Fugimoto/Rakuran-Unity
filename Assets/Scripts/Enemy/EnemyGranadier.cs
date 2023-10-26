using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGranadier : EnemyNavMeshAgent
{
    public GameObject rocketTargetPrefab;
    private bool once = true;

    protected override IEnumerator SpecialAttack() 
    {
        if (!isAttacking && timeSinceLastAttack >= attackCooldown)
        {
            
            if (once)
            {
                once = false;
                CDcontrol = 0f;
                anim.SetBool("Attack", true);
                anim.SetBool("Walk", false);
                yield return new WaitForSeconds(1f);
                GameObject followObject = Instantiate(rocketTargetPrefab, player.transform.position, Quaternion.identity);
                Debug.Log("Instanciou");
                yield return new WaitForSeconds(5f);
                StartCoroutine(SpecialExit());
                //animação nova
            }

        }


    }

    IEnumerator SpecialExit()
    {
        StartCoroutine(EndAttack());
        yield return new WaitForSeconds(0.5f);
        once = true;
    }
}

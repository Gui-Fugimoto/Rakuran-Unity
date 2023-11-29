using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGranadier : EnemyNavMeshAgent
{
    public GameObject rocketTargetPrefab;
    private bool once = true;
    public GameObject projetilBasico;

    protected override void AttackState()
    {
        navMeshAgent.isStopped = true;
        if (!isAttacking && timeSinceLastAttack >= attackCooldown)
        {
            isAttacking = true;
            timeSinceLastAttack = 0f;
            anim.SetTrigger("Attack");
            
            //StartCoroutine(EndAttack(atkEndDelay));         
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        Vector3 directionToPlayer = player.transform.position - transform.position;
        if (distanceToPlayer > attackRange || Mathf.Abs(directionToPlayer.z) >= Mathf.Abs(directionToPlayer.x))
        {
            pursuitTimer = Time.time;
            currentState = 1;
        }
    }
    protected override void SpecialAttack()
    {
        if (!isAttacking && timeSinceLastAttack >= attackCooldown)
        {

            if (once)
            {
                StartCoroutine(GranadierSpecial());
            }

        }


    }
    IEnumerator GranadierSpecial()
    {
        once = false;
        CDcontrol = 0f;
        anim.SetTrigger("Special");
        anim.SetBool("Walk", false);
        yield return new WaitForSeconds(1f);
        GameObject followObject = Instantiate(rocketTargetPrefab, player.transform.position, Quaternion.identity);
        Debug.Log("Instanciou");
        yield return new WaitForSeconds(5f);
        StartCoroutine(SpecialExit());
        //animação nova
    }

    IEnumerator SpecialExit()
    {
        //StartCoroutine(EndAttack(atkEndDelay));
        yield return new WaitForSeconds(0.5f);
        once = true;
    }

    public void InstTiro()
    {
        if (once)
        {
            once = false;
            GameObject tiroObject = Instantiate(projetilBasico, transform.position, Quaternion.identity);
        }
    }
}

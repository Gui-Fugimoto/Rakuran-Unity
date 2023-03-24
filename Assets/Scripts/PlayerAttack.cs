using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region Variaveis
    [SerializeField] GameObject AttackBox;
    [SerializeField] int Combo;
    [SerializeField] bool Cooldown;
    [SerializeField] Animator anim;
    [SerializeField] float FirstAttackTimer;
    [SerializeField] float AttackTimer;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        AttackBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
            AttackTimer = Time.time;
        }
    }

    private void Attack()
    {
        if (Combo == 0 && Cooldown == false)
        {
            anim.SetTrigger("Attack");
            Debug.Log("Atacou kk");
            Combo++;
            StartCoroutine(ClickCooldown());
            Cooldown = true;
            FirstAttackTimer = Time.time;
        }

        if (Combo == 1 && Cooldown == false && FirstAttackTimer - AttackTimer < 3f)
        {
            anim.SetTrigger("Attack");
            Debug.Log("Ataque 2");
            Combo++;
            StartCoroutine(ClickCooldown());
            Cooldown = true;
        }

        if (Combo == 2 && Cooldown == false && FirstAttackTimer - AttackTimer < 3f)
        {
            anim.SetTrigger("AttackEndCombo");
            Debug.Log("Ataque 3 - Fim de combo");
            StartCoroutine(ClickCooldown());
            Cooldown = true;
            FirstAttackTimer = 0;
            Combo = 0;

        }

    }

    IEnumerator ClickCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        Cooldown = false;
    }

}


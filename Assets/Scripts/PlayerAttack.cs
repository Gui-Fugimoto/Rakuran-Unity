using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region Variaveis
    [SerializeField] GameObject AttackPos1;
    [SerializeField] GameObject AttackPos2;
    [SerializeField] GameObject AttackBox;
    
    public int Combo;
    [SerializeField] bool Cooldown;
    
    [SerializeField] Animator anim;
    public int Dano; //<-- not final , for testing

    [SerializeField] PlayerController Player;
    [SerializeField] DamageParameter ArmaEquipada;
    

    #endregion

    void Start()
    {
        Player = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
            AttackBox.SetActive(true);
            Dano = ArmaEquipada.Dano;
        }

        if (Input.GetMouseButtonDown(1))
        {
            HeavyAttack();
            AttackBox.SetActive(true);
            Dano = ArmaEquipada.DanoPesado;
        }

        if (Player.flipped == true)
        {
            AttackBox.transform.position = AttackPos2.transform.position;
        }

        if (Player.flipped == false)
        {
            AttackBox.transform.position = AttackPos1.transform.position;
        }

        if(Cooldown == false)
        {
            AttackBox.SetActive(false);
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
            StartCoroutine(ComboOff());
            Cooldown = true;
        }

        if (Combo == 1 && Cooldown == false)
        {
            anim.SetTrigger("Attack");
            Debug.Log("Ataque 2");
            Combo++;
            StartCoroutine(ClickCooldown());
            Cooldown = true;
        }

        if (Combo == 2 && Cooldown == false)
        {
            anim.SetTrigger("AttackEndCombo");
            Debug.Log("Ataque 3 - Fim de combo");
            StartCoroutine(ClickCooldown());
            Cooldown = true;
            Combo = 0;
            
            StopCoroutine(ComboOff());
        }

    }
    private void HeavyAttack()
    {
        if (Combo == 0 && Cooldown == false)
        {
            anim.SetTrigger("HeavyAttack");
            Debug.Log("Atacou Pesado");
            Combo++;
            StartCoroutine(ClickCooldown());
            StartCoroutine(ComboOff());
            Cooldown = true;
        }

        if (Combo == 1 && Cooldown == false)
        {
            anim.SetTrigger("HeavyAttack");
            Debug.Log("Ataque Pesado" + Combo);
            Combo++;
            StartCoroutine(ClickCooldown());
            Cooldown = true;
        }

        if (Combo == 2 && Cooldown == false)
        {
            anim.SetTrigger("HeavyAttackEndCombo");
            Debug.Log("Ataque Pesado 3 - Fim de combo");
            StartCoroutine(ClickCooldown());
            Cooldown = true;
            Combo = 0;

            StopCoroutine(ComboOff());
        }

    }
    IEnumerator ComboOff()
    {
        yield return new WaitForSeconds(1.7f);
        Combo = 0;
    }
    
    IEnumerator ClickCooldown()
    {
        AttackBox.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        Cooldown = false;

    }

    public void PlayerTakeDamage()
    {

    }

}


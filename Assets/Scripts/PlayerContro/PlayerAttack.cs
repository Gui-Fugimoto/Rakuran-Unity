using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerAttack : MonoBehaviour
{
    #region Variaveis]
    //hitboxes provisorias
    [SerializeField] GameObject AttackPos1;
    [SerializeField] GameObject AttackPos2;
    [SerializeField] GameObject AttackBox;
    
    public int Combo;
    [SerializeField] bool Cooldown;
    
    [SerializeField] Animator anim;
    public int Dano;
    public int DanoPesado;//<-- not final , for testing
    [SerializeField] bool mainHand;

    [SerializeField] PlayerController Player;
    [SerializeField] DamageParameter ArmaEquipada1;
    [SerializeField] DamageParameter ArmaEquipada2;

    WeaponType weaponType;
    private KeyCode changeWeaponKey = KeyCode.Tab;
    //public KeyCode changeWeaponKey = KeyCode.Q;
    #endregion

    void Start()
    {
        Player = GetComponent<PlayerController>();
        weaponType = ArmaEquipada1.weaponType;
        anim.SetBool("Sword", true);
        mainHand = true;
    }

    void FixedUpdate()
    {
        StartCoroutine(QuickWeaponChange());
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
            AttackBox.SetActive(true);
            Dano = ArmaEquipada1.DanoPesado;
        }

        if (Input.GetMouseButtonDown(1))
        {
            HeavyAttack();
            AttackBox.SetActive(true);
            //Dano = ArmaEquipada.DanoPesado;
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
        switch (weaponType) 
        {
            case WeaponType.Sword:

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
                break;

            case WeaponType.Axe:

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
                    Combo = 0;

                    StopCoroutine(ComboOff());
                }

                break;
        }
        
            
        

    }
    private void HeavyAttack()
    {

        switch (weaponType) 
        {
            case WeaponType.Sword:
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
                break;


            case WeaponType.Axe:
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
                    anim.SetTrigger("HeavyAttack");
                    Debug.Log("Ataque Pesado 3 - Fim de combo");
                    StartCoroutine(ClickCooldown());
                    Cooldown = true;
                    Combo = 0;

                    StopCoroutine(ComboOff());
                }
                break;
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

    IEnumerator QuickWeaponChange()
    {
        if (Input.GetKey(changeWeaponKey) && mainHand == false)
        {
            Dano = ArmaEquipada1.Dano;
            DanoPesado = ArmaEquipada1.DanoPesado;
            weaponType = ArmaEquipada1.weaponType;
            anim.SetBool("Sword", true);
            anim.SetBool("Axe", false);
            yield return new WaitForSeconds(0.5f);
            mainHand = true;
        }
        if (Input.GetKey(changeWeaponKey) && mainHand == true)
        {
            Dano = ArmaEquipada2.Dano;
            DanoPesado = ArmaEquipada2.DanoPesado;
            weaponType = ArmaEquipada2.weaponType;
            anim.SetBool("Axe", true);
            anim.SetBool("Sword", false);
            yield return new WaitForSeconds(0.5f);
            mainHand = false;
        }
    }

    void EquipWeapon()
    {
        //for the future
    }

}


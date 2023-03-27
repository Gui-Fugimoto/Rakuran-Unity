using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region Variaveis
    [SerializeField] GameObject AttackPos1;
    [SerializeField] GameObject AttackPos2;
    [SerializeField] GameObject AttackBox;
    [SerializeField] int Combo;
    [SerializeField] bool Cooldown;
    [SerializeField] Animator anim;
    private IEnumerator ComboCourotine;
    [SerializeField] PlayerController Player;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        ComboCourotine = ComboOff();
        Player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
            AttackBox.SetActive(true);
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

}


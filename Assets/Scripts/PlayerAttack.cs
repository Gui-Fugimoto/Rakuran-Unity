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
        }
    }

    private void Attack()
    {
        if (Combo == 0 && Cooldown == false)
        {
            anim.SetTrigger("Attack");
            Debug.Log("Atacou kk");
            StartCoroutine(ComboCounter());
            Combo++;
            StartCoroutine(ClickCooldown());
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
            Combo = 0;
            StartCoroutine(ClickCooldown());
            Cooldown = true;
        }
    }

    IEnumerator ComboCounter()
    {
        yield return new WaitForSeconds(3f);
        Combo = 0;
    }

    IEnumerator ClickCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        Cooldown = false;
    }

}


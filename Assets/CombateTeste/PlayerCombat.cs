using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public bool isAttacking;
    public List<AttackSO> basicCombo;
    public List<AttackSO> upwardCombo;
    float lastClickedTime;
    float lastComboEnd;
    [SerializeField] int basicComboCounter;
    [SerializeField] int upwardComboCounter;

    bool mainHand;

    [SerializeField] WeaponItem mainWeapon;
    [SerializeField] WeaponItem offhandWeapon;

    [SerializeField] GameObject AttackPosLeft;
    [SerializeField] GameObject AttackPosRight;

    [SerializeField] Animator anim;
    [SerializeField] WeaponHitbox equippedWeapon;

    public KeyCode attackInput = KeyCode.Mouse0;
    public KeyCode changeWeaponKey = KeyCode.Tab;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FlipHitbox();
        
        if (Input.GetKey(KeyCode.W) && Input.GetKey(attackInput)) //trocar para inputmanager
        {
            UpwardAttack();
            
        }
        else if (Input.GetKey(attackInput)) //trocar para inputmanager
        {
            BasicAttack();
        }
        ExitAttack();

        StartCoroutine(QuickWeaponChange());

    }

    void BasicAttack()
    {
        isAttacking = true;
        upwardComboCounter = 0;
        if (Time.time - lastComboEnd > 0.5f && basicComboCounter <= basicCombo.Count)
        {
            CancelInvoke("EndCombo");

            if(Time.time - lastClickedTime >= 0.5f)
            {
                anim.runtimeAnimatorController = basicCombo[basicComboCounter].animatorOV;
                anim.Play("Attack", 0, 0);
                equippedWeapon.damage = equippedWeapon.baseDamage* basicCombo[basicComboCounter].damageMultiplier;
                equippedWeapon.EnableTriggerBox();
                Knockback();
                basicComboCounter++;
                lastClickedTime = Time.time;
                
                if (basicComboCounter >= basicCombo.Count)
                {
                    basicComboCounter = 0;
                }
            }
        }
        
    }
    void UpwardAttack()
    {
        isAttacking = true;
        basicComboCounter = 0;
        if (Time.time - lastComboEnd > 0.5f && upwardComboCounter <= upwardCombo.Count)
        {
            CancelInvoke("EndCombo");

            if (Time.time - lastClickedTime >= 0.5f)
            {
                anim.runtimeAnimatorController = upwardCombo[upwardComboCounter].animatorOV;
                anim.Play("Attack", 0, 0);
                equippedWeapon.damage = equippedWeapon.baseDamage * upwardCombo[upwardComboCounter].damageMultiplier;
                equippedWeapon.EnableTriggerBox();
                upwardComboCounter++;
                lastClickedTime = Time.time;

                if (upwardComboCounter >= upwardCombo.Count)
                {
                    upwardComboCounter = 0;
                }
            }
        }

    }

    void ExitAttack()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Invoke("EndCombo", 1);
            equippedWeapon.DisableTriggerBox();
            isAttacking = false;
        }
    }

    void EndCombo()
    {
        ResetComboCounters();
        lastComboEnd = Time.time;
    }

    void FlipHitbox()
    {
        if (gameObject.GetComponent<SpriteRenderer>().flipX == true)
        {
            equippedWeapon.transform.position = AttackPosLeft.transform.position;
           // basicCombo[basicComboCounter].kbDirection.x = basicCombo[basicComboCounter].kbDirection.x * (-1);
        }
        else if (gameObject.GetComponent<SpriteRenderer>().flipX == false)
        {
            equippedWeapon.transform.position = AttackPosRight.transform.position;
           // basicCombo[basicComboCounter].kbDirection.x = basicCombo[basicComboCounter].kbDirection.x * (-1);
        }
    }
    IEnumerator QuickWeaponChange()
    {
        if (Input.GetKey(changeWeaponKey) && mainHand == false)
        {
            ExitAttack();
            equippedWeapon.weaponType = mainWeapon.weaponType;
            equippedWeapon.damage = mainWeapon.damage;
            yield return new WaitForSeconds(0.5f);
            mainHand = true;
        }
        if (Input.GetKey(changeWeaponKey) && mainHand == true)
        {
            ExitAttack();
            equippedWeapon.weaponType = offhandWeapon.weaponType;
            equippedWeapon.damage = offhandWeapon.damage;
            yield return new WaitForSeconds(0.5f);
            mainHand = false;
        }
    }

    void ResetComboCounters()
    {
        basicComboCounter = 0;
        upwardComboCounter = 0;
    }


    void Knockback()
    {
        equippedWeapon.knockDirection = basicCombo[basicComboCounter].kbDirection;
        equippedWeapon.knockDuration = basicCombo[basicComboCounter].kbDuration;
        equippedWeapon.knockbackForce = basicCombo[basicComboCounter].kbForce;
    }
}

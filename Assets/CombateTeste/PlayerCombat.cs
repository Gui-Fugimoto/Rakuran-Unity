using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public bool isAttacking;
    #region Combos
    [HideInInspector] public List<AttackSO> basicLightCombo;
    [HideInInspector] public List<AttackSO> upwardLightCombo;
    [HideInInspector] public List<AttackSO> downwardLightCombo;
    [HideInInspector] public List<AttackSO> jumpingLightCombo;

    [HideInInspector] public List<AttackSO> basicHeavyCombo;
    [HideInInspector] public List<AttackSO> upwardHeavyCombo;
    [HideInInspector] public List<AttackSO> downwardHeavyCombo;
    [HideInInspector] public List<AttackSO> jumpingHeavyCombo;

    public List<AttackSO> swordLightBasicCombo;
    public List<AttackSO> swordLightUpwardCombo;
    public List<AttackSO> swordLightDownwardCombo;
    public List<AttackSO> swordLightJumpingCombo;

    public List<AttackSO> swordHeavyBasicCombo;
    public List<AttackSO> swordHeavyUpwardCombo;
    public List<AttackSO> swordHeavyDownwardCombo;
    public List<AttackSO> swordHeavyJumpingCombo;

    public List<AttackSO> hammerLightBasicCombo;
    public List<AttackSO> hammerLightUpwardCombo;
    public List<AttackSO> hammerLightDownwardCombo;
    public List<AttackSO> hammerLightJumpingCombo;

    public List<AttackSO> hammerHeavyBasicCombo;
    public List<AttackSO> hammerHeavyUpwardCombo;
    public List<AttackSO> hammerHeavyDownwardCombo;
    public List<AttackSO> hammerHeavyJumpingCombo;

    public List<AttackSO> polearmLightBasicCombo;
    public List<AttackSO> polearmLightUpwardCombo;
    public List<AttackSO> polearmLightDownwardCombo;
    public List<AttackSO> polearmLightJumpingCombo;

    public List<AttackSO> polearmHeavyBasicCombo;
    public List<AttackSO> polearmHeavyUpwardCombo;
    public List<AttackSO> polearmHeavyDownwardCombo;
    public List<AttackSO> polearmHeavyJumpingCombo;
    #endregion
    float lastClickedTime;
    float lastComboEnd;

    [SerializeField] int basicComboCounter;
    [SerializeField] int upwardComboCounter;
    [SerializeField] int downwardComboCounter;
    [SerializeField] int jumpingComboCounter;

    [SerializeField] int basicHeavyComboCounter;
    [SerializeField] int upwardHeavyComboCounter;
    [SerializeField] int downwardHeavyComboCounter;
    [SerializeField] int jumpingHeavyComboCounter;

    bool mainHand;
    

    [SerializeField] EquipWeapon mainWeapon;
    [SerializeField] EquipWeapon offhandWeapon;

    [SerializeField] GameObject AttackPosLeft;
    [SerializeField] GameObject AttackPosRight;

    [SerializeField] Animator anim;
    [SerializeField] WeaponHitbox equippedWeapon;

    [SerializeField] PlayerController playerController;

    public KeyCode lightAttackInput = KeyCode.Mouse0;
    public KeyCode heavyAttackInput = KeyCode.Mouse1;
    public KeyCode changeWeaponKey = KeyCode.Tab;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        equippedWeapon.weaponType = mainWeapon.item.weaponType;
        equippedWeapon.baseDamage = mainWeapon.item.damage;
        ChangeWeaponCombos();
        equippedWeapon.DisableTriggerBox();
    }

    // Update is called once per frame
    void Update()
    {
        FlipHitbox();
        if (mainWeapon.item == null)
        {
            isAttacking = false;
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(lightAttackInput) && !playerController.isJumping) //trocar para inputmanager
        {
            UpwardLightAttack();
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(lightAttackInput) && !playerController.isJumping) //trocar para inputmanager
        {
            DownwardLightAttack();
        }
        else if (Input.GetKey(lightAttackInput) && playerController.isJumping) //trocar para inputmanager
        {
            JumpingLightAttack();
        }
        else if (Input.GetKey(lightAttackInput) && !playerController.isJumping) //trocar para inputmanager
        {
            BasicLightAttack();
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(heavyAttackInput) && !playerController.isJumping) //trocar para inputmanager
        {
            UpwardHeavyAttack();
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(heavyAttackInput) && !playerController.isJumping) //trocar para inputmanager
        {
            DownwardHeavyAttack();
        }
        else if (Input.GetKey(heavyAttackInput) && playerController.isJumping) //trocar para inputmanager
        {
            JumpingHeavyAttack();
        }
        else if (Input.GetKey(heavyAttackInput) && !playerController.isJumping) //trocar para inputmanager
        {
            BasicHeavyAttack();
        }

        ExitAttack();

        StartCoroutine(QuickWeaponChange());

    }

    void BasicLightAttack()
    {
        
        upwardComboCounter = 0;
        downwardComboCounter = 0;
        jumpingComboCounter = 0;
        basicHeavyComboCounter = 0;
        upwardHeavyComboCounter = 0;
        downwardHeavyComboCounter = 0;
        jumpingHeavyComboCounter = 0;
        if (Time.time - lastComboEnd > basicLightCombo[basicComboCounter].endLag && basicComboCounter <= basicLightCombo.Count)
        {
            CancelInvoke("EndCombo");

            if (Time.time - lastClickedTime >= basicLightCombo[basicComboCounter].startUp)
            {
                anim.runtimeAnimatorController = basicLightCombo[basicComboCounter].animatorOV;
                //animationSpeed = basicLightCombo[basicComboCounter].animSpeed;
                anim.Play("Attack", 0, 0);
                equippedWeapon.damage = equippedWeapon.baseDamage * basicLightCombo[basicComboCounter].damageMultiplier;
                equippedWeapon.knockbackForce = basicLightCombo[basicComboCounter].kbForce;
                equippedWeapon.knockDuration = basicLightCombo[basicComboCounter].kbDuration;
                equippedWeapon.knockDirection = basicLightCombo[basicComboCounter].kbDirection;
                FlipKnockback();
                isAttacking = true;
                equippedWeapon.EnableTriggerBox();
                //Knockback();
                basicComboCounter++;
                lastClickedTime = Time.time;

                if (basicComboCounter >= basicLightCombo.Count)
                {
                    basicComboCounter = 0;
                }
            }
        }

    }
    void UpwardLightAttack()
    {
        
        basicComboCounter = 0;
        downwardComboCounter = 0;
        jumpingComboCounter = 0;
        basicHeavyComboCounter = 0;
        upwardHeavyComboCounter = 0;
        downwardHeavyComboCounter = 0;
        jumpingHeavyComboCounter = 0;
        if (Time.time - lastComboEnd > 0.5f && upwardComboCounter <= upwardLightCombo.Count)
        {
            CancelInvoke("EndCombo");

            if (Time.time - lastClickedTime >= 0.5f)
            {
                anim.runtimeAnimatorController = upwardLightCombo[upwardComboCounter].animatorOV;
                anim.Play("Attack", 0, 0);
                equippedWeapon.damage = equippedWeapon.baseDamage * upwardLightCombo[upwardComboCounter].damageMultiplier;
                equippedWeapon.knockbackForce = upwardLightCombo[upwardComboCounter].kbForce;
                equippedWeapon.knockDuration = upwardLightCombo[upwardComboCounter].kbDuration;
                equippedWeapon.knockDirection = upwardLightCombo[upwardComboCounter].kbDirection;
                isAttacking = true;
                equippedWeapon.EnableTriggerBox();
                upwardComboCounter++;
                lastClickedTime = Time.time;

                if (upwardComboCounter >= upwardLightCombo.Count)
                {
                    upwardComboCounter = 0;
                }
            }
        }

    }
    void DownwardLightAttack()
    {
        
        basicComboCounter = 0;
        upwardComboCounter = 0;
        jumpingComboCounter = 0;
        basicHeavyComboCounter = 0;
        upwardHeavyComboCounter = 0;
        downwardHeavyComboCounter = 0;
        jumpingHeavyComboCounter = 0;
        if (Time.time - lastComboEnd > 0.5f && downwardComboCounter <= downwardLightCombo.Count)
        {
            CancelInvoke("EndCombo");

            if (Time.time - lastClickedTime >= 0.5f)
            {
                anim.runtimeAnimatorController = downwardLightCombo[downwardComboCounter].animatorOV;
                anim.Play("Attack", 0, 0);
                equippedWeapon.damage = equippedWeapon.baseDamage * downwardLightCombo[downwardComboCounter].damageMultiplier;
                equippedWeapon.knockbackForce = downwardLightCombo[downwardComboCounter].kbForce;
                equippedWeapon.knockDuration = downwardLightCombo[downwardComboCounter].kbDuration;
                equippedWeapon.knockDirection = downwardLightCombo[downwardComboCounter].kbDirection;
                isAttacking = true;
                equippedWeapon.EnableTriggerBox();
                downwardComboCounter++;
                lastClickedTime = Time.time;

                if (downwardComboCounter >= downwardLightCombo.Count)
                {
                    downwardComboCounter = 0;
                }
            }
        }
    }
    void JumpingLightAttack()
    {
        
        basicComboCounter = 0;
        upwardComboCounter = 0;
        downwardComboCounter = 0;
        basicHeavyComboCounter = 0;
        upwardHeavyComboCounter = 0;
        downwardHeavyComboCounter = 0;
        jumpingHeavyComboCounter = 0;
        if (Time.time - lastComboEnd > 0.5f && jumpingComboCounter <= jumpingLightCombo.Count)
        {
            CancelInvoke("EndCombo");

            if (Time.time - lastClickedTime >= 0.5f)
            {
                anim.runtimeAnimatorController = jumpingLightCombo[jumpingComboCounter].animatorOV;
                anim.Play("Attack", 0, 0);
                equippedWeapon.damage = equippedWeapon.baseDamage * jumpingLightCombo[jumpingComboCounter].damageMultiplier;
                equippedWeapon.knockbackForce = jumpingLightCombo[jumpingComboCounter].kbForce;
                equippedWeapon.knockDuration = jumpingLightCombo[jumpingComboCounter].kbDuration;
                equippedWeapon.knockDirection = jumpingLightCombo[jumpingComboCounter].kbDirection;
                isAttacking = true;
                equippedWeapon.EnableTriggerBox();
                jumpingComboCounter++;
                lastClickedTime = Time.time;

                if (jumpingComboCounter >= jumpingLightCombo.Count)
                {
                    jumpingComboCounter = 0;
                }
            }
        }

    }
    void BasicHeavyAttack()
    {
        
        upwardComboCounter = 0;
        downwardComboCounter = 0;
        jumpingComboCounter = 0;
        basicComboCounter = 0;
        upwardHeavyComboCounter = 0;
        downwardHeavyComboCounter = 0;
        jumpingHeavyComboCounter = 0;
        if (Time.time - lastComboEnd > 0.5f && basicHeavyComboCounter <= basicHeavyCombo.Count)
        {
            CancelInvoke("EndCombo");

            if (Time.time - lastClickedTime >= 0.5f)
            {
                anim.runtimeAnimatorController = basicHeavyCombo[basicHeavyComboCounter].animatorOV;
                anim.Play("Attack", 0, 0);
                equippedWeapon.damage = equippedWeapon.baseDamage * basicHeavyCombo[basicHeavyComboCounter].damageMultiplier;
                equippedWeapon.knockbackForce = basicHeavyCombo[basicHeavyComboCounter].kbForce;
                equippedWeapon.knockDuration = basicHeavyCombo[basicHeavyComboCounter].kbDuration;
                equippedWeapon.knockDirection = basicHeavyCombo[basicHeavyComboCounter].kbDirection;
                isAttacking = true;
                equippedWeapon.EnableTriggerBox();
                //Knockback();
                basicHeavyComboCounter++;
                lastClickedTime = Time.time;

                if (basicHeavyComboCounter >= basicHeavyCombo.Count)
                {
                    basicHeavyComboCounter = 0;
                }
            }
        }

    }
    void UpwardHeavyAttack()
    {
        
        upwardComboCounter = 0;
        downwardComboCounter = 0;
        jumpingComboCounter = 0;
        basicComboCounter = 0;
        basicHeavyComboCounter = 0;
        downwardHeavyComboCounter = 0;
        jumpingHeavyComboCounter = 0;
        if (Time.time - lastComboEnd > 0.5f && upwardHeavyComboCounter <= upwardHeavyCombo.Count)
        {
            CancelInvoke("EndCombo");

            if (Time.time - lastClickedTime >= 0.5f)
            {
                anim.runtimeAnimatorController = upwardHeavyCombo[upwardHeavyComboCounter].animatorOV;
                anim.Play("Attack", 0, 0);
                equippedWeapon.damage = equippedWeapon.baseDamage * upwardHeavyCombo[upwardHeavyComboCounter].damageMultiplier;
                equippedWeapon.knockbackForce = upwardHeavyCombo[upwardHeavyComboCounter].kbForce;
                equippedWeapon.knockDuration = upwardHeavyCombo[upwardHeavyComboCounter].kbDuration;
                equippedWeapon.knockDirection = upwardHeavyCombo[upwardHeavyComboCounter].kbDirection;
                isAttacking = true;
                equippedWeapon.EnableTriggerBox();
                //Knockback();
                upwardHeavyComboCounter++;
                lastClickedTime = Time.time;

                if (upwardHeavyComboCounter >= upwardHeavyCombo.Count)
                {
                    upwardHeavyComboCounter = 0;
                }
            }
        }

    }
    void DownwardHeavyAttack()
    {
        
        upwardComboCounter = 0;
        downwardComboCounter = 0;
        jumpingComboCounter = 0;
        basicComboCounter = 0;
        basicHeavyComboCounter = 0;
        upwardHeavyComboCounter = 0;
        jumpingHeavyComboCounter = 0;
        if (Time.time - lastComboEnd > 0.5f && downwardHeavyComboCounter <= downwardHeavyCombo.Count)
        {
            CancelInvoke("EndCombo");

            if (Time.time - lastClickedTime >= 0.5f)
            {
                anim.runtimeAnimatorController = downwardHeavyCombo[downwardHeavyComboCounter].animatorOV;
                anim.Play("Attack", 0, 0);
                equippedWeapon.damage = equippedWeapon.baseDamage * downwardHeavyCombo[downwardHeavyComboCounter].damageMultiplier;
                equippedWeapon.knockbackForce = downwardHeavyCombo[downwardHeavyComboCounter].kbForce;
                equippedWeapon.knockDuration = downwardHeavyCombo[downwardHeavyComboCounter].kbDuration;
                equippedWeapon.knockDirection = downwardHeavyCombo[downwardHeavyComboCounter].kbDirection;
                isAttacking = true;
                equippedWeapon.EnableTriggerBox();
                //Knockback();
                downwardHeavyComboCounter++;
                lastClickedTime = Time.time;

                if (downwardHeavyComboCounter >= downwardHeavyCombo.Count)
                {
                    downwardHeavyComboCounter = 0;
                }
            }
        }

    }
    void JumpingHeavyAttack()
    {
        
        upwardComboCounter = 0;
        downwardComboCounter = 0;
        jumpingComboCounter = 0;
        basicComboCounter = 0;
        basicHeavyComboCounter = 0;
        upwardHeavyComboCounter = 0;
        downwardHeavyComboCounter = 0;
        if (Time.time - lastComboEnd > 0.5f && jumpingHeavyComboCounter <= jumpingHeavyCombo.Count)
        {
            CancelInvoke("EndCombo");

            if (Time.time - lastClickedTime >= 0.5f)
            {
                anim.runtimeAnimatorController = jumpingHeavyCombo[jumpingHeavyComboCounter].animatorOV;
                anim.Play("Attack", 0, 0);
                equippedWeapon.damage = equippedWeapon.baseDamage * jumpingHeavyCombo[jumpingHeavyComboCounter].damageMultiplier;
                equippedWeapon.knockbackForce = jumpingHeavyCombo[jumpingHeavyComboCounter].kbForce;
                equippedWeapon.knockDuration = jumpingHeavyCombo[jumpingHeavyComboCounter].kbDuration;
                equippedWeapon.knockDirection = jumpingHeavyCombo[jumpingHeavyComboCounter].kbDirection;
                isAttacking = true;
                equippedWeapon.EnableTriggerBox();
                //Knockback();
                jumpingHeavyComboCounter++;
                lastClickedTime = Time.time;

                if (jumpingHeavyComboCounter >= jumpingHeavyCombo.Count)
                {
                    jumpingHeavyComboCounter = 0;
                }
            }
        }

    }
    void ExitAttack()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
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

    void FlipKnockback()
    {
        if (gameObject.GetComponent<SpriteRenderer>().flipX)
        {
            equippedWeapon.knockDirection.x = equippedWeapon.knockDirection.x * (-1);
        }
        
    }
    IEnumerator QuickWeaponChange()
    {
        if (Input.GetKey(changeWeaponKey) && mainHand == false && mainWeapon != null)
        {
            ExitAttack();
            equippedWeapon.weaponType = mainWeapon.item.weaponType;
            equippedWeapon.baseDamage = mainWeapon.item.damage;
            ChangeWeaponCombos();
            ResetComboCounters();
            yield return new WaitForSeconds(0.5f);
            mainHand = true;

        }
        if (Input.GetKey(changeWeaponKey) && mainHand == true && offhandWeapon != null)
        {
            ExitAttack();
            equippedWeapon.weaponType = offhandWeapon.item.weaponType;
            equippedWeapon.baseDamage = offhandWeapon.item.damage;
            ChangeWeaponCombos();
            ResetComboCounters();
            yield return new WaitForSeconds(0.5f);
            mainHand = false;

        }
    }

    void ResetComboCounters()
    {
        basicComboCounter = 0;
        upwardComboCounter = 0;
        downwardComboCounter = 0;
        jumpingComboCounter = 0;
        basicHeavyComboCounter = 0;
        downwardHeavyComboCounter = 0;
        upwardHeavyComboCounter = 0;
        jumpingHeavyComboCounter = 0;
    }




    void ChangeWeaponCombos()
    {
        switch (equippedWeapon.weaponType)
        {
            case WeaponType.Sword:
                basicLightCombo = swordLightBasicCombo;
                upwardLightCombo = swordLightUpwardCombo;
                downwardLightCombo = swordLightDownwardCombo;
                jumpingLightCombo = swordLightJumpingCombo;
                basicHeavyCombo = swordHeavyBasicCombo;
                upwardHeavyCombo = swordHeavyUpwardCombo;
                downwardHeavyCombo = swordHeavyDownwardCombo;
                jumpingHeavyCombo = swordHeavyJumpingCombo;
                break;

            case WeaponType.Hammer:
                basicLightCombo = hammerLightBasicCombo;
                upwardLightCombo = hammerLightUpwardCombo;
                downwardLightCombo = hammerLightDownwardCombo;
                jumpingLightCombo = hammerLightJumpingCombo;
                basicHeavyCombo = hammerHeavyBasicCombo;
                upwardHeavyCombo = hammerHeavyUpwardCombo;
                downwardHeavyCombo = hammerHeavyDownwardCombo;
                jumpingHeavyCombo = hammerHeavyJumpingCombo;
                break;

            case WeaponType.Polearm:
                basicLightCombo = polearmLightBasicCombo;
                upwardLightCombo = polearmLightUpwardCombo;
                downwardLightCombo = polearmLightDownwardCombo;
                jumpingLightCombo = polearmLightJumpingCombo;
                basicHeavyCombo = polearmHeavyBasicCombo;
                upwardHeavyCombo = polearmHeavyUpwardCombo;
                downwardHeavyCombo = polearmHeavyDownwardCombo;
                jumpingHeavyCombo = polearmHeavyJumpingCombo;
                break;
        }




    }

    

}

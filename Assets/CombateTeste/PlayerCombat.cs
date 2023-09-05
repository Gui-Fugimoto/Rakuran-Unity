using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public bool isAttacking;
    #region Combos
    public List<AttackSO> basicLightCombo;
    public List<AttackSO> upwardLightCombo;
    public List<AttackSO> downwardLightCombo;
    public List<AttackSO> jumpingLightCombo;

    private List<AttackSO> basicHeavyCombo;
    private List<AttackSO> upwardHeavyCombo;
    private List<AttackSO> downwardHeavyCombo;
    private List<AttackSO> jumpingHeavyCombo;
    
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

    [SerializeField] WeaponItem mainWeapon;
    [SerializeField] WeaponItem offhandWeapon;

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
        equippedWeapon.weaponType = mainWeapon.weaponType;
        equippedWeapon.baseDamage = mainWeapon.damage;
        ChangeWeaponCombos();
    }

    // Update is called once per frame
    void Update()
    {
        FlipHitbox();
        
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
            

        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(heavyAttackInput) && !playerController.isJumping) //trocar para inputmanager
        {


        }
        else if (Input.GetKey(heavyAttackInput) && playerController.isJumping) //trocar para inputmanager
        {
            
        }
        else if (Input.GetKey(heavyAttackInput) && !playerController.isJumping) //trocar para inputmanager
        {
            
        }

        ExitAttack();

        StartCoroutine(QuickWeaponChange());

    }

    void BasicLightAttack()
    {
        isAttacking = true;
        upwardComboCounter = 0;
        downwardComboCounter = 0;
        jumpingComboCounter = 0;
        basicHeavyComboCounter = 0;
        upwardHeavyComboCounter = 0;
        downwardHeavyComboCounter = 0;
        jumpingHeavyComboCounter = 0;
        if (Time.time - lastComboEnd > 0.5f && basicComboCounter <= basicLightCombo.Count)
        {
            CancelInvoke("EndCombo");

            if(Time.time - lastClickedTime >= 0.5f)
            {
                anim.runtimeAnimatorController = basicLightCombo[basicComboCounter].animatorOV;
                anim.Play("Attack", 0, 0);
                equippedWeapon.damage = equippedWeapon.baseDamage* basicLightCombo[basicComboCounter].damageMultiplier;
                equippedWeapon.knockbackForce = basicLightCombo[basicComboCounter].kbForce;
                equippedWeapon.knockDuration = basicLightCombo[basicComboCounter].kbDuration;
                equippedWeapon.knockDirection = basicLightCombo[basicComboCounter].kbDirection;
                equippedWeapon.EnableTriggerBox();
                Knockback();
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
        isAttacking = true;
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
        isAttacking = true;
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
        isAttacking = true;
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
                equippedWeapon.EnableTriggerBox();
                downwardComboCounter++;
                lastClickedTime = Time.time;

                if (jumpingComboCounter >= jumpingLightCombo.Count)
                {
                    jumpingComboCounter = 0;
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
            equippedWeapon.baseDamage = mainWeapon.damage;
            ChangeWeaponCombos();
            ResetComboCounters();
            yield return new WaitForSeconds(0.5f);
            mainHand = true;
            
        }
        if (Input.GetKey(changeWeaponKey) && mainHand == true)
        {
            ExitAttack();
            equippedWeapon.weaponType = offhandWeapon.weaponType;
            equippedWeapon.baseDamage = offhandWeapon.damage;
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
    }


    void Knockback()
    {
        equippedWeapon.knockDirection = basicLightCombo[basicComboCounter].kbDirection;
        equippedWeapon.knockDuration = basicLightCombo[basicComboCounter].kbDuration;
        equippedWeapon.knockbackForce = basicLightCombo[basicComboCounter].kbForce;
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
                break;

            case WeaponType.Hammer:
                basicLightCombo = hammerLightBasicCombo;
                upwardLightCombo = hammerLightUpwardCombo;
                downwardLightCombo = hammerLightDownwardCombo;
                jumpingLightCombo = hammerLightJumpingCombo;
                break;

            case WeaponType.Polearm:
                basicLightCombo = polearmLightBasicCombo;
                upwardLightCombo = polearmLightUpwardCombo;
                downwardLightCombo = polearmLightDownwardCombo;
                jumpingLightCombo = polearmLightJumpingCombo;
                break;
        }




    }
}

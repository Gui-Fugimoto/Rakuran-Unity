using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public bool isAttacking;
    public bool fabianoOnce;
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

    public bool mainHand;
    

    public EquipWeapon mainWeapon;
    public EquipWeapon offhandWeapon;

    [SerializeField] GameObject AttackPosLeft;
    [SerializeField] GameObject AttackPosRight;

    [SerializeField] Animator anim;
    private WeaponHitbox equippedWeapon;

    public GameObject hitboxGameObj;
    private Vector3 hbDefaultScale = new Vector3(1, 1, 0.56f);
    private float hbZDefault = 0.56f;
    

    [SerializeField] PlayerController playerController;

    public KeyCode lightAttackInput = KeyCode.Mouse0;
    public KeyCode heavyAttackInput = KeyCode.Mouse1;
    public KeyCode changeWeaponKey = KeyCode.Q;

    public AudioSource audioSource;
    

    // Start is called before the first frame update
    void Start()
    {
        fabianoOnce = false;
        equippedWeapon = hitboxGameObj.GetComponent<WeaponHitbox>();
        anim = GetComponent<Animator>();
        equippedWeapon.weaponType = mainWeapon.item.weaponType;
        equippedWeapon.baseDamage = mainWeapon.item.damage;
       // ChangeWeaponCombos();
        StartCoroutine(QuickWeaponChange());
        hitboxGameObj.transform.localScale = hbDefaultScale;
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
        if (!playerController.isSprinting)
        {
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
        }
        

        

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

            if (Time.time - lastClickedTime >= basicLightCombo[basicComboCounter].startUp && isAttacking == false)
            {
                anim.runtimeAnimatorController = basicLightCombo[basicComboCounter].animatorOV;
                //animationSpeed = basicLightCombo[basicComboCounter].animSpeed;
                anim.Play("Attack", 0, 0);
                equippedWeapon.damage = equippedWeapon.baseDamage * basicLightCombo[basicComboCounter].damageMultiplier;
                equippedWeapon.knockbackForce = basicLightCombo[basicComboCounter].kbForce;
                equippedWeapon.knockDuration = basicLightCombo[basicComboCounter].kbDuration;
                equippedWeapon.knockDirection = basicLightCombo[basicComboCounter].kbDirection;
                equippedWeapon.audioClip = basicLightCombo[basicComboCounter].audioClip;
                hitboxGameObj.transform.localScale = new Vector3 (basicLightCombo[basicComboCounter].hboxXScale, basicLightCombo[basicComboCounter].hboxYScale, hbZDefault);
                hitboxGameObj.transform.localPosition = new Vector3(hitboxGameObj.transform.localPosition.x, basicLightCombo[basicComboCounter].hboxYPos, 0);                
                //isAttacking = true;
                //equippedWeapon.EnableTriggerBox();
                audioSource.clip = equippedWeapon.audioClip;
                audioSource.Play();
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
        if (Time.time - lastComboEnd > upwardLightCombo[upwardComboCounter].endLag && upwardComboCounter <= upwardLightCombo.Count)
        {
            CancelInvoke("EndCombo");

            if (Time.time - lastClickedTime >= 0.5f && isAttacking == false)
            {
                anim.runtimeAnimatorController = upwardLightCombo[upwardComboCounter].animatorOV;
                anim.Play("Attack", 0, 0);
                equippedWeapon.damage = equippedWeapon.baseDamage * upwardLightCombo[upwardComboCounter].damageMultiplier;
                equippedWeapon.knockbackForce = upwardLightCombo[upwardComboCounter].kbForce;
                equippedWeapon.knockDuration = upwardLightCombo[upwardComboCounter].kbDuration;
                equippedWeapon.knockDirection = upwardLightCombo[upwardComboCounter].kbDirection;
                equippedWeapon.audioClip = upwardLightCombo[upwardComboCounter].audioClip;
                hitboxGameObj.transform.localScale = new Vector3(upwardLightCombo[upwardComboCounter].hboxXScale, upwardLightCombo[upwardComboCounter].hboxYScale, hbZDefault);
                hitboxGameObj.transform.localPosition = new Vector3(hitboxGameObj.transform.localPosition.x, upwardLightCombo[upwardComboCounter].hboxYPos, 0);
                //equippedWeapon.EnableTriggerBox();
                upwardComboCounter++;
                lastClickedTime = Time.time;
                audioSource.clip = equippedWeapon.audioClip;
                audioSource.Play();
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
        if (Time.time - lastComboEnd > downwardLightCombo[downwardComboCounter].endLag && downwardComboCounter <= downwardLightCombo.Count)
        {
            CancelInvoke("EndCombo");

            if (Time.time - lastClickedTime >= 0.5f && isAttacking == false)
            {
                anim.runtimeAnimatorController = downwardLightCombo[downwardComboCounter].animatorOV;
                anim.Play("Attack", 0, 0);
                equippedWeapon.damage = equippedWeapon.baseDamage * downwardLightCombo[downwardComboCounter].damageMultiplier;
                equippedWeapon.knockbackForce = downwardLightCombo[downwardComboCounter].kbForce;
                equippedWeapon.knockDuration = downwardLightCombo[downwardComboCounter].kbDuration;
                equippedWeapon.knockDirection = downwardLightCombo[downwardComboCounter].kbDirection;
                equippedWeapon.audioClip = downwardLightCombo[downwardComboCounter].audioClip;
                hitboxGameObj.transform.localScale = new Vector3(downwardLightCombo[downwardComboCounter].hboxXScale, downwardLightCombo[downwardComboCounter].hboxYScale, hbZDefault);
                hitboxGameObj.transform.localPosition = new Vector3(hitboxGameObj.transform.localPosition.x, downwardLightCombo[downwardComboCounter].hboxYPos, 0);
                //equippedWeapon.EnableTriggerBox();
                downwardComboCounter++;
                lastClickedTime = Time.time;
                audioSource.clip = equippedWeapon.audioClip;
                audioSource.Play();
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
        if (Time.time - lastComboEnd > jumpingLightCombo[jumpingComboCounter].endLag && jumpingComboCounter <= jumpingLightCombo.Count)
        {
            CancelInvoke("EndCombo");

            if (Time.time - lastClickedTime >= 0.5f && isAttacking == false)
            {
                anim.runtimeAnimatorController = jumpingLightCombo[jumpingComboCounter].animatorOV;
                anim.Play("Attack", 0, 0);
                equippedWeapon.damage = equippedWeapon.baseDamage * jumpingLightCombo[jumpingComboCounter].damageMultiplier;
                equippedWeapon.knockbackForce = jumpingLightCombo[jumpingComboCounter].kbForce;
                equippedWeapon.knockDuration = jumpingLightCombo[jumpingComboCounter].kbDuration;
                equippedWeapon.knockDirection = jumpingLightCombo[jumpingComboCounter].kbDirection;
                equippedWeapon.audioClip = jumpingLightCombo[jumpingComboCounter].audioClip;
                hitboxGameObj.transform.localScale = new Vector3(jumpingLightCombo[jumpingComboCounter].hboxXScale, jumpingLightCombo[jumpingComboCounter].hboxYScale, hbZDefault);
                hitboxGameObj.transform.localPosition = new Vector3(hitboxGameObj.transform.localPosition.x, jumpingLightCombo[jumpingComboCounter].hboxYPos, 0);
                //equippedWeapon.EnableTriggerBox();
                jumpingComboCounter++;
                lastClickedTime = Time.time;
                audioSource.clip = equippedWeapon.audioClip;
                audioSource.Play();
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
        if (Time.time - lastComboEnd > basicHeavyCombo[basicHeavyComboCounter].endLag && basicHeavyComboCounter <= basicHeavyCombo.Count)
        {
            CancelInvoke("EndCombo");

            if (Time.time - lastClickedTime >= 0.5f && isAttacking == false)
            {
                anim.runtimeAnimatorController = basicHeavyCombo[basicHeavyComboCounter].animatorOV;
                anim.Play("Attack", 0, 0);
                equippedWeapon.damage = equippedWeapon.baseDamage * basicHeavyCombo[basicHeavyComboCounter].damageMultiplier;
                equippedWeapon.knockbackForce = basicHeavyCombo[basicHeavyComboCounter].kbForce;
                equippedWeapon.knockDuration = basicHeavyCombo[basicHeavyComboCounter].kbDuration;
                equippedWeapon.knockDirection = basicHeavyCombo[basicHeavyComboCounter].kbDirection;
                equippedWeapon.audioClip = basicHeavyCombo[basicHeavyComboCounter].audioClip;
                hitboxGameObj.transform.localScale = new Vector3(basicHeavyCombo[basicHeavyComboCounter].hboxXScale, basicHeavyCombo[basicHeavyComboCounter].hboxYScale, hbZDefault);
                hitboxGameObj.transform.localPosition = new Vector3(hitboxGameObj.transform.localPosition.x, basicHeavyCombo[basicHeavyComboCounter].hboxYPos, 0);
                //equippedWeapon.EnableTriggerBox();
                //Knockback();
                basicHeavyComboCounter++;
                lastClickedTime = Time.time;
                audioSource.clip = equippedWeapon.audioClip;
                audioSource.Play();
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
        if (Time.time - lastComboEnd > upwardHeavyCombo[upwardHeavyComboCounter].endLag && upwardHeavyComboCounter <= upwardHeavyCombo.Count)
        {
            CancelInvoke("EndCombo");

            if (Time.time - lastClickedTime >= 0.5f && isAttacking == false)
            {
                anim.runtimeAnimatorController = upwardHeavyCombo[upwardHeavyComboCounter].animatorOV;
                anim.Play("Attack", 0, 0);
                equippedWeapon.damage = equippedWeapon.baseDamage * upwardHeavyCombo[upwardHeavyComboCounter].damageMultiplier;
                equippedWeapon.knockbackForce = upwardHeavyCombo[upwardHeavyComboCounter].kbForce;
                equippedWeapon.knockDuration = upwardHeavyCombo[upwardHeavyComboCounter].kbDuration;
                equippedWeapon.knockDirection = upwardHeavyCombo[upwardHeavyComboCounter].kbDirection;
                equippedWeapon.audioClip = upwardHeavyCombo[upwardHeavyComboCounter].audioClip;
                hitboxGameObj.transform.localScale = new Vector3(upwardHeavyCombo[upwardHeavyComboCounter].hboxXScale, upwardHeavyCombo[upwardHeavyComboCounter].hboxYScale, hbZDefault);
                hitboxGameObj.transform.localPosition = new Vector3(hitboxGameObj.transform.localPosition.x, upwardHeavyCombo[upwardHeavyComboCounter].hboxYPos, 0);
                //equippedWeapon.EnableTriggerBox();
                //Knockback();
                upwardHeavyComboCounter++;
                lastClickedTime = Time.time;
                audioSource.clip = equippedWeapon.audioClip;
                audioSource.Play();
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
        if (Time.time - lastComboEnd > downwardHeavyCombo[downwardHeavyComboCounter].endLag && downwardHeavyComboCounter <= downwardHeavyCombo.Count)
        {
            CancelInvoke("EndCombo");

            if (Time.time - lastClickedTime >= 0.5f && isAttacking == false)
            {
                anim.runtimeAnimatorController = downwardHeavyCombo[downwardHeavyComboCounter].animatorOV;
                anim.Play("Attack", 0, 0);
                equippedWeapon.damage = equippedWeapon.baseDamage * downwardHeavyCombo[downwardHeavyComboCounter].damageMultiplier;
                equippedWeapon.knockbackForce = downwardHeavyCombo[downwardHeavyComboCounter].kbForce;
                equippedWeapon.knockDuration = downwardHeavyCombo[downwardHeavyComboCounter].kbDuration;
                equippedWeapon.knockDirection = downwardHeavyCombo[downwardHeavyComboCounter].kbDirection;
                equippedWeapon.audioClip = downwardHeavyCombo[downwardHeavyComboCounter].audioClip;
                hitboxGameObj.transform.localScale = new Vector3(downwardHeavyCombo[downwardHeavyComboCounter].hboxXScale, downwardHeavyCombo[downwardHeavyComboCounter].hboxYScale, hbZDefault);
                hitboxGameObj.transform.localPosition = new Vector3(hitboxGameObj.transform.localPosition.x, downwardHeavyCombo[downwardHeavyComboCounter].hboxYPos, 0);
                //equippedWeapon.EnableTriggerBox();
                //Knockback();
                downwardHeavyComboCounter++;
                lastClickedTime = Time.time;
                audioSource.clip = equippedWeapon.audioClip;
                audioSource.Play();
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
        if (Time.time - lastComboEnd > jumpingHeavyCombo[jumpingHeavyComboCounter].endLag && jumpingHeavyComboCounter <= jumpingHeavyCombo.Count)
        {
            CancelInvoke("EndCombo");

            if (Time.time - lastClickedTime >= 0.5f && isAttacking == false)
            {
                anim.runtimeAnimatorController = jumpingHeavyCombo[jumpingHeavyComboCounter].animatorOV;
                anim.Play("Attack", 0, 0);
                equippedWeapon.damage = equippedWeapon.baseDamage * jumpingHeavyCombo[jumpingHeavyComboCounter].damageMultiplier;
                equippedWeapon.knockbackForce = jumpingHeavyCombo[jumpingHeavyComboCounter].kbForce;
                equippedWeapon.knockDuration = jumpingHeavyCombo[jumpingHeavyComboCounter].kbDuration;
                equippedWeapon.knockDirection = jumpingHeavyCombo[jumpingHeavyComboCounter].kbDirection;
                equippedWeapon.audioClip = jumpingHeavyCombo[jumpingHeavyComboCounter].audioClip;
                hitboxGameObj.transform.localScale = new Vector3(jumpingHeavyCombo[jumpingHeavyComboCounter].hboxXScale, jumpingHeavyCombo[jumpingHeavyComboCounter].hboxYScale, hbZDefault);
                hitboxGameObj.transform.localPosition = new Vector3(hitboxGameObj.transform.localPosition.x, jumpingHeavyCombo[jumpingHeavyComboCounter].hboxYPos, 0);
                //equippedWeapon.EnableTriggerBox();
                //Knockback();
                jumpingHeavyComboCounter++;
                lastClickedTime = Time.time;
                audioSource.clip = equippedWeapon.audioClip;
                audioSource.Play();
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
            DisableTriggerBoxEQPW();
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
            hitboxGameObj.transform.position = new Vector3(AttackPosLeft.transform.position.x, hitboxGameObj.transform.position.y, hitboxGameObj.transform.position.z);
            equippedWeapon.flipLeft = true;
        }
        else if (gameObject.GetComponent<SpriteRenderer>().flipX == false)
        {
            hitboxGameObj.transform.position = new Vector3 (AttackPosRight.transform.position.x, hitboxGameObj.transform.position.y, hitboxGameObj.transform.position.z);
            equippedWeapon.flipLeft = false;
        }
    }

    
    IEnumerator QuickWeaponChange()
    {

        if (mainHand == false && mainWeapon.item != null && fabianoOnce == false )
        {
            ExitAttack();
            equippedWeapon.weaponType = mainWeapon.item.weaponType;
            equippedWeapon.baseDamage = mainWeapon.item.damage;
            ChangeWeaponCombos();
            ResetComboCounters();
            yield return new WaitForSeconds(0.5f);
            mainHand = true;
            fabianoOnce = true;
        }

        if (Input.GetKey(changeWeaponKey) && mainHand == false && mainWeapon.item != null)
        {
            ExitAttack();
            equippedWeapon.weaponType = mainWeapon.item.weaponType;
            equippedWeapon.baseDamage = mainWeapon.item.damage;
            ChangeWeaponCombos();
            ResetComboCounters();
            yield return new WaitForSeconds(0.5f);
            mainHand = true;

        }
        if (Input.GetKey(changeWeaponKey) && mainHand == true && offhandWeapon.item != null)
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


    public void EnableTriggerBoxEQPW()
    {
        equippedWeapon.EnableTriggerBox();
    }
    public void DisableTriggerBoxEQPW()
    {
        equippedWeapon.DisableTriggerBox();
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

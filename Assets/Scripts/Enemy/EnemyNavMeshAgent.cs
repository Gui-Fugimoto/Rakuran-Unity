using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyNavMeshAgent : MonoBehaviour
{
    
    //public Transform player;
    public GameObject player;
    public PlayerController playerCont;
    public PlayerHealthController Phc;
    public float attackRange = 2f;
    public float pursuitRange = 8f;
    public float wanderRange = 10f;
    public float fleeRange = 0f;
    public float attackCooldown = 2f;
    public float atkEndDelay = 1f;

    private Vector3 wanderTarget;
    private Vector3 initialPosition;
    public float timeSinceLastAttack = 0f;
    public int currentState = 0; // 0 = wander, 1 = pursuit, 2 = attack
    public bool isAttacking = false;

    public Animator anim;
    string attackAnim;
    [HideInInspector]public SpriteRenderer spriteRend;

    public NavMeshAgent navMeshAgent;
    private Rigidbody rb;
    [SerializeField] private bool grounded;
    private LayerMask Ground;
    [SerializeField] private float groundCheckDistance = 0.2f;

    [SerializeField] GameObject hitBox;
    [SerializeField] GameObject hitBoxPosLeft;
    [SerializeField] GameObject hitBoxPosRight;

    private Transform spriteTransform;
    public float hitstun;
    float pauseDuration = 2f;
    float pauseTimer = 0f;
    bool isPaused = false;
    public float pursuitTimer;
    [SerializeField] float specialCooldown = 100f;
    public float CDcontrol = 0f;
    public GameObject specialHitBox;

    public bool bossBawn;
    private CapsuleCollider capCollidier;
    void Start()
    {
        initialPosition = transform.position;
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        spriteRend = GetComponentInChildren<SpriteRenderer>();
        spriteTransform = this.gameObject.transform.GetChild(0);
        rb = GetComponent<Rigidbody>();
        hitBox.SetActive(false);
        specialHitBox.SetActive(false);
        wanderTarget = initialPosition + Random.insideUnitSphere * wanderRange;
        wanderTarget.y = transform.position.y;
        Ground = LayerMask.GetMask("Ground");
        playerCont = FindObjectOfType<PlayerController>();
        player = playerCont.gameObject;
        Phc = player.GetComponent<PlayerHealthController>();
        attackAnim = "Attack";
        capCollidier = GetComponent<CapsuleCollider>();
        if (bossBawn == true)
        {
            anim.SetTrigger("Spawn");
            Debug.Log("bossbawn");
            currentState = 7;
        }
    }

    void FixedUpdate()
    {
        CDcontrol = Mathf.Clamp(CDcontrol + 4f * Time.deltaTime, 0f, specialCooldown);
        SendInCombatMessage();
        CheckGrounded();
        timeSinceLastAttack += Time.deltaTime;
        spriteTransform.rotation = Quaternion.Euler(0, 0, 0);
        switch (currentState)
        {
            case 0:
                WanderState();
                break;
            case 1:
                PursuitState();
                break;
            case 2:
                AttackState();
                break;
            case 3:
                StartCoroutine(HitstunState());
                break;
            case 4:
                StartCoroutine(CooldownState(Random.Range(3f, 5f)));
                break;
            case 5:
                SpecialAttack();
                break;
            case 6:
                FleeState();
                break;
            case 7:
                SpawningState();
                break;
        }

        hitBoxPosLeft.transform.position = new Vector3(transform.position.x + 0.95f, transform.position.y, transform.position.z);
        hitBoxPosRight.transform.position = new Vector3(transform.position.x - 0.828f, transform.position.y, transform.position.z);

        if (spriteRend.flipX == false)
        {
            hitBox.transform.position = hitBoxPosLeft.transform.position;
        }
        if (spriteRend.flipX == true)
        {
            hitBox.transform.position = hitBoxPosRight.transform.position;
        }

    }

    void WanderState()
    {
        navMeshAgent.isStopped = false;
        if (isPaused)
        {
            pauseTimer += Time.deltaTime;
            if (pauseTimer >= pauseDuration)
            {
                pauseTimer = 0f;
                isPaused = false;
                wanderTarget = initialPosition + Random.insideUnitSphere * wanderRange;
                wanderTarget.y = transform.position.y;
                anim.SetBool("Walk", true);

                if (wanderTarget.x > initialPosition.x)
                {
                    spriteRend.flipX = false;
                }
                else if (wanderTarget.x < initialPosition.x)
                {
                    spriteRend.flipX = true;
                }
                        

            }
            else
            {
                anim.SetBool("Walk", false);
            }
            return;
        }

        float distanceToTarget = Vector3.Distance(transform.position, wanderTarget);
        if (distanceToTarget < 3f)
        {
            isPaused = true;
        }
        else
        {
            Vector3 direction = wanderTarget - transform.position;
            bool isMovingLeft = direction.x < 0f;
            spriteRend.flipX = isMovingLeft;
           
            navMeshAgent.SetDestination(wanderTarget);
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < pursuitRange && Phc.IsInv == false)
        {
            pursuitTimer = Time.time;
            currentState = 1;
        }
    }

    void PursuitState()
    {
        anim.SetBool("Walk", true);

        navMeshAgent.SetDestination(player.transform.position);
        navMeshAgent.isStopped = false;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        Vector3 directionToPlayer = player.transform.position - transform.position;

       if(Phc.IsInv == false)
       {
            if (distanceToPlayer < attackRange && Mathf.Abs(directionToPlayer.z) < Mathf.Abs(directionToPlayer.x))
            {
                currentState = 2;
            }
            else if (distanceToPlayer > pursuitRange)
            {
                currentState = 0;
            }

            if (directionToPlayer.x > 0)
            {
                spriteRend.flipX = false;
            }
            else if (directionToPlayer.x <= 0)
            {
                spriteRend.flipX = true;
            }

            if (Time.time - pursuitTimer > Random.Range(2f, 4f))
            {

                if (CDcontrol == 100f)
                {
                    currentState = 5;
                }
                else
                {
                    currentState = 4;
                }
            }



        } 
        else if (Phc.IsInv == true)
        {
            currentState = 0;
        }
    }

    protected virtual void AttackState()
    {
        navMeshAgent.isStopped = true;
        if (!isAttacking && timeSinceLastAttack >= attackCooldown)
        {
            timeSinceLastAttack = 0f;
            anim.SetTrigger("Attack");
            anim.SetBool("Walk", false);
            //hitBox.SetActive(true);
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

    IEnumerator HitstunState()
    {
        navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(hitstun);



        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        Vector3 directionToPlayer = player.transform.position - transform.position;

        if (distanceToPlayer < attackRange && Mathf.Abs(directionToPlayer.z) < Mathf.Abs(directionToPlayer.x))
        {
            currentState = 2;
        }
        if (distanceToPlayer > attackRange || Mathf.Abs(directionToPlayer.z) >= Mathf.Abs(directionToPlayer.x))
        {
            pursuitTimer = Time.time;
            currentState = 1;
        }
        else if (distanceToPlayer > pursuitRange)
        {
            currentState = 0;
        }
    }

    IEnumerator CooldownState(float duration)
    {
        Debug.Log("stopped");
        anim.SetBool("Walk", false);
        navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(duration);
        Debug.Log("back");
        
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        Vector3 directionToPlayer = player.transform.position - transform.position;

        if (distanceToPlayer < attackRange && Mathf.Abs(directionToPlayer.z) < Mathf.Abs(directionToPlayer.x))
        {
            currentState = 2;
        }
        if (distanceToPlayer > attackRange || Mathf.Abs(directionToPlayer.z) >= Mathf.Abs(directionToPlayer.x))
        {
            pursuitTimer = Time.time;
            currentState = 1;
        }
        else if (distanceToPlayer > pursuitRange)
        {
            currentState = 0;
        }
    }

    protected virtual void SpecialAttack()
    {
        if (!isAttacking && timeSinceLastAttack >= attackCooldown)
        {
            isAttacking = true;
            timeSinceLastAttack = 0;
            anim.SetTrigger("Special");
            anim.SetBool("Walk", false);
            //specialHitBox.SetActive(true);
            
            //StartCoroutine(EndAttack(atkEndDelay));
            
        }
        
    }

    public void FleeState()
    {
        //this stage goes to pursuit - pursuit goes to attack
        navMeshAgent.isStopped = false;
        anim.SetBool("Walk", true);

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        Vector3 directionToPlayer = player.transform.position - transform.position;
        Vector3 awayFromPlayerDestination = transform.position - directionToPlayer.normalized * distanceToPlayer;
        navMeshAgent.SetDestination(awayFromPlayerDestination);
        //attackstate brings to this if player is too close
        //attack range is big easily enters it
        //need a flee range smaller than attack range to come here
        //melee enemies should have flee range = 0

        if (distanceToPlayer > fleeRange)
        {
            pursuitTimer = Time.time;
            currentState = 1;
        }
      
    }

    /*
    public IEnumerator EndAttack(float dur)
    {
        yield return new WaitForSeconds(dur);
        isAttacking = false;
        hitBox.SetActive(false);
        specialHitBox.SetActive(false);
        currentState = 4;
    }
    */

    public void EnableHitbox()
    {
        hitBox.SetActive(true);
        isAttacking = true;
    }
    public void EnableSpecialHitbox()
    {
        hitBox.SetActive(true);
        isAttacking = true;
        CDcontrol = 0f;
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized; //usar lerp
        rb.isKinematic = false;
        navMeshAgent.enabled = false;

        rb.AddForce(directionToPlayer * 20f, ForceMode.Impulse);
        
    }
    public void DisableHitbox()
    {
        hitBox.SetActive(false);
        isAttacking = false;
        specialHitBox.SetActive(false);
        currentState = 4;
    }
    public void DisableSpecialHitbox()
    {
        isAttacking = true;
        specialHitBox.SetActive(false);
        navMeshAgent.enabled = true;
        rb.isKinematic = true;
        currentState = 4;
    }

    public void KeepDisableHitbox()
    {
        hitBox.SetActive(false);
        specialHitBox.SetActive(false);
        isAttacking = false;
    }
    public void Knockback(Vector3 direction, float force, float duration)
    {
         
        navMeshAgent.enabled = false;
        rb.isKinematic = false;
        direction.Normalize();
        
        
        rb.AddForce(direction * force, ForceMode.Impulse);        
        StartCoroutine(ResetKnockback(duration));
        
        
    }
    IEnumerator ResetKnockback(float dur)
    {
        yield return new WaitForSeconds(dur);

        while (grounded)
        {
            yield return new WaitForEndOfFrame();
            navMeshAgent.enabled = true;
            rb.isKinematic = true;
            Debug.Log("chegou aqui");
            if (fleeRange != 0)
            {
                currentState = 6;
            }
            break;
        }   
    }

    void CheckGrounded()
    {
        
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
        if (Physics.Raycast(rayOrigin, Vector3.down, groundCheckDistance, Ground))
        {           
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    public void Stun(float TimerStun)
    {
        hitstun = TimerStun * 5;
        currentState = 3;
    }

    void SendInCombatMessage()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < pursuitRange && Phc.IsInv == false)
        {
            Phc.EnterCombat();           
        }
    }

    public bool IsPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag(stateName) &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }

    public void EndSpawning()
    {
        currentState = 1;
        navMeshAgent.enabled = true;
        capCollidier.enabled = true;
        Debug.Log("finished spawning");
    }

    public void SpawningState()
    {
        navMeshAgent.enabled = false;
        capCollidier.enabled = false;
    }


}


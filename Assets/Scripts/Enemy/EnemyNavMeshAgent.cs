using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyNavMeshAgent : MonoBehaviour
{
    
    public Transform player;
    public float attackRange = 2f;
    public float pursuitRange = 8f;
    public float wanderRange = 10f;
    public float attackCooldown = 2f;

    private Vector3 wanderTarget;
    private Vector3 initialPosition;
    public float timeSinceLastAttack = 0f;
    public int currentState = 0; // 0 = wander, 1 = pursuit, 2 = attack
    public bool isAttacking = false;

    public Animator anim;
    private SpriteRenderer spriteRend;

    private NavMeshAgent navMeshAgent;
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
    float pursuitTimer;
    [SerializeField] float specialCooldown = 100f;
    public float CDcontrol = 0f;
    public GameObject specialHitBox;

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
    }

    void Update()
    {
        CDcontrol = Mathf.Clamp(CDcontrol + 4f * Time.deltaTime, 0f, specialCooldown);
        
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
                StartCoroutine(SpecialAttack());
                break;
        }

        hitBoxPosLeft.transform.position = new Vector3(transform.position.x + 0.95f, transform.position.y, transform.position.z);
        hitBoxPosRight.transform.position = new Vector3(transform.position.x - 0.828f, transform.position.y, transform.position.z);

        if (spriteRend.flipX == false)
        {
            hitBox.transform.position = hitBoxPosRight.transform.position;
        }
        if (spriteRend.flipX == true)
        {
            hitBox.transform.position = hitBoxPosLeft.transform.position;
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
                    spriteRend.flipX = true;
                }
                else if (wanderTarget.x < initialPosition.x)
                {
                    spriteRend.flipX = false;
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
            bool isMovingRight = direction.x > 0f;
            spriteRend.flipX = isMovingRight;
           
            navMeshAgent.SetDestination(wanderTarget);
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < pursuitRange)
        {
            pursuitTimer = Time.time;
            currentState = 1;
        }
    }

    void PursuitState()
    {
        anim.SetBool("Walk", true);

        navMeshAgent.SetDestination(player.position);
        navMeshAgent.isStopped = false;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Vector3 directionToPlayer = player.position - transform.position;

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
            spriteRend.flipX = true;
        }
        else if (directionToPlayer.x <= 0)
        {
            spriteRend.flipX = false;
        }

        if (Time.time - pursuitTimer > Random.Range(3f, 8f))
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

    void AttackState()
    {
        navMeshAgent.isStopped = true;
        if (!isAttacking && timeSinceLastAttack >= attackCooldown)
        {
            isAttacking = true;
            timeSinceLastAttack = 0f;
            anim.SetBool("Attack", true);
            anim.SetBool("Walk", false);
            hitBox.SetActive(true);
            StartCoroutine(EndAttack());
            
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Vector3 directionToPlayer = player.position - transform.position;
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


        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Vector3 directionToPlayer = player.position - transform.position;

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
        anim.SetBool("Attack", false);
        anim.SetBool("Walk", false);
        navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(duration);
        Debug.Log("back");
        
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Vector3 directionToPlayer = player.position - transform.position;

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

    public virtual IEnumerator SpecialAttack()
    {
        if (!isAttacking && timeSinceLastAttack >= attackCooldown)
        {
            isAttacking = true;
            timeSinceLastAttack = 0;
            anim.SetBool("Attack", true);
            anim.SetBool("Walk", false);
            specialHitBox.SetActive(true);
            CDcontrol = 0f;
            yield return new WaitForSeconds(1f);
            Vector3 directionToPlayer = (player.position - transform.position).normalized; //usar lerp
            rb.isKinematic = false;
            navMeshAgent.enabled = false;
            
            rb.AddForce(directionToPlayer * 20f, ForceMode.Impulse);
            anim.SetBool("Attack", false);
            yield return new WaitForSeconds(2f);
            navMeshAgent.enabled = true;
            rb.isKinematic = true;
            StartCoroutine(EndAttack());
            
        }
        
    }


    IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
        anim.SetBool("Attack", false);
        hitBox.SetActive(false);
        specialHitBox.SetActive(false);
        currentState = 4;
    }

    public void Knockback(Vector3 direction, float force, float duration)
    {
        /*
        Vector3 horizontal = direction.x * transform.forward;
        Vector3 vertical = direction.y * transform.up;
        Vector3 newDirection = new Vector3(horizontal.x, -vertical.y, 0);
        Vector3 knockbackVector = (newDirection.normalized * force);
        Debug.Log("lançado");
        StartCoroutine(MoveOverTime(knockbackVector, duration));
        */
        
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

    
}


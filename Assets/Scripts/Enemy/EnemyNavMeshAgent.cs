using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyNavMeshAgent : MonoBehaviour
{
    
    public Transform player;
    public float attackRange = 0.5f;
    public float pursuitRange;
    public float wanderRange = 10f;
    public float attackCooldown = 2f;

    private Vector3 wanderTarget;
    private float timeSinceLastAttack = 0f;
    public int currentState = 0; // 0 = wander, 1 = pursuit, 2 = attack
    private bool isAttacking = false;

    private Animator anim;
    private SpriteRenderer spriteRend;

    private NavMeshAgent navMeshAgent;
    private Rigidbody rb;

    [SerializeField] GameObject hitBox;
    [SerializeField] GameObject hitBoxPosLeft;
    [SerializeField] GameObject hitBoxPosRight;

    private Transform spriteTransform;

    float pauseDuration = 2f;
    float pauseTimer = 0f;
    bool isPaused = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        spriteRend = GetComponentInChildren<SpriteRenderer>();
        spriteTransform = this.gameObject.transform.GetChild(0);

        hitBox.SetActive(false);
        wanderTarget = Random.insideUnitSphere * wanderRange;
        wanderTarget.y = transform.position.y;
    }

    void Update()
    {
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
        if (isPaused)
        {
            pauseTimer += Time.deltaTime;
            if (pauseTimer >= pauseDuration)
            {
                pauseTimer = 0f;
                isPaused = false;
                wanderTarget = Random.insideUnitSphere * wanderRange;
                wanderTarget.y = transform.position.y;
                anim.SetBool("Walk", true);
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
            currentState = 1;
        }
    }

    void PursuitState()
    {
        anim.SetBool("Walk", true);

        navMeshAgent.SetDestination(player.position);

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
    }

    void AttackState()
    {
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
            currentState = 1;
        }
    }

    IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
        currentState = 1;
        anim.SetBool("Attack", false);
        hitBox.SetActive(false);
    }

    public void Knockback(Vector3 direction, float force, float duration)
    {
        Vector3 horizontal = direction.x * transform.forward;
        Vector3 vertical = direction.y * transform.up;
        Vector3 newDirection = new Vector3(horizontal.x, -vertical.y, 0);
        Vector3 knockbackVector = (newDirection.normalized * force);
        Debug.Log("lançado");
        StartCoroutine(MoveOverTime(knockbackVector, duration));
    }

    private IEnumerator MoveOverTime(Vector3 knockbackVector, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position += knockbackVector * Time.deltaTime / duration;
            elapsedTime += Time.deltaTime;
            yield return null;
            //RigidBodyOnOff(false);
        }
    }
    

    void RigidBodyOnOff(bool turnOn)
    {
        if (turnOn)
        {
            rb.isKinematic = true;
            navMeshAgent.enabled = false;
        }
        else if (!turnOn)
        {
            rb.isKinematic = false;
            navMeshAgent.enabled = true;
        }
    }
}


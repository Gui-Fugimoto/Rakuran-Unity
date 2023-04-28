using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float attackRange = 0.5f;
    public float pursuitRange;
    public float wanderRange = 10f;
    public float attackCooldown = 2f;

    private Vector3 wanderTarget;
    private float timeSinceLastAttack = 0f;
    public int currentState = 0; // 0 = wander, 1 = pursuit, 2 = attack
    private bool isAttacking = false;
    private Rigidbody rb;
    private Vector3 targetPosition;

    private Animator anim;
    private Transform spriteTransform;
    private SpriteRenderer spriteRend;

    void Start()
    {
        // Set a random target location for wandering
        wanderTarget = Random.insideUnitSphere * wanderRange;
        wanderTarget.y = transform.position.y;

        // Get the Rigidbody component for physics
        rb = GetComponent<Rigidbody>();

        anim = GetComponentInChildren<Animator>();
        spriteTransform = this.gameObject.transform.GetChild(0);
        spriteRend = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        spriteTransform.rotation = Quaternion.Euler(0, 0, 0);
        targetPosition = new Vector3(player.position.x, 0, player.position.z);
        // Update the state machine based on the current state
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
    }

    void WanderState()
    {
        // Move towards wander target
        float distanceToTarget = Vector3.Distance(transform.position, wanderTarget);
        if (distanceToTarget < 3f)
        {
            // Set a new random target location for wandering
            wanderTarget = Random.insideUnitSphere * wanderRange;
            wanderTarget.y = transform.position.y;
        }
        else
        {

            transform.LookAt(wanderTarget);
            rb.MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime);
        }

        // Check if player is within pursuit range
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < pursuitRange)
        {
            currentState = 1; // Switch to pursuit state
        }
    }

    void PursuitState()
    {
        // Move towards player
        transform.LookAt(targetPosition);//player);
        rb.MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime);

        // Check if player is within attack range and facing on Z axis
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Vector3 directionToPlayer = player.position - transform.position;
        if (distanceToPlayer < attackRange && Mathf.Abs(directionToPlayer.z) < Mathf.Abs(directionToPlayer.x))
        {
            currentState = 2; // Switch to attack state
        }
        else if (distanceToPlayer > pursuitRange)
        {
            currentState = 0; // Switch back to wander state
        }
    }

    void AttackState()
    {
        // Attack if not already attacking and cooldown has expired
        if (!isAttacking && timeSinceLastAttack >= attackCooldown)
        {
            isAttacking = true;
            timeSinceLastAttack = 0f;
            anim.SetBool("Attack", true);
            // Play attack animation or perform attack action
            StartCoroutine(EndAttack());
        }

        // Check if player is still within attack range and facing on Z axis
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Vector3 directionToPlayer = player.position - transform.position;
        if (distanceToPlayer > attackRange || Mathf.Abs(directionToPlayer.z) >= Mathf.Abs(directionToPlayer.x))
        {
            currentState = 1; // Switch back to pursuit state
            
        }
    }

    

    IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(0.5f); // Insert animation length or attack duration here
        isAttacking = false;
        currentState = 1; // Switch back to pursuit state
        anim.SetBool("Attack", false);
    }

    public void Knockback(Vector3 direction, float force, float duration)
    {
        
        Vector3 horizontal = direction.x * transform.forward;
        Vector3 vertical = direction.y * transform.up;
        
        Vector3 newDirection = new Vector3(horizontal.x, -vertical.y, 0);
        Vector3 knockbackVector = (newDirection.normalized * force);
        Debug.Log("lan�ado");

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
        }
    }
}


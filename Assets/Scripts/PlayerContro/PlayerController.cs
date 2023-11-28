using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region variáveis
    private string moveInputAxis = "Vertical";
    private string turnInputAxis = "Horizontal";

    public float moveSpeedX;
    public float moveSpeedY;
    private float baseMoveSpeedX;
    private float baseMoveSpeedY;
    public bool flipped;
    public bool InInventory;

    [SerializeField] private float gravity = 0.25f;
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float terminalVelocity = 5.0f;
    [SerializeField] private float SpeedBonus;
    private float verticalVelocity;

    [SerializeField] private bool grounded;


    private CharacterController controller;
    private Vector3 slopeNormal;
    public bool isSprinting;
    public bool isJumping;
    public Animator anim;
    public SpriteRenderer playerSprite;
    public PlayerCombat playerCombatScript;

    Vector3 movement;

    [Header("Ground Check Raycast")]
    [SerializeField] private float extremitiesOffset = 0.05f;
    [SerializeField] private float innerVerticalOffset = 0.25f;
    [SerializeField] private float distanceGrounded = 0.15f;
    [SerializeField] private float slopeThreshold = 0.55f;

  
    private float initialSprintTimer;
    private float holdButtonSprintTimer;

    //Variaveis Dodge
    public float rollDistance = 3f; // distance to roll
    public float rollDuration = 0.5f; // duration of the roll
    public float staminaCost = 10f; // stamina cost of the roll
    public float invulnerabilityDuration = 1f; // duration of invulnerability after the roll
    public KeyCode dodgeKey = KeyCode.LeftShift; // key to trigger the dodge roll
    public bool invulnerable;

    private bool isRolling = false; // whether the player is currently rolling
    private Vector3 rollDirection; // direction of the roll
    private float rollStartTime; // time when the roll started
    public float rollSpeedBoost = 5f;
    private float invulnerabilityEndTime; // time when invulnerability ends

    //Stamina system variaveis

    public float maxStamina = 100f;
    public float staminaRegenRate = 10f;

    public float currentStamina;

    public SaveFile currentSave;
    public Transform FirstSpawnPos;

    private LayerMask Ground;

    #endregion

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        currentStamina = maxStamina;
        
    }

    void Start()
    {
        currentSave = FindObjectOfType<GameController>().Save;
        baseMoveSpeedX = moveSpeedX;
        baseMoveSpeedY = moveSpeedY;
        playerCombatScript = GetComponentInChildren<PlayerCombat>();
        SpeedBonus = 1;
        Ground = LayerMask.GetMask("Ground");
    }
    
    public void Spawn()
    {
        if (currentSave.CPpos != new Vector3(0, 0, 0))
        {
            Debug.Log("spawnei");
            transform.position = currentSave.CPpos;
        }
        else
        {
            currentSave.CPpos = FirstSpawnPos.position;
        }
    }
    
public void speedPotion()
    {
        StartCoroutine(SpeedBuff());
        Debug.Log("bebeuVeloz");
    }

    IEnumerator SpeedBuff()
    {
        SpeedBonus = 1.5f;
        yield return new WaitForSeconds(10f);
        SpeedBonus = 1;
    }

    void Update()
    {
        if (isSprinting == false)
        {
            //moveSpeedX = baseMoveSpeedX;
            //moveSpeedY = baseMoveSpeedY;
            if (InInventory == false)
            {
               
                Move(!playerCombatScript.isAttacking);
                DodgeRoll();
            }
           
        }       
       

        if (Input.GetAxis(turnInputAxis) == 0 && Input.GetAxis(moveInputAxis) == 0)
        {
            isRolling = false;
        }
        currentStamina = Mathf.Clamp(currentStamina + staminaRegenRate * Time.deltaTime, 0f, maxStamina);
        PegasusSprint();

        // Apply verticalVelocity to our movement vector
        movement.y = verticalVelocity;
        anim?.SetFloat("VerticalVelocity", verticalVelocity);

        // If we're on the floor, angle our vector to match its curves
        if (slopeNormal != Vector3.up) movement = FollowFloor(movement);

        // Finaly move the controller, this also checks for collisions
        if (!playerCombatScript.isAttacking)
        {
            controller.Move(movement * Time.deltaTime);
        }

    }

    private void FixedUpdate()
    {
        if(InInventory == false && !playerCombatScript.isAttacking)
        {
            Jump();
            grounded = Grounded();
            if (!grounded)
            {
                isJumping = true;
            }
            else
            {
                isJumping = false;
            }
        }
    }

    private void Move(bool canmove)
    {

        float moveAxis = Input.GetAxis(moveInputAxis);
        float turnAxis = Input.GetAxis(turnInputAxis);
        movement = new Vector3(turnAxis  * moveSpeedX * SpeedBonus, 0, moveAxis * moveSpeedY * SpeedBonus);
        movement = Vector3.ClampMagnitude(movement, moveSpeedX*SpeedBonus);


        if (canmove)
        {
          // transform.Translate(movement * Time.deltaTime);

        }
        else
        {
            Debug.Log("not");
        }



        if (moveAxis != 0)
        {
            anim.SetBool("animMove", true);
            
        }
        else if (moveAxis == 0)
        {
            anim.SetBool("animMove", false);
        }


        //Controla a dire��o do sprite quando o player est� se movendo
        if (turnAxis != 0 && turnAxis < 0 && !playerCombatScript.isAttacking)
        {
            playerSprite.flipX = true;
            anim.SetBool("animMove", true);
            flipped = true;
        }
        else if (turnAxis != 0 && turnAxis > 0 && !playerCombatScript.isAttacking)
        {
            playerSprite.flipX = false;
            anim.SetBool("animMove", true);
            flipped = false;
        }

    }



    public bool Grounded()
    {
        if (verticalVelocity > 0)
            return false;

        float yRay = (controller.bounds.center.y - (controller.height * 0.5f)) // Bottom of the character controller
                     + innerVerticalOffset;

        RaycastHit hit;

        // Mid
        if (Physics.Raycast(new Vector3(controller.bounds.center.x, yRay, controller.bounds.center.z), -Vector3.up, out hit, innerVerticalOffset + distanceGrounded))
        {
            Debug.DrawRay(new Vector3(controller.bounds.center.x, yRay, controller.bounds.center.z), -Vector3.up * (innerVerticalOffset + distanceGrounded), Color.red);
            slopeNormal = hit.normal;
            return (slopeNormal.y > slopeThreshold) ? true : false;
        }
        // Front-Right
        if (Physics.Raycast(new Vector3(controller.bounds.center.x + (controller.bounds.extents.x - extremitiesOffset), yRay, controller.bounds.center.z + (controller.bounds.extents.z - extremitiesOffset)), -Vector3.up, out hit, innerVerticalOffset + distanceGrounded))
        {
            slopeNormal = hit.normal;
            return (slopeNormal.y > slopeThreshold) ? true : false;
        }
        // Front-Left
        if (Physics.Raycast(new Vector3(controller.bounds.center.x - (controller.bounds.extents.x - extremitiesOffset), yRay, controller.bounds.center.z + (controller.bounds.extents.z - extremitiesOffset)), -Vector3.up, out hit, innerVerticalOffset + distanceGrounded))
        {
            slopeNormal = hit.normal;
            return (slopeNormal.y > slopeThreshold) ? true : false;
        }
        // Back Right
        if (Physics.Raycast(new Vector3(controller.bounds.center.x + (controller.bounds.extents.x - extremitiesOffset), yRay, controller.bounds.center.z - (controller.bounds.extents.z - extremitiesOffset)), -Vector3.up, out hit, innerVerticalOffset + distanceGrounded))
        {
            slopeNormal = hit.normal;
            return (slopeNormal.y > slopeThreshold) ? true : false;
        }
        // Back Left
        if (Physics.Raycast(new Vector3(controller.bounds.center.x - (controller.bounds.extents.x - extremitiesOffset), yRay, controller.bounds.center.z - (controller.bounds.extents.z - extremitiesOffset)), -Vector3.up, out hit, innerVerticalOffset + distanceGrounded))
        {
            slopeNormal = hit.normal;
            return (slopeNormal.y > slopeThreshold) ? true : false;
        }

        return false;
    }

    private Vector3 FollowFloor(Vector3 moveVector)
    {
        Vector3 right = new Vector3(slopeNormal.y, -slopeNormal.x, 0).normalized;
        Vector3 forward = new Vector3(0, -slopeNormal.z, slopeNormal.y).normalized;
        return right * moveVector.x + forward * moveVector.z;
    }

    private void PegasusSprint()
    {
        holdButtonSprintTimer = 1f;
        if (Input.GetKeyDown(KeyCode.LeftControl)) //Depois, criar input Sprint no input manager Input.GetAxis("Sprint") != 0)
        {
            initialSprintTimer = Time.time;
            //Debug.Log("Carregando");
            //anim.SetBool("sprintCharge", true);
        }

        else if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Time.time - initialSprintTimer > holdButtonSprintTimer)
            {
                //by making it positive inf, we won't subsequently run this code by accident,
                //since X - +inf = -inf, which is always less than holdDur
                //initialSprintTimer = float.PositiveInfinity;


                //perform your action
                //Debug.Log("Correndo");
                float moveAxis = Input.GetAxis(moveInputAxis);
                isSprinting = true;
                if (flipped == false)
                {
                    moveSpeedX = 20;
                    moveSpeedY = 3;
                    movement = new Vector3(moveSpeedX * SpeedBonus, 0, moveAxis * moveSpeedY);
                    movement = Vector3.ClampMagnitude(movement, moveSpeedX * SpeedBonus);
                    transform.Translate(movement * Time.deltaTime);
                }
                else if (flipped == true)
                {
                    moveSpeedX = 20;
                    moveSpeedY = 3;
                    movement = new Vector3(-moveSpeedX * SpeedBonus, 0, moveAxis * moveSpeedY);
                    movement = Vector3.ClampMagnitude(movement, moveSpeedX * SpeedBonus);
                    transform.Translate(movement * Time.deltaTime);
                }

                anim.SetBool("Run", true);
                //anim.SetBool("sprintCharge", false);
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            initialSprintTimer = float.PositiveInfinity;
            //Debug.Log("parou");
            isSprinting  = false;
            moveSpeedX = 4;
            moveSpeedY = 4;
            anim.SetBool("Run", false);
            //anim.SetBool("sprintCharge", false);
        }
        else
        {
            initialSprintTimer = float.PositiveInfinity;
        }
    }

    private void Jump()
    {
        
        anim?.SetBool("Grounded", grounded);
        if (grounded)
        {
            // Apply slight gravity
            verticalVelocity = -1;
            
            // If spacebar, apply high negative gravity, and forget about the floor
            if (Input.GetAxis("Jump") != 0) //&& sprinting == false)
            {
                verticalVelocity = jumpForce;
                slopeNormal = Vector3.up;
                anim.SetTrigger("Jump");
                
            }
        }
        else
        {
            // Gradualy increment gravity
            verticalVelocity -= gravity;
            slopeNormal = Vector3.up;
            
            if (verticalVelocity <= 0 && !playerCombatScript.isAttacking)
            {
                
                anim.SetTrigger("Falling");
            }
            // Clamp to match terminal velocity, if faster
            if (verticalVelocity < -terminalVelocity)
            {
                verticalVelocity = -terminalVelocity;
                
            }
                
        }
    }
    private void DodgeRoll()
    {

        if (Input.GetKeyDown(dodgeKey) && !isRolling && UseStamina(staminaCost))
        {
            // Calculate the direction of the roll based on the player's movement direction
            Vector3 movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (movementDirection.magnitude > 0)
            {
                rollDirection = movementDirection.normalized;
                isRolling = true;
                rollStartTime = Time.time;
                invulnerabilityEndTime = Time.time + invulnerabilityDuration;

                // Deduct stamina cost
                //PlayerStamina.DeductStamina(staminaCost);
            }
        }

        // Perform the roll
        if (isRolling)
        {
            float rollProgress = (Time.time - rollStartTime) / rollDuration;
            if (rollProgress < 1)
            {
                // Move the player in the roll direction and apply a speed boost
                Vector3 rollVelocity = rollDirection * rollDistance / rollDuration;
                controller.Move((rollVelocity + rollDirection * rollSpeedBoost) * Time.deltaTime);
                anim.Play("Raku_Dash");
                // Rotate the player to face the roll direction
                //transform.rotation = Quaternion.LookRotation(rollDirection);
                invulnerable = true;
            }
            else
            {
                isRolling = false;
                invulnerable = false;
            }
        }

        // Set invulnerability
        //if (Time.time < invulnerabilityEndTime)
        //{
        //    Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemy"), true);
        //}
        //else
        //{
        //    Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemy"), false);
        //}
    }


    
    public bool UseStamina(float amount)
    {
        if (currentStamina >= amount)
        {
            currentStamina -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Knockback(Vector3 direction, float force, float duration)
    {

        Vector3 horizontal = direction.x * transform.forward;
        Vector3 vertical = direction.y * transform.up;

        Vector3 newDirection = new Vector3(horizontal.x, -vertical.y, 0);
        Vector3 knockbackVector = (newDirection.normalized * force);
        //Debug.Log("lançado");
        //mudar para Addforce()

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

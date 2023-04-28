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
    public bool flipped;

    [SerializeField] private float gravity = 0.25f;
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float terminalVelocity = 5.0f;
    private float verticalVelocity;

    private bool grounded;

    private CharacterController controller;
    private Vector3 slopeNormal;
    public bool isSprinting;
    public bool isJumping;
    public Animator anim;
    public SpriteRenderer playerSprite;

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
    public KeyCode dodgeKey = KeyCode.E; // key to trigger the dodge roll

    private bool isRolling = false; // whether the player is currently rolling
    private Vector3 rollDirection; // direction of the roll
    private float rollStartTime; // time when the roll started
    public float rollSpeedBoost = 5f;
    private float invulnerabilityEndTime; // time when invulnerability ends

    //Stamina system variaveis

    public float maxStamina = 100f;
    public float staminaRegenRate = 10f;

    public float currentStamina;

    


    #endregion

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        currentStamina = maxStamina;
    }
    void Start()
    {
        
    }


    void Update()
    {
        if (isSprinting == false)
        {
            Move();
            DodgeRoll();
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
        controller.Move(movement * Time.deltaTime);
    }

    private void FixedUpdate()
    {

        Jump();
        grounded = Grounded();
    }

    private void Move()
    {

        float moveAxis = Input.GetAxis(moveInputAxis);
        float turnAxis = Input.GetAxis(turnInputAxis);
        movement = new Vector3(turnAxis  * moveSpeedX, 0, moveAxis * moveSpeedY);
        movement = Vector3.ClampMagnitude(movement, moveSpeedX);
        


        transform.Translate(movement * Time.deltaTime);



        if (moveAxis != 0)
        {
            anim.SetBool("animMove", true);
            
        }
        else if (moveAxis == 0)
        {
            anim.SetBool("animMove", false);
        }


        //Controla a dire��o do sprite quando o player est� se movendo
        if (turnAxis != 0 && turnAxis < 0)
        {
            playerSprite.flipX = true;
            anim.SetBool("animMove", true);
            flipped = true;
        }
        else if (turnAxis != 0 && turnAxis > 0)
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
        if (Input.GetKeyDown(KeyCode.LeftShift)) //Depois, criar input Sprint no input manager Input.GetAxis("Sprint") != 0)
        {
            initialSprintTimer = Time.time;
            //Debug.Log("Carregando");
            //anim.SetBool("sprintCharge", true);
        }

        else if (Input.GetKey(KeyCode.LeftShift))
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
                    movement = new Vector3(moveSpeedX, 0, moveAxis * moveSpeedY);
                    movement = Vector3.ClampMagnitude(movement, moveSpeedX);
                    transform.Translate(movement * Time.deltaTime);
                }
                else if (flipped == true)
                {
                    moveSpeedX = 20;
                    moveSpeedY = 3;
                    movement = new Vector3(-moveSpeedX, 0, moveAxis * moveSpeedY);
                    movement = Vector3.ClampMagnitude(movement, moveSpeedX);
                    transform.Translate(movement * Time.deltaTime);
                }

                anim.SetBool("Run", true);
                //anim.SetBool("sprintCharge", false);
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            initialSprintTimer = float.PositiveInfinity;
            //Debug.Log("parou");
            isSprinting  = false;
            moveSpeedX = 5;
            moveSpeedY = 5;
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
                anim?.SetTrigger("Jump");
            }
        }
        else
        {
            // Gradualy increment gravity
            verticalVelocity -= gravity;
            slopeNormal = Vector3.up;

            // Clamp to match terminal velocity, if faster
            if (verticalVelocity < -terminalVelocity)
                verticalVelocity = -terminalVelocity;
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

                // Rotate the player to face the roll direction
                //transform.rotation = Quaternion.LookRotation(rollDirection);

            }
            else
            {
                isRolling = false;
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

}

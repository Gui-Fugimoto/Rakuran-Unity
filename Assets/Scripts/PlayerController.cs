using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private string moveInputAxis = "Vertical";
    private string turnInputAxis = "Horizontal";

    public float moveSpeedX;
    public float moveSpeedY;

    [SerializeField] private float gravity = 0.25f;
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float terminalVelocity = 5.0f;
    private float verticalVelocity;

    private bool grounded;

    private CharacterController controller;
    private Vector3 slopeNormal;

    public Animator anim;
    public SpriteRenderer playerSprite;

    Vector3 movement;

    [Header("Ground Check Raycast")]
    [SerializeField] private float extremitiesOffset = 0.05f;
    [SerializeField] private float innerVerticalOffset = 0.25f;
    [SerializeField] private float distanceGrounded = 0.15f;
    [SerializeField] private float slopeThreshold = 0.55f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    void Start()
    {
        
    }


    void Update()
    {
        Move();

        grounded = Grounded();
        anim?.SetBool("Grounded", grounded);
        if (grounded)
        {
            // Apply slight gravity
            verticalVelocity = -1;

            // If spacebar, apply high negative gravity, and forget about the floor
            if (Input.GetKeyDown(KeyCode.Space))
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

        // Apply verticalVelocity to our movement vector
        movement.y = verticalVelocity;
        anim?.SetFloat("VerticalVelocity", verticalVelocity);

        // If we're on the floor, angle our vector to match its curves
        if (slopeNormal != Vector3.up) movement = FollowFloor(movement);

        // Finaly move the controller, this also checks for collisions
        controller.Move(movement * Time.deltaTime);
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
            Debug.Log("teste");
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
        }
        else if (turnAxis != 0 && turnAxis > 0)
        {
            playerSprite.flipX = false;
            anim.SetBool("animMove", true);
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

}

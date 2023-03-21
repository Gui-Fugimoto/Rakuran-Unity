using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollerMotor : MonoBehaviour
{
    [Header("Logic")]
    [SerializeField] private Animator anim;
    private Vector3 slopeNormal;
    private CharacterController controller;
    private bool grounded;

    [Header("Movement configuration")]
    [SerializeField] private float speedX = 5;
    [SerializeField] private float speedY = 5;
    [SerializeField] private float gravity = 0.25f;
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float terminalVelocity = 5.0f;
    private float verticalVelocity;

    [Header("Ground Check Raycast")]
    [SerializeField] private float extremitiesOffset = 0.05f;
    [SerializeField] private float innerVerticalOffset = 0.25f;
    [SerializeField] private float distanceGrounded = 0.15f;
    [SerializeField] private float slopeThreshold = 0.55f;

    private void Awake()
    {
        // Get the reference on top of the current object
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Look at which key the user is pressing, store it 
        Vector3 inputVector = PoolInput();

        // Multiply the inputs with the speed, and switch Y & Z
        Vector3 moveVector = new Vector3(inputVector.x * speedX, 0, inputVector.y * speedY);

        // Set the speed on the animator, before adding in the gravity values
        anim?.SetFloat("Speed", moveVector.magnitude);

        // Store it in a varriable, so we don't call it more than once per frame
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
        moveVector.y = verticalVelocity;
        anim?.SetFloat("VerticalVelocity",verticalVelocity);

        // If we're on the floor, angle our vector to match its curves
        if (slopeNormal != Vector3.up) moveVector = FollowFloor(moveVector);

        // Finaly move the controller, this also checks for collisions
        controller.Move(moveVector * Time.deltaTime);
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
    private Vector3 PoolInput()
    {
        Vector3 r = default(Vector3);

        r.x = Input.GetAxisRaw("Horizontal");
        r.y = Input.GetAxisRaw("Vertical");
        return r.normalized;
    }
}
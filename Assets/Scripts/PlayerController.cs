using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private string moveInputAxis = "Vertical";
    private string turnInputAxis = "Horizontal";
    public float moveSpeedX;
    public float moveSpeedY;
    private bool grounded;
    [SerializeField] Collider collider;
    public Animator anim;
    public SpriteRenderer playerSprite;
    Vector3 movement;



    void Start()
    {
        collider = GetComponent<Collider>();
    }


    void Update()
    {
        Move();
       
    }

    private void Move()
    {
        float moveAxis = Input.GetAxis(moveInputAxis);
        float turnAxis = Input.GetAxis(turnInputAxis);

        movement = new Vector3(turnAxis, 0, moveAxis);
        movement = Vector3.ClampMagnitude(movement, moveSpeedX);
        //movement = movement.normalized;


        transform.Translate(movement * moveSpeedX * Time.deltaTime);



        if (moveAxis != 0)
        {
            anim.SetBool("animMove", true);
            Debug.Log("teste");
        }
        else if (moveAxis == 0)
        {
            anim.SetBool("animMove", false);
        }


        //Controla a direção do sprite quando o player está se movendo
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



    /* public bool Grounded()
    {
        if (verticalVelocity > 0)
            return false;

        float yRay = (collider.bounds.center.y - (collider * 0.5f)) // Bottom of the character controller
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
    */


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private string moveInputAxis = "Vertical";
    private string turnInputAxis = "Horizontal";
    public float moveSpeed = 1;
    private bool grounded;

    public Animator anim;

    public SpriteRenderer playerSprite;
    Vector3 movement;
   
    void Start()
    {
        
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
        movement = Vector3.ClampMagnitude(movement, 1);
        //movement = movement.normalized;


        transform.Translate(movement * moveSpeed);


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
    

    
}

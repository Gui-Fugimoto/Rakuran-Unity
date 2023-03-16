using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private string moveInputAxis = "Vertical";
    private string turnInputAxis = "Horizontal";
    public float moveSpeed = 1;

    Animator anim;

    public SpriteRenderer playerSprite;
    Vector3 movement;
   
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    
    void Update()
    {
        float moveAxis = Input.GetAxis(moveInputAxis);
        float turnAxis = Input.GetAxis(turnInputAxis);

        movement = new Vector3(turnAxis, 0, moveAxis);
        //movement = Vector3.ClampMagnitude(movement, 1);
        movement = movement.normalized;

        Move();

        //Controla a direção do sprite quando o player está se movendo
        if (turnAxis != 0 && turnAxis < 0)
        {
            playerSprite.flipX = true;
        }
        else if (turnAxis != 0 && turnAxis > 0)
        {
            playerSprite.flipX = false;
        }
        

    }
    
    private void Move()
    {
        transform.Translate(movement * moveSpeed);
    }
    

    
}

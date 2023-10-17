using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionThrow : MonoBehaviour
{
    public float throwspeed;
    [SerializeField] float fallDistance;
    public ItemParameter Item;
    [SerializeField] SpriteRenderer Potion;
    PlayerController playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        Potion.sprite = Item.Icon;
        playerController = FindObjectOfType<PlayerController>();

        if (playerController.flipped == true)
        {
            throwspeed = throwspeed * -1;
        }

        if (playerController.flipped == false)
        {
            throwspeed = throwspeed * +1;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = transform.position + new Vector3(throwspeed * Time.deltaTime, 0, 0);
        transform.Rotate(0,0, 300 * Time.deltaTime);

      
    }



}

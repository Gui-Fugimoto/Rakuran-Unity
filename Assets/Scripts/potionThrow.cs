using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionThrow : MonoBehaviour
{
    [SerializeField] float throwspeed;
    [SerializeField] float fallDistance;
    public ItemParameter Item;
    [SerializeField] SpriteRenderer Potion;
    
    // Start is called before the first frame update
    void Start()
    {
        Potion.sprite = Item.Icon;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = transform.position + new Vector3(throwspeed * Time.deltaTime, 0, 0);
        transform.Rotate(0,0, 300 * Time.deltaTime);

      // if (Time.deltaTime >= fallDistance)
      // {
      //     transform.position = transform.position + new Vector3(throwspeed * Time.deltaTime, -1 * Time.deltaTime, 0);
      //     Debug.Log("caindo");
      // }
    }



}

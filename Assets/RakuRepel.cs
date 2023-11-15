using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RakuRepel : MonoBehaviour
{
    [SerializeField] SaveFile save;
    [SerializeField] bool inWater;
    [SerializeField] GameObject player;
    Collider thisCollider;


    private void Start()
    {
        save = FindObjectOfType<GameController>().Save;
        thisCollider = GetComponent<Collider>();
    }

    void FixedUpdate()
    {
      if (thisCollider.bounds.Contains(player.transform.position))
      {
          player.transform.position = save.CPpos;
      
      }
      
    }

   // private void OnTriggerStay(Collider other)
   // {
   //     if(other.gameObject.tag == "Player")
   //     {
   //         Debug.Log("entrei na agua");
   //         player.transform.position = new Vector3 (save.CPpos.x, save.CPpos.y, save.CPpos.z);
   //     }
   // }
   //
}

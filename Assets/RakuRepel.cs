using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RakuRepel : MonoBehaviour
{
    [SerializeField] bool inWater;
    [SerializeField] GameObject player;
    Collider thisCollider;
    public GameObject fadeOut;


    private void Start()
    {
        thisCollider = GetComponent<Collider>();
    }
    void FixedUpdate()
    {
      if (thisCollider.bounds.Contains(player.transform.position))
      {
            fadeOut.SetActive(true);
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

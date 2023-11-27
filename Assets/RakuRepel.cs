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
    public TeleportParkour FadeGrandOrder;

    private void Start()
    {
        thisCollider = GetComponent<Collider>();
        FadeGrandOrder = fadeOut.GetComponent<TeleportParkour>();
    }
     private void OnTriggerStay(Collider other)
     {
         if(other.gameObject.tag == "Player")
         {
            fadeOut.SetActive(true);
            StartCoroutine(TeleportThisFucker());
           
        }
     }
    
    IEnumerator TeleportThisFucker()
    {
        yield return new WaitForSeconds(0.5f);
        FadeGrandOrder.Teleport();
    }
}

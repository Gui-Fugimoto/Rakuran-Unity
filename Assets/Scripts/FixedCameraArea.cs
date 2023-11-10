using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCameraArea : MonoBehaviour
{
    public GameObject player;
    Collider thisCollider;
    public State_Controller stateCtrl;
    private float enterTimer;
    private bool fabianoOnce = true;
    public float exitTimerDelay = 0.2f;

    public GameObject thisFixedCamera;
    void Start()
    {
        thisCollider = GetComponent<Collider>();
        
    }

    void FixedUpdate()
    {
        if (thisCollider.bounds.Contains(player.transform.position))
        {
            EnterAreaTimer();
            
        }
        ExitAreaTimer();

    }
    void EnterAreaTimer()
    {
        enterTimer = Time.time;
        stateCtrl.inFixedArea = true;
        fabianoOnce = true;

        //descomentar isso quando tiver tudo pronto:
        //stateCtrl.fixedCamera = thisFixedCamera;
    }

    void ExitAreaTimer()
    {
        if (fabianoOnce && Time.time - enterTimer > exitTimerDelay)
        {
            fabianoOnce = false;
            stateCtrl.inFixedArea = false;
        }
    }
}

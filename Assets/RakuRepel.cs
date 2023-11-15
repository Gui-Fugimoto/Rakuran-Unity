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

}

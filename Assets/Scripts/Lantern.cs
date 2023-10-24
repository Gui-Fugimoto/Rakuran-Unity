using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{

    private GameObject attackPosRight;
    private GameObject attackPosLeft;
    private PlayerController controller;
    private bool cameraLock;


    void Start()
    {
        attackPosRight = GameObject.Find("AttackPosRight");
        attackPosLeft = GameObject.Find("AttackPosLeft");
        controller = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!controller.flipped)
        {
            gameObject.transform.position = attackPosRight.transform.position;
        }

        else if (controller.flipped)
        {
            gameObject.transform.position = attackPosLeft.transform.position;
        }
    }
}

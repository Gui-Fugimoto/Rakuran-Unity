using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFade : MonoBehaviour
{
    public GameObject FadeIn;
    public GameObject Player;
    public PlayerController controller;
    public void Start()
    {
        controller = FindObjectOfType<PlayerController>();
    }

    public void fadeInEnd()
    {
        FadeIn.SetActive(false);
    }

    public void SetplayerPosition()
    {
       if(controller != null)
        {
            controller.Spawn();
        }
    }

}

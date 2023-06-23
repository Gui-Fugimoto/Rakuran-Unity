using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public enum TypeOfInteraction
{
    E,
    WASD,
    Q,
    Tab,
    Space,
    Shift,
    Ctrl,
    num1234
}

    public TypeOfInteraction interaction;
    

    // Update is called once per frame
    void Update()
    {
        
    }
*/
public class InteractTutorial : MonoBehaviour
{
    [SerializeField] GameObject tutorialSprite;
    private void Start()
    {
        tutorialSprite.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tutorialSprite.SetActive(true);


        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tutorialSprite.SetActive(false);

        }
    }
}

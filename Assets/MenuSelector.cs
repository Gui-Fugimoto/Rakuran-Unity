using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelector : MonoBehaviour
{
    [SerializeField] GameObject Image1;
    [SerializeField] GameObject Image2;
    [SerializeField] GameObject Image3;
    [SerializeField] int selector;


    void Start()
    {
        selector = Random.Range(0, 3);

        switch (selector) 
        {
            case 0:
                Image1.SetActive(true);
                Image2.SetActive(false);
                Image3.SetActive(false);
                break;

            case 1:
                Image1.SetActive(false);
                Image2.SetActive(true);
                Image3.SetActive(false);
                break;

            case 2:
                Image1.SetActive(false);
                Image2.SetActive(false);
                Image3.SetActive(true);
                break;        
        }
    }

}

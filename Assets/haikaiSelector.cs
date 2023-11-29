using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class haikaiSelector : MonoBehaviour
{
    [SerializeField] GameController GameC;
    [SerializeField] GameObject HaikaiGO;
    [SerializeField] TMP_Text Haikai;
    [SerializeField] TMP_Text Nome;
    
    void Start()
    {
        GameC = FindObjectOfType<GameController>();
        Haikai.text = GameC.HaiKai;
        Nome.text = GameC.Name;
    }

    public void OnDisable()
    {
        HaikaiGO.SetActive(false);
    }

}

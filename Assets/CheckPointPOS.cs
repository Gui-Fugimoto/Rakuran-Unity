using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CheckPointPOS : MonoBehaviour
{
    public SaveFile Save;
    [SerializeField] float spawnSafeGuard;

    private void Start()
    {
        Save = FindObjectOfType<GameController>().Save;
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Save.CPpos = new Vector3 (gameObject.transform.position.x + spawnSafeGuard, gameObject.transform.position.y, gameObject.transform.position.z);
        }
    }
}

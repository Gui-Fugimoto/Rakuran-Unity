using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcIdleBounce : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector3 ScaleChange;
    [SerializeField] bool Deflate; 
    // Update is called once per frame
    void Update()
    {
        if (Deflate == false)
        {
            gameObject.transform.localScale += ScaleChange;
        }
        
        if (Deflate == true)
        {
            gameObject.transform.localScale -= ScaleChange;
        }

        if (gameObject.transform.localScale.y >= 5.95)
        {
            Deflate = true;
        }

        if (gameObject.transform.localScale.y <= 5.75)
        {
            Deflate = false;   
        }
    }
}

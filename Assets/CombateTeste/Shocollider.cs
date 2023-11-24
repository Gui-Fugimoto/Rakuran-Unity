using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shocollider : MonoBehaviour
{
    [SerializeField] bool showCollider;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        OnDrawGizmos();
    }
    void OnDrawGizmos()
    {
        if (showCollider)
        {
            Gizmos.color = Color.green;
            Gizmos.matrix = this.transform.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        }
    }
}

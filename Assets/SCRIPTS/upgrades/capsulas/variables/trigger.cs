using UnityEngine;
using System.Collections;

public class trigger : MonoBehaviour
{
    private CapsuleCollider cc;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 2)
        {
            cc = GetComponent<CapsuleCollider>();
            //cc.isTrigger = false; 
        }
    }

    
}

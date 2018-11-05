using UnityEngine;
using System.Collections;

public class bala : MonoBehaviour
{
    Vector3 movArriba = new Vector3(0, 10f, 0);
    private Rigidbody rb;
    // Use this for initialization
    void Start()
    {   
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(movArriba);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col) //al chocar algo..
    {
        DestroyObject(gameObject);
        //if (col.gameObject.name == "pelota(Clone)")
        //{
        //    DestroyObject(gameObject);
        //}
    }
}

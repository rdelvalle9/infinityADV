using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caidaCapsula : MonoBehaviour
{
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        float numeroRandom = Random.Range(-.022f, -.030f);
        rb.AddForce(0, numeroRandom, 0);
        //rb.AddForce(0, -.022f, 0);
    }

}
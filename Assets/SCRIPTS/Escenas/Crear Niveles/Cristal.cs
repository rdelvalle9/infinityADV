using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script del cristal verde
public class Cristal : MonoBehaviour {
    public AudioClip sonido;
    public float volumen = 1.0f;
    protected Transform posicion = null;
    public GameObject explosion;
    private Rigidbody rb;

    void Start()
    {
        hacerCaerCristal();
    }

    void Update()
    {
        Transform cristal = GetComponent<Transform>();
        cristal.transform.Rotate(new Vector3(3, 4, 2 * Time.deltaTime)); //rota el objeto
    }

    void OnTriggerEnter(Collider col)
    {
        GM scriptGM;
        if (col.gameObject.name == "Vaus")
        {
            if (sonido) AudioSource.PlayClipAtPoint(sonido, posicion.position, volumen);
            scriptGM = GameObject.Find("GM").GetComponent<GM>();//busca el objeto del script y lo asigna a la variable
            //Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    /* hacerCaerCristal = al iniciar mueve el objeto para abajo */
    void hacerCaerCristal()
    {
        posicion = transform;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(0, -100, 0); 
    }
}

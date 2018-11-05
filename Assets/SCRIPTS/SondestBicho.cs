using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// sonido q produce el bicho al morir
public class SondestBicho : MonoBehaviour
{
    public AudioClip sonido = null;
    public float volumen = 1.0f;
    protected Transform posicion = null;

    // Use this for initialization
    void Start()
    {
        posicion = transform;
    }

    // Update is called once per frame
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Vaus" || col.gameObject.name == "pelota" || col.gameObject.name == "bala(Clone)") //al chocar con la vaus
        {
            if (sonido) AudioSource.PlayClipAtPoint(sonido, posicion.position, volumen);
        }

    }
}




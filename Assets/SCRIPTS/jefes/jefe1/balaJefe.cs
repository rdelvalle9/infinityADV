using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balaJefe: MonoBehaviour {

    private Rigidbody rb;
    public GameObject explosion;
    public AudioClip sonidoExplosion;
    string nombreJefe = "Jefe1(Clone)";

    void Start()
    {
        Jefe1 jefe1 = GameObject.Find(nombreJefe).GetComponent<Jefe1>();
        switch (jefe1.modoDisparo)
        {
            case 1:
                modo1();
                break;
            case 2:
                modo2();
                break;
        }
    }

    void Update()
    {
        destruir();
    }

    void OnCollisionEnter(Collision col) //al chocar algo..
    {//"pelota(Clone)"
        
        if (col.gameObject.name != "paredIzq" &&
                 col.gameObject.name != "paredDer" &&
                 col.gameObject.name != "balaJefe(Clone)")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            if (sonidoExplosion) AudioSource.PlayClipAtPoint(sonidoExplosion, transform.position, 2);
            if(col.gameObject.name == "Vaus")
            {
                Pelota pelota = GameObject.Find("pelota").GetComponent<Pelota>();
                Instantiate(GM.esteObjeto.explosionPelota, pelota.transform.position, Quaternion.identity);
                pelota.transform.position = new Vector3(0,-5,0);
                Svaus.esteObjeto.iniciarVaus();
            }
            DestroyObject(gameObject);
        }
    }

    void destruir()
    {
        if (transform.position.y < -2.8f)
        {
            Destroy(gameObject);
        }
    }

    void modo1()
    {
        float fuerza = 2f;
        Vector3 movAbajo = new Vector3(0, -fuerza, 0);
        Vector3 movAbajoIzq = new Vector3(-fuerza, -fuerza, 0);
        Vector3 movAbajoDer = new Vector3(fuerza, -fuerza, 0);
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        Transform posJefe = GameObject.Find(nombreJefe).GetComponent<Transform>();

        if (transform.position.x < posJefe.transform.position.x)
        {//si la posicion de la pelota es menor q la del jefe sale para la izquierda
            rb.AddForce(movAbajoIzq);
        }
        else if (transform.position.x > posJefe.transform.position.x)
        {
            rb.AddForce(movAbajoDer);
        }
        else if (transform.position.x == posJefe.transform.position.x)
        {
            rb.AddForce(movAbajo);
        }
    }

    void modo2()
    {
        float fuerza = 2.5f;
        Vector3 movAbajo = new Vector3(0, -fuerza, 0);
        Vector3 movAbajoIzq = new Vector3(-fuerza, -fuerza, 0);
        Vector3 movAbajoDer = new Vector3(fuerza, -fuerza, 0);
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        Transform posJefe = GameObject.Find(nombreJefe).GetComponent<Transform>();

        if (transform.position.x < posJefe.transform.position.x)
        {//si la posicion de las pelotas es menor q la del jefe salen para la izquierda
            rb.AddForce(movAbajoIzq);
        }
        else if (transform.position.x == posJefe.transform.position.x)
        {
            rb.AddForce(movAbajoIzq);
        }
        else if (transform.position.x > posJefe.transform.position.x)
        {//sino para la derecha
            rb.AddForce(movAbajoDer);
        }
    }

    void modo3()
    {
        float fuerza = 2.7f;
        Vector3 movAbajo = new Vector3(0, -fuerza, 0);
        Vector3 movAbajoIzq = new Vector3(-fuerza, -fuerza, 0);
        Vector3 movAbajoDer = new Vector3(fuerza, -fuerza, 0);
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        Transform posJefe = GameObject.Find(nombreJefe).GetComponent<Transform>();

        if (transform.position.x < posJefe.transform.position.x)
        {//si la posicion de las pelotas es menor q la del jefe salen para la izquierda
            rb.AddForce(movAbajoIzq);
        }
        else if (transform.position.x == posJefe.transform.position.x)
        {
            rb.AddForce(movAbajoIzq);
        }
        else if (transform.position.x > posJefe.transform.position.x)
        {//sino para la derecha
            rb.AddForce(movAbajoDer);
        }
    }
}

#region old
/*
    private Rigidbody rb;
    public GameObject explosion;
    public AudioClip sonidoExplosion;

    void Start()
    {
        jefe1 jefe1 = GameObject.Find("Jefe1").GetComponent<jefe1>();
        switch (jefe1.modoDisparo)
        {
            case 1:
                modo1();
                break;
            case 2:
                modo2();
                break;
        }
    }

    void Update()
    {
        destruir();
    }

    void OnCollisionEnter(Collision col) //al chocar algo..
    {        
        if (col.gameObject.name != "paredIzq" &&
                 col.gameObject.name != "paredDer" &&
                 col.gameObject.name != "balaJefe(Clone)")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            if (sonidoExplosion) AudioSource.PlayClipAtPoint(sonidoExplosion, transform.position, 2);
            DestroyObject(gameObject);
        }
    }

    void destruir()
    {
        if (transform.position.y < -2.8f)
        {
            Destroy(gameObject);
        }
    }

    // modo1 = el modo de disparo 1 del jefe
    void modo1()
    {
        float fuerza = 3f;
        Vector3 movAbajo = new Vector3(0, -fuerza, 0);
        Vector3 movAbajoIzq = new Vector3(-fuerza, -fuerza, 0);
        Vector3 movAbajoDer = new Vector3(fuerza, -fuerza, 0);
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        Transform posJefe = GameObject.Find("Jefe1").GetComponent<Transform>();

        if (transform.position.x < posJefe.transform.position.x)
        {//si la posicion de la pelota es menor q la del jefe sale para la izquierda
            rb.AddForce(movAbajoIzq);
        }
        else if (transform.position.x > posJefe.transform.position.x)
        {
            rb.AddForce(movAbajoDer);
        }
        else if (transform.position.x == posJefe.transform.position.x)
        {
            rb.AddForce(movAbajo);
        }
    }

    void modo2()
    {
        float fuerza = 3f;
        Vector3 movAbajo = new Vector3(0, -fuerza, 0);
        Vector3 movAbajoIzq = new Vector3(-fuerza, -fuerza, 0);
        Vector3 movAbajoDer = new Vector3(fuerza, -fuerza, 0);
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        Transform posJefe = GameObject.Find("Jefe1").GetComponent<Transform>();

        if (transform.position.x < posJefe.transform.position.x)
        {//si la posicion de las pelotas es menor q la del jefe salen para la izquierda
            rb.AddForce(movAbajoIzq);
        }
        else if (transform.position.x == posJefe.transform.position.x)
        {
            rb.AddForce(movAbajoIzq);
        }
        else if (transform.position.x > posJefe.transform.position.x)
        {//sino para la derecha
            rb.AddForce(movAbajoDer);
        }
    }
}
/*
 * public class balaJefe: MonoBehaviour {

    private Rigidbody rb;
    public GameObject explosion;
    public AudioClip sonidoExplosion;
    string nombreJefe = "Jefe1(Clone)";

    void Start()
    {
        jefe1 jefe1 = GameObject.Find(nombreJefe).GetComponent<jefe1>();
        switch (jefe1.modoDisparo)
        {
            case 1:
                modo1();
                break;
            case 2:
                modo2();
                break;
        }
    }

    void Update()
    {
        destruir();
    }

    void OnCollisionEnter(Collision col) //al chocar algo..
    {//"pelota(Clone)"
        
        if (col.gameObject.name != "paredIzq" &&
                 col.gameObject.name != "paredDer" &&
                 col.gameObject.name != "balaJefe(Clone)")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            if (sonidoExplosion) AudioSource.PlayClipAtPoint(sonidoExplosion, transform.position, 2);
            DestroyObject(gameObject);
        }
    }

    void destruir()
    {
        if (transform.position.y < -2.8f)
        {
            Destroy(gameObject);
        }
    }

    void modo1()
    {
        float fuerza = 3f;
        Vector3 movAbajo = new Vector3(0, -fuerza, 0);
        Vector3 movAbajoIzq = new Vector3(-fuerza, -fuerza, 0);
        Vector3 movAbajoDer = new Vector3(fuerza, -fuerza, 0);
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        Transform posJefe = GameObject.Find(nombreJefe).GetComponent<Transform>();

        if (transform.position.x < posJefe.transform.position.x)
        {//si la posicion de la pelota es menor q la del jefe sale para la izquierda
            rb.AddForce(movAbajoIzq);
        }
        else if (transform.position.x > posJefe.transform.position.x)
        {
            rb.AddForce(movAbajoDer);
        }
        else if (transform.position.x == posJefe.transform.position.x)
        {
            rb.AddForce(movAbajo);
        }
    }

    void modo2()
    {
        float fuerza = 3f;
        Vector3 movAbajo = new Vector3(0, -fuerza, 0);
        Vector3 movAbajoIzq = new Vector3(-fuerza, -fuerza, 0);
        Vector3 movAbajoDer = new Vector3(fuerza, -fuerza, 0);
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        Transform posJefe = GameObject.Find(nombreJefe).GetComponent<Transform>();

        if (transform.position.x < posJefe.transform.position.x)
        {//si la posicion de las pelotas es menor q la del jefe salen para la izquierda
            rb.AddForce(movAbajoIzq);
        }
        else if (transform.position.x == posJefe.transform.position.x)
        {
            rb.AddForce(movAbajoIzq);
        }
        else if (transform.position.x > posJefe.transform.position.x)
        {//sino para la derecha
            rb.AddForce(movAbajoDer);
        }
    }

    void modo3()
    {
        float fuerza = 3f;
        Vector3 movAbajo = new Vector3(0, -fuerza, 0);
        Vector3 movAbajoIzq = new Vector3(-fuerza, -fuerza, 0);
        Vector3 movAbajoDer = new Vector3(fuerza, -fuerza, 0);
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        Transform posJefe = GameObject.Find(nombreJefe).GetComponent<Transform>();

        if (transform.position.x < posJefe.transform.position.x)
        {//si la posicion de las pelotas es menor q la del jefe salen para la izquierda
            rb.AddForce(movAbajoIzq);
        }
        else if (transform.position.x == posJefe.transform.position.x)
        {
            rb.AddForce(movAbajoIzq);
        }
        else if (transform.position.x > posJefe.transform.position.x)
        {//sino para la derecha
            rb.AddForce(movAbajoDer);
        }
    }*/
#endregion
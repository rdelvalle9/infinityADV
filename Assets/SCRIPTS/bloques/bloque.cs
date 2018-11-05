using UnityEngine;
using System.Collections;

public class bloque : MonoBehaviour
{
    public AudioClip sonido;
    public float volumen = 1.0f;
    protected Transform posicion = null;
    public GameObject explosion;
    float tiempoMoverseInicial;
    float tiempoMoverseFinal;


    // Use this for initialization
    void Start()
    {
        posicion = transform;
    }

    void Update()
    {
        moverBloque();
        desactivarKinematic();
    }

    // tener cuidado de q los bloques no bajen demasiado
    public void moverBloque()
    {
        //if (Svaus.esteObjeto.bBloque)
        //{
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            if (Svaus.esteObjeto.bBloque && rb.isKinematic == true)
            {
                rb.isKinematic = false;
                rb.AddForce(0, -.05f, 0);
                tiempoMoverseInicial = GM.esteObjeto.segundos + 1f;
                tiempoMoverseFinal = GM.esteObjeto.segundos + 1f;
            }
            if (tiempoMoverseInicial < GM.esteObjeto.segundos || transform.position.y < 3)
            {
                rb.isKinematic = true;
            }
            if (GM.esteObjeto.segundos > tiempoMoverseFinal)
            {
                Svaus.esteObjeto.bBloque = false;
            }
        //}
    }

    public void desactivarKinematic()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if (Svaus.esteObjeto.bSuperBall && rb.isKinematic == true)
        {
            rb.isKinematic = false;
        }
        if (!Svaus.esteObjeto.bSuperBall && !Svaus.esteObjeto.bBloque)
        {
            rb.isKinematic = true;
        }
    }

    // Update is called once per frame
    public void OnCollisionEnter(Collision col)
    {
        GM scriptGM;
        if (col.gameObject.name != "bicho")
        {
            if (sonido) AudioSource.PlayClipAtPoint(sonido, posicion.position, volumen);
            scriptGM = GameObject.Find("GM").GetComponent <GM>();//busca el objeto del script y lo asigna a la variable
            scriptGM.puntaje++;
            scriptGM.puntajeTotal++;
            scriptGM.descontarBloque();
            //muestra la explosion
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
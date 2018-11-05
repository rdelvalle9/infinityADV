using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloqueGris : MonoBehaviour
{
    public AudioClip sonido;
    public float volumen = 1.0f;
    protected Transform posicion = null;
    public GameObject explosion;
	public int Colisiones=0;
    public float tiempoMoverseInicial;
    public float tiempoMoverseFinal;

    void Start()
    {
        posicion = transform;
    }

    void OnCollisionEnter(Collision col)
	{
		GM scriptGM;
		if (col.gameObject.name != "bicho") {
			if (sonido)AudioSource.PlayClipAtPoint (sonido, posicion.position, volumen);
			scriptGM = GameObject.Find ("GM").GetComponent<GM> ();//busca el objeto del script y lo asigna a la variable
			posYcolor posYcolorGris = GameObject.Find (gameObject.name).GetComponent<posYcolor> (); //LO BUSCO Y ASIGNO LOS VALORES
			//scriptGM.reemplazarPorBloqueRoto(gameObject.name, transform, posYcolorGris);
			GameObject bloqueRoto = scriptGM.bloqueRoto;
			//ASI SE CAMBIA ENTRE MESH
			gameObject.GetComponent<MeshFilter>().sharedMesh = bloqueRoto.GetComponent<MeshFilter>().sharedMesh;
			//muestra la explosionas
			Instantiate (explosion, transform.position, transform.rotation);
			//Destroy(gameObject);
			Colisiones++;
		}
		if (Colisiones == 2) {
			// al contar la segunda colision, destruye el bloque gris
			scriptGM = GameObject.Find ("GM").GetComponent<GM> ();//busca el objeto del script y lo asigna a la variable
			scriptGM.puntaje++;
			scriptGM.puntajeTotal++;
			scriptGM.descontarBloque();
			Destroy (gameObject);
		}
	}

    void Update()
    {
        moverBloque();
    }

    public void moverBloque()
    {
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
    }

}
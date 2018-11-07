using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonido : MonoBehaviour {

    public static Sonido esteObjeto = null;
    AudioClip btnAceptar = null;
    AudioClip btnCancelar = null;
    AudioClip btnDenegar = null;

    AudioClip expVaus = null;
    AudioClip menuPrincipal = null;

    AudioClip choqueAbloqueNegro = null;

    public float volumen = 1.0f;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        asignarAudios();
    }

    void Start (){
        if (esteObjeto == null) { esteObjeto = this; } else if (esteObjeto != this) { Destroy(gameObject); }

        int m = PlayerPrefs.GetInt("musica");
        AudioSource parlante = GameObject.Find("parlante").GetComponent<AudioSource>();
        if (m == 0)
        {
            parlante.enabled = false;
        }
    }

    void Update (){
        
    }

    void asignarAudios()
    {
        btnAceptar = (AudioClip)Resources.Load("sonido/botones/btnAceptar");
        btnCancelar = (AudioClip)Resources.Load("sonido/botones/btnCancelar");

        btnDenegar = (AudioClip)Resources.Load("sonido/botones/btnDenegar");
        menuPrincipal = (AudioClip)Resources.Load("musica/menuPrincipal");

        choqueAbloqueNegro = (AudioClip)Resources.Load("sonido/bloques/bloqueNegro");

        //expVaus = (AudioClip)Resources.Load("sonido//bloqueNegro");
    }

    public void sonidoBtnAceptar()
    {
        if (btnAceptar) AudioSource.PlayClipAtPoint(btnAceptar, transform.position, volumen);
    }

    public void sonidoBtnCancelar()
    {
        if (btnCancelar) AudioSource.PlayClipAtPoint(btnCancelar, transform.position, volumen);
    }

    public void sonidoBtnDenegado()
    {
        if (btnDenegar) AudioSource.PlayClipAtPoint(btnDenegar, transform.position, volumen);
    }

    public void songMenuPrincipal()
    {
        if (menuPrincipal) AudioSource.PlayClipAtPoint(menuPrincipal, transform.position, volumen);
    }

    public void sonidoChoqueAbloqueNegro()
    {
        if (choqueAbloqueNegro) AudioSource.PlayClipAtPoint(choqueAbloqueNegro, transform.position, volumen);
    }

    public void vausSonidoDeSuExplosion()
    {
        if (expVaus) AudioSource.PlayClipAtPoint(expVaus, transform.position, volumen);
    }
}

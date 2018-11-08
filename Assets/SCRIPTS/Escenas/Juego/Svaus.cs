using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Svaus : MonoBehaviour
{
    #region ~atributos~
    public static float xInic = -8f;
    public static float yInic = 0f;
    public static float zInic = 0f;
    public Vector3 vPosIni = new Vector3(xInic, yInic, zInic);
    public bool bActivarLaser = false;
    public bool disparar = false;
    public bool bExpandir = false;
    public bool bComprimir = false;
    public bool bIman = false;
    public bool bIAPrincipal = false;
    public bool bIA = false;
    public bool bSlider = false;
    public bool bAnimPelotas = false;
    public bool bColoresPelota = false;
    public bool bLanzarPelotas = false;
    public bool bBloque = false;
    public bool bSuperBall = false;
    public bool bActivarAmetralladora = false;
    public bool bAmetralladora = false;
    public GameObject bala;
    public AudioClip sDispararLaser;
    public AudioClip sonidoDispPel;
    public AudioClip sonidoExp;
    public float volumen = 1.0f;
    public float tiempo;
    public float tiempoGM;
    public float tiempoBSlider = 0;
    public float tiempoAmetralladora = 0;
    Animator animator;
    private GM scriptGM;
    //public bool bDestruir = false;
    public AudioClip sDispararPelotas;
    public bool llegoAlaPos = false;
    private Rigidbody rb;
    float x = 0f;

    public float recargando; //VARIABLE PARA Q NO DISPARE MUCHAS VECES A LA VEZ
    float fy; //
    float fx; //
    public float fuerzaY; //VARIABLES PARA DAR EL ANGULO Y VELOCIDAD A LA PELOTA
    public float fuerzaX; //
    public GameObject humoDispLaser; //efecto
    public static Svaus esteObjeto = null;

    bool bIniciarVaus = true;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (esteObjeto == null){esteObjeto = this;}else if (esteObjeto != this){Destroy(gameObject);}
        transform.position = vPosIni;
        x = transform.position.x;
        animator = GetComponent<Animator>();
        asignarVelPelota();
        iniciarVaus();
        GetComponent<BoxCollider>().isTrigger = true;
    }

    void Update()
    {
        tiempoGM = GM.esteObjeto.segundos;
        moverHasta();
        scriptGM = GameObject.Find("GM").GetComponent<GM>();//busca el objeto del script y lo asigna a la variable
        funcionUpgrades();
        //float xPos = transform.position.x + (Input.GetAxis("Horizontal") * velocidadVaus * Time.deltaTime);
        //playerPos = new Vector3(Mathf.Clamp(xPos, margenIzq, margenDer), yInic, zInic);
        cargarAnimator();
        //}
    }

    #region funciones de upgrades
    /// <summary>
    /// es cuando la vaus esta en laser y dispara las bolas de energia
    /// </summary>
    void laser()
    {
        if (disparar == true)
        {
            if (scriptGM.segundos - recargando > .1)
            {
                recargando = scriptGM.segundos;
                Vector3 posBala = transform.position;
                posBala.x += 0.65f;
                posBala.y += 0.35f;
                Instantiate(bala, posBala, Quaternion.identity);
                posBala.x += -0.65f;
                posBala.x += -0.65f;
                Instantiate(bala, posBala, Quaternion.identity);
                if (sDispararLaser) AudioSource.PlayClipAtPoint(sDispararLaser, transform.position, volumen);

                //TODO arreglar el humo
                //HUMO
                //Transform posBocaL = GameObject.Find("bocaL").GetComponent<Transform>();
                //Transform posBocaR = GameObject.Find("bocaR").GetComponent<Transform>();
                //Vector3 posHumoL = posBocaL.transform.position;
                //Vector3 posHumoR = posBocaR.transform.position;
                //Instantiate(humoDispLaser, posHumoL, Quaternion.identity);
                //Instantiate(humoDispLaser, posHumoR, Quaternion.identity);
            }
        }
    }

    void ametralladora()
    {
        if (bAmetralladora == true)
        {
            if (tiempoAmetralladora > scriptGM.segundos)
            {
                if (scriptGM.segundos > recargando)
                {
                    recargando = scriptGM.segundos + .1f;
                    Vector3 posBala = transform.position;
                    posBala.x += 0.65f;
                    posBala.y += 0.35f;
                    Instantiate(bala, posBala, Quaternion.identity);
                    posBala.x += -0.65f;
                    posBala.x += -0.65f;
                    Instantiate(bala, posBala, Quaternion.identity);
                    if (sDispararLaser) AudioSource.PlayClipAtPoint(sDispararLaser, transform.position, volumen);
                }
            }
            else
            {
                desactivarUpgrades();
            }
        }
    }

    /// <summary>
    /// es el upgrade de lanzar 2 pelotas
    /// </summary>
    public void lanzarPelotas()
    {
        if (scriptGM.segundos - recargando > .5 && bLanzarPelotas == true)
        {
            recargando = scriptGM.segundos;
            Svaus vaus = GameObject.Find("Vaus").GetComponent<Svaus>();//busca el objeto del script y lo asigna a la variable
            vaus.bLanzarPelotas = false;
            vaus.bAnimPelotas = false;

            GameObject pelota2 = scriptGM.pelotaParaLanzar1;
            GameObject pelota3 = scriptGM.pelotaParaLanzar2;

            Vector3 posPelota2 = new Vector3(vaus.transform.position.x + 0.5f, vaus.transform.position.y + 0.4f, 0);
            Vector3 posPelota3 = new Vector3(vaus.transform.position.x - 0.5f, vaus.transform.position.y + 0.4f, 0);
            pelota2.transform.position = posPelota2;//las posiciones de las pelotas q van a dispararse
            pelota3.transform.position = posPelota3;

            scriptGM.cntPelotas += 2; //aumenta la variable de pelotas en 3
            Instantiate(pelota2, pelota2.transform.position, Quaternion.identity);//crea una pelota mas
            pelota2.name = "pelota2";
            Instantiate(pelota3, pelota3.transform.position, Quaternion.identity);//crea una pelota mas
            pelota3.name = "pelota3";
            Pelota scriptPelota2 = GameObject.Find("pelota2(Clone)").GetComponent<Pelota>();     //le pongo el nombre de "pelota" a todas las pelotas
            Rigidbody rb2 = GameObject.Find("pelota2(Clone)").GetComponent<Rigidbody>();     //le pongo el nombre de "pelota" a todas las pelotas
            scriptPelota2.name = "pelota";
            Pelota scriptPelota3 = GameObject.Find("pelota3(Clone)").GetComponent<Pelota>();
            Rigidbody rb3 = GameObject.Find("pelota3(Clone)").GetComponent<Rigidbody>();     //le pongo el nombre de "pelota" a todas las pelotas
            scriptPelota3.name = "pelota";
            if (sonidoDispPel) AudioSource.PlayClipAtPoint(sonidoDispPel, transform.position, 2);
        }
    }
    #endregion

    void moverHasta()
    {
        if (!llegoAlaPos)
        {
            x += 1.2f;
            rb = GetComponent<Rigidbody>();
            rb.AddForce(x, 0, 0);
            if (transform.position.x > 0)
            {
                rb.isKinematic = true;
                llegoAlaPos = true;
                GetComponent<BoxCollider>().isTrigger = false;
                Slider slider = GameObject.Find("Slider").GetComponent<Slider>();
                slider.interactable = true;
                gateDownL.esteObjeto.bAbrirPuertaAbaL = false;
                }
        }//TODO ver esto
        //else//activa el boton para poder hacer pausa
        //{
        //    GM.esteObjeto.pausa.GetComponent<Button>().enabled = true;
        //}
    }

    /// <summary>
    /// carga las variables para las animaciones
    /// </summary>
    void cargarAnimator()
    {
        animator.SetBool("upExpand", bExpandir);
        animator.SetBool("bComprimir", bComprimir);
        animator.SetBool("bAnimLanzarPelotas", bAnimPelotas);
        animator.SetBool("bActivarLaser", bActivarLaser);
        animator.SetBool("bIman", bIman);
        animator.SetBool("bIA", bIAPrincipal);
        animator.SetBool("bActivarAmetralladora", bActivarAmetralladora);
    }

    void funcionUpgrades()
    {
        laser();
        ametralladora();
        lanzarPelotas();
        seguirPelota();
    }

    /// <summary>
    /// pone a la vaus en el estado normal por si se agarro otro upgrade
    /// o si se cayeron todas la pelotas
    /// </summary>
    public void desactivarUpgrades()
    {
        bActivarLaser = false;
        bExpandir = false;
        bComprimir = false;
        bIman = false;
        bIA = false;
        bIAPrincipal = false;
        bSuperBall = false;
        bAmetralladora = false; 
        bActivarAmetralladora = false;
        tiempo = 0;
        camaraJuego scriptCamaraJuego = GameObject.Find("Main Camera").GetComponent<camaraJuego>();
        scriptCamaraJuego.bSeguirVausLaser = false;
        Pelota pelota = GameObject.Find("pelota").GetComponent<Pelota>();
        pelota.color = false;
        recargando = 0;
        sliderDefault();
    }

    public void reiniciarFuerzaDeRebote()
    {
        fuerzaX = fx;
        fuerzaY = fy;
    }

    /// <summary>
    /// vuelve a poner los valores del slider q es hasta donde se mueve la vaus en el eje X
    /// solo cambia cuando la vaus se alarga y se tiene q mover menos para q no traspase las paredes
    /// </summary>
    void sliderDefault()
    {
        Slider slider = Scanvas.esteObjeto.slider.GetComponent<Slider>();
        slider.minValue = -3.47f;
        slider.maxValue = 3.47f;
    }

    /// <summary>
    /// es para q la vaus siga a la pelota cuando tiene el upgrade de IA
    /// </summary>
    void seguirPelota()
    {
        if (bIAPrincipal)
        {
            if (bIA)
            {
                float xPelota = GameObject.Find("pelota").GetComponent<Transform>().position.x;
                if (GM.esteObjeto.segundos < tiempo)
                {
                    if (xPelota <= -3.47f)
                    {
                        transform.position = new Vector3(-3.47f, 0);
                    }
                    else if (xPelota >= 3.47f)
                    {
                        transform.position = new Vector3(3.47f, 0);
                    }
                    else
                    {
                        Slider slider = GameObject.Find("Slider").GetComponent<Slider>();
                        slider.onValueChanged.AddListener(
                            delegate {
                                bIA = false;
                                bSlider = true;
                                tiempoBSlider = GM.esteObjeto.segundos + .2f;
                               //GameObject.Find("IALuz").GetComponent<Renderer>().material = crearNiveles.esteObjeto.blanco;
                            });
                        transform.position = new Vector3(xPelota, 0);
                    }
                }
                else
                {
                    bIAPrincipal = false;
                }
            }

            if (GM.esteObjeto.segundos > tiempoBSlider && bSlider)
            {
                bIA = true;
                bSlider = false;
                //GameObject.Find("IALuz").GetComponent<Renderer>().material = crearNiveles.esteObjeto.rojo
            }
        }
    }

    /// <summary>
    /// carga la velocidad de base q va a tener la pelota dependiendo de la dificultad de juego elegida
    /// </summary>
    void asignarVelPelota()
    {
        Svaus vaus = GameObject.Find("Vaus").GetComponent<Svaus>();
        int dificultad = 1;
        try
        {
            dificultad = PlayerPrefs.GetInt("dificultadJuego");
        }
        catch
        {
            PlayerPrefs.SetInt("dificultadJuego", 1);
        }
        switch (dificultad)
        {
            case 1:
                vaus.fx = 140;
                vaus.fy = 140;
                break;
            case 2:
                vaus.fx = 170;
                vaus.fy = 170;
                break;
            case 3:
                vaus.fx = 200;
                vaus.fy = 200;
                break;
        }
        reiniciarFuerzaDeRebote();
    }

    /// <summary>
    /// es la animacion de cuando entra la vaus por la pared
    /// </summary>
    public void iniciarVaus()
    {
        //TODO hacer clase efectos visuales y otra sonoros estatico
        gameObject.transform.position = vPosIni;
        bIniciarVaus = false;
        gateDownL.esteObjeto.bAbrirPuertaAbaL = true;
        Pelota scriptPelota = GameObject.Find("pelota").GetComponent<Pelota>();
        transform.position = vPosIni;
        x = transform.position.x;
        Slider slider = GameObject.Find("Slider").GetComponent<Slider>();
        slider.interactable = false;
        rb.isKinematic = false;
        llegoAlaPos = false;
        GetComponent<BoxCollider>().isTrigger = true;
    }

    public void reproducirSonidoExplosion()
    {
        Sonido.esteObjeto.vausSonidoDeSuExplosion();
    }
}


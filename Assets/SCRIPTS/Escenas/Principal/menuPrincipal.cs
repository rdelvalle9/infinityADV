using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using GoogleMobileAds.Api;

public class menuPrincipal : MonoBehaviour
{

    public GameObject vaus;
    public bool bNuevoJuego;
    public bool bContinuarJuego;
    public bool animNuevoJuego;
    public bool animContinuar;
    Animator animator;
    public GameObject imagencarga;
    public Slider barra;
    AsyncOperation asyn;
    private object[] bloquee;
    public AudioClip btnAceptar = null;
    public AudioClip btnCancelar = null;
    public float volumen = 1.0f;
    public Sprite fondo;
    int version;
    public GameObject objPopUpRateUs = null;
    public GameObject canvasPopUpRateUs = null;
    private BannerView bannerView;
    //clase para el uso de las posiciones y colores de los bloques guardados de disco
    [Serializable]
    public class Bloque
    {
        byte Pos;
        byte Color;
        public byte pos
        {
            get
            {
                return Pos;
            }
            set
            {
                Pos = value;
            }
        }
        public byte color
        {
            get
            {
                return Color;
            }
            set
            {
                Color = value;
            }
        }
    }

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
    }

    void Start()
    {
        PlayerPrefs.SetInt("nivelAlcanzado", 105); //TODO sacar esto en produccion
        comprobacionInicial();
        PlayerPrefs.SetInt("mostrarPublicidad", 0);
        //PlayerPrefs.SetInt("popUpRate", 1);//para q muestre la ventana de comentar aplicacion
        //AdManager.Instance.mostrarBanner(); //muestra la publicidad de arriba
        crearCarpetaGuardado();
        animator = GetComponent<Animator>();
        //animacion de jugar
        //animator.SetBool("jugar", animNuevoJuego);
        //animator.SetBool("continuar", animContinuar);
        mostrarTextos();
        //cargarVelPelota();PlayerPrefs.HasKey("isNewPlayer")
    }

    void Update()
    {
        //animator.SetBool("jugar", animNuevoJuego);
        //animator.SetBool("continuar", animContinuar);
        if (bNuevoJuego == true)
        {
            bNuevoJuego = false;
            juegoNuevo();
        }
        if (bContinuarJuego == true)
        {
            bContinuarJuego = false;
            continuarJuego();
        }
    }
    
    // mostrarTextos = muestra los textos de new game o continue correctamente de los botones
    void mostrarTextos()
    {
        if (PlayerPrefs.GetInt("continuarNivel") == 0)
        {
            GameObject.Find("continuar").GetComponent<Button>().enabled = false;
            GameObject.Find("continuar").GetComponentInChildren<Text>().enabled = false;
        }
        else
        {
            GameObject.Find("continuar").GetComponent<Button>().enabled = true;
            GameObject.Find("continuar").GetComponentInChildren<Text>().enabled = true;
        }
        if (PlayerPrefs.GetInt("popUpRate") == 0)//oculta el texto de nuevo juego si esta el pop up de rate us
        {
            objPopUpRateUs.SetActive(false); 
            canvasPopUpRateUs.SetActive(false);
        }
        else
        {
            objPopUpRateUs.SetActive(true);
            canvasPopUpRateUs.SetActive(true);
            GameObject.Find("nuevoJuego").GetComponent<Button>().enabled = false;
            GameObject.Find("nuevoJuego").GetComponentInChildren<Text>().enabled = false;
            GameObject.Find("continuar").GetComponent<Button>().enabled = false;
            GameObject.Find("continuar").GetComponentInChildren<Text>().enabled = false;
        }
    }

    public void pulsarJugar()
    {
        RectTransform btnContinuar = GameObject.Find("continuar").GetComponent<RectTransform>();
        RectTransform btnNuevoJuego = GameObject.Find("nuevoJuego").GetComponent<RectTransform>();
        RectTransform btnOpciones = GameObject.Find("opciones").GetComponent<RectTransform>();
        RectTransform btnJugar = GameObject.Find("jugar").GetComponent<RectTransform>();
        RectTransform btnAtras = GameObject.Find("jugar").GetComponent<RectTransform>();
        btnContinuar.transform.position = new Vector3(2000, 2000, 0);
        //btnContinuar.transform.position = new Vector3(2000, 2000, 0);
        btnNuevoJuego.transform.position = new Vector3(540, 653, 0);
        //btnOpciones.transform.position = new Vector3(2000, 2000, 0);
    }

    public void pulsarAtras()
    {
        RectTransform btnContinuar = GameObject.Find("continuar").GetComponent<RectTransform>();
        RectTransform btnNuevoJuego = GameObject.Find("nuevoJuego").GetComponent<RectTransform>();
        RectTransform btnJugar = GameObject.Find("jugar").GetComponent<RectTransform>();
        btnContinuar.transform.position = new Vector3(540, 653, 0);
        btnNuevoJuego.transform.position = new Vector3(2000, 2000, 0);
    }

    public void cerrarJuego()
    {
        Application.Quit();
    }

    public void irAopciones()
    {
        SceneManager.LoadScene("opciones");
    }

    public void volverMenuPrincipal()
    {
        //Debug.Log("menu");
        SceneManager.LoadScene("menuPrincipal");

    }

    public void verNiveles()
    {
        SceneManager.LoadScene("verNiveles");
    }

    public void irAsuarios()
    {
        SceneManager.LoadScene("usuarios");
    }

    public void irCrearNiveles()
    {
        SceneManager.LoadScene("crearNivel");
    }

    // aNuevoJuego = animacion de abrir la pared y meter la vaus
    public void aNuevoJuego()
    {
        pared scriptWall = GameObject.Find("wall").GetComponent<pared>();
        scriptWall.bJugar = true;
        animNuevoJuego = true;
    }
    // aContinuar = animacion de abrir la pared y meter la vaus
    public void aContinuar()
    {
        pared scriptWall = GameObject.Find("wall").GetComponent<pared>();
        scriptWall.bJugar = true;
        animContinuar = true;
    }

    // juegoNuevo = carga el juego nuevo
    public void juegoNuevo()
    {
        clickCarga("galaxias");
    }

    public void continuarJuego()
    {
        PlayerPrefs.SetInt("nivel", 0);
        clickCarga("galaxia1");
    }

    // clickCarga = carga la escena pero antes muestra la pantalla de carga
    public void clickCarga(string escena)
    {
        imagencarga.SetActive(true);
        StartCoroutine(Loadlevelslider(escena));
    }

    // Loadlevelslider = funcion de clickCarga
    IEnumerator Loadlevelslider(string escena)
    {
        asyn = SceneManager.LoadSceneAsync(escena);
        while (!asyn.isDone)
        {
            barra.value = asyn.progress;
            yield return null;
        }
    }
    
    /* crearCarpetaGuardado = CREO LA CARPETA DONDE VAN LAS PARTIDAS GUARDADAS(solo 
     lo hace la primera vez) */
    void crearCarpetaGuardado()
    {
        if (Directory.Exists(Application.persistentDataPath + "/saves") == false)
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
            PlayerPrefs.SetInt("musica", 1);
            PlayerPrefs.SetInt("dificultadJuego", 1);
        }
    }

    //25/4/18 SACAR DESPUES DE UN TIEMPO
    /*cargarVelPelota = carga la velocidad de base q va a tener la pelota dependiendo de la 
     * dificultad de juego elegida*/
    void cargarVelPelota()
    {
        int dificultad = 1;
        TextMesh txt = GameObject.Find("txt").GetComponent<TextMesh>();
        try
        {
            PlayerPrefs.SetInt("dificultadJuego", 1);
            dificultad = PlayerPrefs.GetInt("dificultadJuego");
            txt.text = "1";
        }
        catch
        {
            PlayerPrefs.SetInt("dificultadJuego", 1);
            txt.text = "3";
        }
    }

	void comprobacionInicial(){
        PlayerPrefs.SetInt("mostrarPublicidad", 0);
        //AdManager.Instance.mostrarBanner(); //muestra la publicidad de arriba

        //// Create an empty ad request.
        //AdRequest request = new AdRequest.Builder().Build();
        //// Load the banner with the request.
        //bannerView.LoadAd(request);

        crearCarpetaGuardado();
        animator = GetComponent<Animator>();
        mostrarTextos();
        if (!PlayerPrefs.HasKey("isNewPlayer"))
        {
            PlayerPrefs.SetInt("isNewPlayer", 1);
            PlayerPrefs.SetInt("dificultadJuego", 1);//pone la dificultad facil
            PlayerPrefs.SetInt("musica", 1);//activa la reproduccion de la musica
            PlayerPrefs.SetInt("nivel", 1);
            PlayerPrefs.SetInt("nivelAlcanzado", 1);
            PlayerPrefs.SetInt("popUpRate", 0);//para q muestre la ventana de comentar aplicacion
            PlayerPrefs.SetInt("continuarNivel", 0);
        }
        if (!PlayerPrefs.HasKey("nivelByGalaxia"))
        {
            PlayerPrefs.SetInt("nivelByGalaxia", 1);
            PlayerPrefs.SetInt("fondoPantalla", 1);
        }
        if (!PlayerPrefs.HasKey("enemigosInGame"))
        {
            PlayerPrefs.SetInt("enemigosInGame", 1);
        }
        if (!PlayerPrefs.HasKey("mostrarPublicidad"))
        {
            PlayerPrefs.SetInt("mostrarPublicidad", 0);
        }
    }

    public void repSonAceptar()
    {
        if (btnAceptar) AudioSource.PlayClipAtPoint(btnAceptar, gameObject. transform.position, volumen);
    }

    public void repSonCancelar()
    {
        if (btnCancelar) AudioSource.PlayClipAtPoint(btnCancelar, transform.position, volumen);
    }

    public void jugarJefe()
    {
        //SceneManager.LoadScene("jefe1");
        PlayerPrefs.SetInt("esNivelJefe", 1);
        SceneManager.LoadScene("galaxia1");
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine.EventSystems;
//script q maneja la mayor parte del juego
public class GM : MonoBehaviour
{
    public static GM esteObjeto = null;
    public int vidas;
    public int nivel;
    public int bloques;
    public GameObject BLOQUE;
    public int galaxia;
    public float resetDelay = 1f;
    public Text textoDeVidas;
    public GameObject bloquesPrefab;
    public GameObject pelota;
    public GameObject pelotaParaLanzar1;
    public GameObject pelotaParaLanzar2;
    public GameObject deathParticules;
    public GameObject bichoCono;
    public GameObject vida1;
    public GameObject bala1;
    public GameObject capPelotas;
    public GameObject bloqueRoto;
    public int cntPelotas = 1;
    private GameObject vaus;
    public Time tiempo;
    public float segundos = 0;
    public int cantBLoques;
    public int puntaje = 0;
    public int puntajeTotal = 0;
    public int contadorVidasPerdidas = 0;
    public int vidasTotalesPerdidas = 0;
    public bool capsulaEnCaida = false;
    public GameObject explosion;
    public GameObject explosionPelota;
    public GameObject puc;
    public GameObject pu;
    public GameObject puIconoVaus;
    public GameObject btnOtraVida;
    public GameObject pausa;
    public bool ganaste = false;

    private void Awake()
    {

    }

    void Start()
    {
        this.mostrarPublicidad(); //publicidad
        //cargarValores(); //carga los valores de x ej si se va a escuchar la musica
        this.nivel = PlayerPrefs.GetInt("nivel");   //traigo de disco el nivel con el que va a empezar el juego
        int nivelContinuar = PlayerPrefs.GetInt("continuarNivel");
        TextMesh txtNivel = GameObject.Find("txtNivel").GetComponent<TextMesh>();
        txtNivel.text = "level=" + nivel.ToString();  //solo es para mostrar el nivel
        this.vidas = PlayerPrefs.GetInt("vidas");    //asigno la cantidad de vidas
        this.bloques = 40;   //pongo un valor falso para que no de error el juego
        this.cargarVidas();
        Time.timeScale = 1f;
        this.instanciador(); //crea objetos
        if (nivelContinuar != 0 && nivel == 0)//si el nivel es distinto de cero significa que el usuario puso GUARDAR
        {
            crearNiveles scriptCrearNiveles = GameObject.Find("crear").GetComponent<crearNiveles>();
            vidas = PlayerPrefs.GetInt("vidasGuardadas");
            scriptCrearNiveles.continuarJuego();
            nivel = nivelContinuar;
            txtNivel.text = "level=" + nivel.ToString();
            PlayerPrefs.SetInt("continuarNivel", 0);
        }
        else if (PlayerPrefs.GetInt("esNivelJefe") == 0)
        {
            this.cargarProximoNivel();
        }
        PlayerPrefs.SetInt("esNivelJefe", 0);//asi ya no va a volver a ser nivel jefe

    }
    /// <summary>
    /// instanciador = instancia la vaus y la pelota
    /// </summary>
    public void instanciador()
    {
        if (esteObjeto == null) { esteObjeto = this; } else if (esteObjeto != this) { Destroy(gameObject); }
        GameObject Vaus = Resources.Load("vausCmp") as GameObject;
        Vector3 posVausInicial = new Vector3(-8, 0, 0);
        vaus = Instantiate(Vaus, posVausInicial, Quaternion.identity) as GameObject;//instancia la vaus en 0,0,0
        vaus.name = "Vaus";
        pelota = Instantiate(pelota, transform.position, Quaternion.identity) as GameObject;//instancia la pelota
        pelota.name = "pelota";
        if (PlayerPrefs.GetInt("esNivelJefe") != 0)
        {
            GameObject jefe = Resources.Load("jefe1") as GameObject;
            Vector3 posJefe = new Vector3(0, 9, 0);
            Instantiate(jefe, posJefe, Quaternion.identity); //crea al jefe en la posicion
            TextMesh txtNivel = GameObject.Find("txtNivel").GetComponent<TextMesh>();
            txtNivel.text = "BOSS=" + 1; //muestra el numero de boss
            bloques = -1; //para q pueda entrar en a funcion "juegoJefe"
        }
    }

    void Update()
    {
        mostrarVidas();
        checkGameOver();
        segundos += Time.deltaTime;
    }

    /* checkGameOver = verica si se acabo el juego por terminacion de bloques
       o de vidas */
    void checkGameOver()
    {
        juegoNormalBloques();
        //juegoJefe();
    }

    void juegoNormalBloques()
    {
        if (bloques == 0)
        {
            mostrarPopUp(true);
        }
        else if (vidas < 0)
        {
            mostrarPopUp(false);
        }
    }

    // juegoJefe = si bloques es -1 quiere decir q es nivel jefe y busca la vida del mismo
    //void juegoJefe()
    //{

    //    if (bloques == -1)
    //    {
    //        VidaJefe vidaJefe = GameObject.Find("vidaJefe").GetComponent<VidaJefe>();
    //        if (vidaJefe.vidaJefe == 0)
    //        {
    //            //SceneManager.LoadScene("galaxias");
    //        }
    //    }
    //}

    void cargarVidas()
    {
        if (PlayerPrefs.HasKey("vidas"))///////////////////////
        {
            vidas = PlayerPrefs.GetInt("vidas");
        }
    }

    /* mostrarPopUp = muestra el pop up    */
    public void mostrarPopUp(bool p)
    {
        //reiniciar la vista de la camara
        PlayerPrefs.SetInt("mostrarPublicidad", 1);//para que en el proximo nivel si muestre la publicidad
        Time.timeScale = 0;    //ralentiza el tiempo
        Scanvas.esteObjeto.slider.SetActive(false);
        GameObject.Find("pausa").GetComponent<Button>().enabled = false;
        //Svaus.esteObjeto.desactivarUpgrades();
        puc.SetActive(true);
        pu.SetActive(true);
        if (p)
        {
            GameObject.Find("txtPerdiste").GetComponent<Text>().text = "";
            GameObject.Find("btnPUReiniciar").GetComponent<Image>().enabled = false;
        }
        else//si perdio desactivo el boton de siguiente, el texto de ganaste
        {
            GameObject.Find("btnPUSiguiente").GetComponent<Image>().enabled = false;
            GameObject.Find("txtGanaste").GetComponent<Text>().text = "";

            //activo el icono de la vaus
            puIconoVaus.SetActive(true);
            //activo el boton de regalar una vida si ve el video
            btnOtraVida.SetActive(true);
        }
    }

    public void descontarBloque()
    {
        bloques--;
    }

    public void mostrarVidas()
    {
        vida1.SetActive(true);
        TextMesh txtVidas = GameObject.Find("txtVidas").GetComponent<TextMesh>();
        if (vidas >= 0)
        {
            txtVidas.text = "x" + vidas.ToString();
        }
    }

    public void cargarProximoNivel()
    {
        crearNiveles scriptCrearNiveles = GameObject.Find("crear").GetComponent<crearNiveles>();
        scriptCrearNiveles.cargar();
    }

    //public void cargarNivelJefe(int n)
    //{
    //    int jefe = (n % 20) + 4;
    //    PlayerPrefs.SetInt("esNivelJefe", 1);
    //    if (esNivelJefe == 0)
    //    {
    //        SceneManager.LoadScene(jefe);
    //    }
    //}
    // cargarValores = carga los valores de la musica por ejemplo si se reproduce o no
    public void cargarValores()
    {
        //AudioSource sonido = Sonido.esteObjeto.GetComponent<AudioSource>();
        //int musica;
        //musica = PlayerPrefs.GetInt("musica");
        //if (musica == 1)
        //{
        //    sonido.enabled = true;  //reproduce la musica
        //}
        //else
        //{
        //    sonido.enabled = false;
        //}
        //pausa = GameObject.Find("pausa");
        //pausa.GetComponent<Button>().enabled = false;//para q no pueda apretar pausa xq la vaus no se mueve se queda trabada
    }

    public void mostrarPublicidad()
    {
        AdManager.esteObjeto.mostrarInstersticial();
    }

}

/*
// reemplazarPorBloqueRoto = reemplaza al bloque gris por uno roto
public void reemplazarPorBloqueRoto(string nombreBloque, Transform posicion, posYcolor posYcolorGris)
{
	Instantiate(bloqueRoto, posicion.position, Quaternion.identity);       //CREO EL BLOQUE ROTO
	posYcolor posYcolorGrisRoto = GameObject.Find(nombreBloque).GetComponent<posYcolor>(); //LO BUSCO Y ASIGNO LOS VALORES
	posYcolorGrisRoto.pos = posYcolorGris.pos;
	posYcolorGrisRoto.color = posYcolorGris.color;
}*/

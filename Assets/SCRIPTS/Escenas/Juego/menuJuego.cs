//using admob;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class menuJuego : MonoBehaviour
{
    bool fpausa = false;
    private RewardBasedVideoAd rewardBasedVideoAd;

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
        rewardBasedVideoAd = RewardBasedVideoAd.Instance;

        rewardBasedVideoAd.OnAdRewarded += HandleOnAdRewarded;
    }
    public void PonerPausa()
    {
        if (fpausa == false)
        {
            PlayerPrefs.SetInt("mostrarPublicidad", 1);
            Time.timeScale = 0;
            fpausa = true;
            Scanvas.esteObjeto.guardar.SetActive(true);
            if (GM.esteObjeto.bloques == -1)
            {
                Scanvas.esteObjeto.guardar.SetActive(false);
            }
        }
        else
        {
            Time.timeScale = 1;
            fpausa = false;
        }
    }

    public void irAmenuPrincipal()
    {
        GM.esteObjeto.mostrarPublicidad();
        Time.timeScale = 1;
        SceneManager.LoadScene("menuPrincipal");
    }

    // guardarJuego = guarda la partida en el disco para poder continuarla despues
    public void guardarJuego()
    {
        Pelota pelota = GameObject.Find("pelota").GetComponent<Pelota>();//busca el objeto 
        if (pelota.transform.position.y >= 0)
        {
            GM.esteObjeto.mostrarPublicidad();
            //guardar un string con los bloques q se rompieron y al poner "guardar"
            //guardar ese sting en el disco y modificar las funciones correspondientes

            /*otra opcion ir guardando en un string cuando se rompe un bloque con su color y pos
             * y al apretar guardar se guarda ese string en disco luego al cargar se carga el nivel
             * completo pero se lee el archivo guardado y se destruyen los bloques q indiquen(hacerlo antes
             * de asignar las capsulas)*/

            GM scriptGM = GameObject.Find("GM").GetComponent<GM>();
            PlayerPrefs.SetInt("continuarNivel", scriptGM.nivel);
            PlayerPrefs.SetInt("vidasGuardadas", scriptGM.vidas);
            //File.Delete(Application.persistentDataPath + "/saves/slot1.bin");
            crearNiveles scriptCrearNiveles = GameObject.Find("crear").GetComponent<crearNiveles>();
            Time.timeScale = 1;
            fpausa = false;
            if (scriptCrearNiveles.guardarJuego())
            {
                SceneManager.LoadScene("menuPrincipal");
            }
        }
        else
        {
            Time.timeScale = 1;
            fpausa = false;
            Scanvas.esteObjeto.slider.gameObject.GetComponent<Slider>().enabled = true;
        }
    }

    // pelotaLenta = reinicia la velocidad minima a la pelota
    public void pelotaLenta()
    {
        PlayerPrefs.SetInt("velocidadPelota", 1);
    }

    public void pasarNivel()
    {
        GM.esteObjeto.bloques = 0;
    }

    public void popUpVolver()
    {
        PlayerPrefs.SetInt("mostrarPublicidad", 1);//asi muestra la publicidad cuando vuelve al menu principal
        GM.esteObjeto.mostrarPublicidad();
        Sonido.esteObjeto.sonidoBtnCancelar();
        int nivel = GM.esteObjeto.nivel;
        if (GM.esteObjeto.bloques == 0)
        {
            nivel++;
            int nivelAlc = PlayerPrefs.GetInt("nivelAlcanzado");
            if (nivel > nivelAlc) PlayerPrefs.SetInt("nivelAlcanzado", nivel);
        }
        if (nivel % 10 == 0)
        {
            PlayerPrefs.SetInt("popUpRate", 1);//para q muestre la ventana de comentar aplicacion
        }
        SceneManager.LoadScene("menuPrincipal");
    }

    public void popUpRecargarNivel()
    {
        Sonido.esteObjeto.sonidoBtnAceptar();
        if (!GM.esteObjeto.ganaste)
        {
            PlayerPrefs.SetInt("vidas", 3);
        }
        PlayerPrefs.SetInt("nivel", GM.esteObjeto.nivel);
        PlayerPrefs.SetInt("puntaje", 0);
        if (GM.esteObjeto.bloques == -1) PlayerPrefs.SetInt("esNivelJefe", 1);//si era nivel jefe para q juegue devuelta
        SceneManager.LoadScene("galaxia1");
    }

    public void popUpSiguienteNivel()
    {
        Sonido.esteObjeto.sonidoBtnAceptar();
        int nivel = GM.esteObjeto.nivel;
        if (GM.esteObjeto.bloques != -1)//si es distinto de -1 quiere decir q no era nivel jefe y suma nivel y carga la escena
        {
            if (nivel % 15 == 0)
            {
                PlayerPrefs.SetInt("esNivelJefe", 1);
            }
            nivel++;
            int nivelAlc = PlayerPrefs.GetInt("nivelAlcanzado");
            if (nivel > nivelAlc) PlayerPrefs.SetInt("nivelAlcanzado", nivel);
        }
        if (nivel % 10 == 0)
        {
            PlayerPrefs.SetInt("popUpRate", 1);//para q muestre la ventana de comentar aplicacion
        }
        //si es -1 es q le gano al jefe y antes ya se aumento el nivel a 16 porej asique no tiene q aumentar devuelta el nivel solo cargar la escena
        PlayerPrefs.SetInt("nivel", nivel);
        PlayerPrefs.SetInt("vidas", GM.esteObjeto.vidas); //asigno la cantidad de vidas
        SceneManager.LoadScene("galaxia1");
    }

    public void popUpOtraVida()
    {
        GM.esteObjeto.vidas = 1;
        Time.timeScale = 1;
        Scanvas.esteObjeto.slider.SetActive(true);
        GameObject.Find("pausa").GetComponent<Button>().enabled = true;
        Svaus.esteObjeto.desactivarUpgrades();
        GM.esteObjeto.puc.SetActive(false);
        GM.esteObjeto.pu.SetActive(false);
        GM.esteObjeto.puIconoVaus.SetActive(false);
        GM.esteObjeto.btnOtraVida.SetActive(false);//TODO pasar todo esto a un solo gameobject boton
        GM.esteObjeto.btnOtraVida.SetActive(false);

        //AdManager.esteObjeto.mostrarRewardedVideo();
    }

    public void HandleOnAdRewarded(object sender, Reward args)
    {
        //string txt = "Obtuviste 1 vida!" + args.Amount + args.Type;
        GM.esteObjeto.vidas = Int32.Parse(args.Amount.ToString()); //le doy la vida q gano

        //Dejo todos los parametros para q pueda seguir jugando normalmente
        Time.timeScale = 1;   
        Scanvas.esteObjeto.slider.SetActive(true);
        GameObject.Find("pausa").GetComponent<Button>().enabled = true;
        Svaus.esteObjeto.desactivarUpgrades();
        GM.esteObjeto.puc.SetActive(false);
        GM.esteObjeto.pu.SetActive(false);
        GM.esteObjeto.puIconoVaus.SetActive(false);
        GameObject.Find("btnPUSiguiente").GetComponent<Image>().enabled = false;//TODO pasar todo esto a un solo gameobject boton
        GameObject.Find("txtGanaste").GetComponent<Text>().text = "";
        GameObject.Find("btnPUOtraVida").GetComponent<Image>().enabled = false;
        GM.esteObjeto.btnOtraVida.SetActive(false);
    }
}

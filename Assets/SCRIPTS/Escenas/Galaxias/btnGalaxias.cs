using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class btnGalaxias : MonoBehaviour {
    int galaxia = 0;
    int cantGalaxia = 7; //la cantidad de galaxias para q las muestre bien y los botones tambien
    public float volumen = 1.0f;

    void Start () {
        mostrarObloquearGalaxia();
        asignarBotones();
    }
	
	void Update () {
		
	}

    void asignarBotones()//es para asignar los distintos fondos de pantalla por galaxia
    {
        //var go = EventSystem.current.currentSelectedGameObject;
        int i;
        Button btn = GetComponent<Button>();
        btn = GetComponent<Button>();
        for (i=0; i<cantGalaxia; i++)
        {
            if (gameObject.name =="Galaxia ("+i+")")
            {
                galaxia = i;
            }
        }

        if (gameObject.GetComponent<Image>().color == Color.white)
        {
            btn.onClick.AddListener(delegate
            {//asigna el fondo
                Sonido.esteObjeto.sonidoBtnAceptar();
                PlayerPrefs.SetInt("nivelByGalaxia", galaxia);
                PlayerPrefs.SetInt("fondoPantalla", galaxia + 1);
                SceneManager.LoadScene("selecNivel");
            });
            gameObject.GetComponentInChildren<Text>().enabled = false;
        }
        else
        {
            btn.onClick.AddListener(delegate
            {
                Sonido.esteObjeto.sonidoBtnDenegado();
            });
        }
    }

    void mostrarObloquearGalaxia()
    {
        for (int i = 0; i < cantGalaxia; i++)
        {
            if (gameObject.name == "Galaxia (" + i + ")")
            {
                int nivelAlcanzado = PlayerPrefs.GetInt("nivelAlcanzado");
                double aux = nivelAlcanzado / 15f;
                if (aux > i)
                {
                    gameObject.GetComponent<Image>().color = Color.white;
                }
            }
        }
    }

    //asignar()
    //{
    //    int nivelAlcanzado = PlayerPrefs.GetInt("nivelAlcanzado");
    //    int valor = 
    ////}
    //void asignarBotones()
    //{
    //    Button btn = GetComponent<Button>();
    //    btn = GetComponent<Button>();
    //    btn.onClick.AddListener(delegate
    //    {
    //        Debug.Log(PlayerPrefs.GetInt("nivelByGalaxia"));
    //        PlayerPrefs.SetInt("nivelByGalaxia", nivel);
    //        SceneManager.LoadScene("selecNivel");
    //    });

    //}
}

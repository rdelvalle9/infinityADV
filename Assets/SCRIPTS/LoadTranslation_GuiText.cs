using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadTranslation_GuiText : MonoBehaviour {

    //public string textInKeyLanguage;  // español
    public delegate void CambiarLenguajeDelegate(int lang);
    public static CambiarLenguajeDelegate CambiarLenguaje;

    void Start()
    {
        LoadTranslation(GameControl.GameLanguage);
        cambiarFuente();
    }

    void LoadTranslation(int lang)
    {

        Text tx = GetComponent<Text>();
        tx.text = LanguageManager.getTextInLanguage(gameObject.GetComponent<Text>().text, lang).ToString();
    }

    /*Cuando el objeto se active, enlace la funcion de traduccion*/
    void OnEnable()
    {
        LoadTranslation_GUIelement.CambiarLenguaje += LoadTranslation;
    }

    /*Cuando el objeto se desactive, desenlace la funcion de traduccion*/
    void OnDisable()
    {
        LoadTranslation_GUIelement.CambiarLenguaje -= LoadTranslation;
    }

    void cambiarFuente()
    {
        int lenguaje = PlayerPrefs.GetInt("idioma");
        if(lenguaje > 1)
        {
            gameObject.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;

            //menuOpciones menu = GameObject.Find("scripts").GetComponent<menuOpciones>();
            //gameObject.GetComponent<Text>().font = menu.fontArka;
            gameObject.GetComponent<Text>().fontStyle = FontStyle.Bold;
        }
        else
        {

        }
    }
}

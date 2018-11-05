using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTranslation_GUIelement : MonoBehaviour {

    public delegate void CambiarLenguajeDelegate(int lang);
    public static CambiarLenguajeDelegate CambiarLenguaje;

    public string textInKeyLanguage = "Cambiar Lenguaje";
    string textBoton;

    void Start()
    {
        textBoton = textInKeyLanguage;
        LoadTranslation(0);
    }

    void LoadTranslation(int lang)
    {
        textBoton = LanguageManager.getTextInLanguage(textInKeyLanguage, lang);
    }

    void OnGUI()
    {
        /*si damos click en el boton*/
        if (GUI.Button(new Rect(10, 10, 150, 70), textBoton))
        {
            /* Disparamos el evento de cambiar lenguaje */
            GameControl.GameLanguage = GameControl.GameLanguage == 1 ? 0 : 1;
            if (CambiarLenguaje != null) { CambiarLenguaje(GameControl.GameLanguage); }
        }
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
}

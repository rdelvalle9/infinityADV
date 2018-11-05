using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTranslation_3DText : MonoBehaviour {

    public string textInKeyLanguage;  // español

    void Start()
    {
        cambiarFuente();
        LoadTranslation(GameControl.GameLanguage);
        
        //LoadTranslation(PlayerPrefs.GetInt("idioma"));
        //LoadTranslation(0);
        //PlayerPrefs.SetInt("idioma", 0);
    }

    void LoadTranslation(int lang)
    {
        transform.GetComponent<TextMesh>().text =
                 LanguageManager.getTextInLanguage(textInKeyLanguage, lang);
    }

    void cambiarFuente()
    {
        menuOpciones menu = GameObject.Find("scripts").GetComponent<menuOpciones>();
        //gameObject.GetComponent<TextMesh>().font = menu.fontArka;
        //gameObject.GetComponent<TextMesh>().font = null;
        gameObject.GetComponent<TextMesh>().font = menu.fontDefault;

        //Text text3D = GameObject.Find(this).GetComponent<Text>().font;
    }
}

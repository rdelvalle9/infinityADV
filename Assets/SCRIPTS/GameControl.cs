using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {

    public static int GameLanguage = 0; //0=español, 1=ingles

    void Awake()
    {
        GameLanguage = PlayerPrefs.GetInt("idioma");
        LanguageManager.LoadLanguagesFile("Lenguajes");
    }

}

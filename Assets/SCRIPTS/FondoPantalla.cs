using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FondoPantalla : MonoBehaviour {
    // Use this for initialization
    void Start () {
        seleccionarFondo();
	}
	
	
	void Update () {
    }

    void seleccionarFondo()
    {
        //PlayerPrefs.SetInt("fondoPantalla", 1);
        int nFondo = PlayerPrefs.GetInt("fondoPantalla");
        Texture2D fondo = Resources.Load("fondos/galaxia"+ nFondo) as Texture2D;
        Sprite sprite = Sprite.Create(fondo, new Rect(0, 0, fondo.width, fondo.height), Vector2.zero);
        SpriteRenderer objFondo = GameObject.Find("fondo").GetComponent<SpriteRenderer>();
        objFondo.sprite = sprite;
        objFondo.transform.position = new Vector3(0, 0, 0);
        objFondo.transform.position = new Vector3(8.88f, -9.21f, 5.05f);
        //x=8.83 y=-15.64 z=12.1 scale=1.61761
    }



}

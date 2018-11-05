using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnRateUs : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Material estrellas = GameObject.Find("imgEstrellas").GetComponent<Material>().shader.t
	}

    public void irACalificar()
    {
        cambiarEstadoPlayerPrefs();
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.infinityCR.Arkadroid3D&showAllReviews=true");
    }

    //public void activarBotones()
    //{
    //    if (PlayerPrefs.GetInt("continuarNivel") == 0)
    //    {
    //        GameObject.Find("continuar").GetComponent<Button>().enabled = true;
    //        GameObject.Find("continuar").GetComponentInChildren<Text>().enabled = true;
    //    }
    //}

    public void cambiarEstadoPlayerPrefs()
    {
        if (PlayerPrefs.GetInt("continuarNivel") == 0)
        {
            GameObject.Find("continuar").GetComponent<Button>().enabled = false;
            GameObject.Find("continuar").GetComponentInChildren<Text>().enabled = false;
        }
        else {
            GameObject.Find("continuar").GetComponent<Button>().enabled = true;
            GameObject.Find("continuar").GetComponentInChildren<Text>().enabled = true;
        }
        PlayerPrefs.SetInt("popUpRate", 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdiomaGlobal : MonoBehaviour {

    public string IdiomaActual = "espanol";

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void CambiarIdioma(string idioma)
    {
        IdiomaActual = idioma;
        NotificationCenter.DefaultCenter().PostNotification(this, "CambiarIdioma_", idioma);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

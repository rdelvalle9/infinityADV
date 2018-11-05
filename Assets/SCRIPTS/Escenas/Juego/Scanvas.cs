using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanvas : MonoBehaviour {

    public GameObject slider;
    public GameObject fondoElijeOpcion;
    public GameObject guardar;
    public static Scanvas esteObjeto;
	// Use this for initialization
	void Start () {
        if (esteObjeto == null) { esteObjeto = this; } else if (esteObjeto != this) { Destroy(gameObject); }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

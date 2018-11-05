using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jefe1Muestra : MonoBehaviour
{

    string nombreJefe = "Jefe1(Clone)";


    bool mover = true;
    Vector3 movi = new Vector3(); // Vector3 de Movimento
    float varCambiarColor;
    int directionMov = 1; //Direccion del moviento del Jefe, inicial 1 = Derecha  2 = Diagonal hacia abajo y al centro   3 = Diagonal hacia arriba y a la izquierda

    float sDisparo;
    float sDisparoFinal;
    bool bDeDisparoFinal = false;
    bool bDeDisparo = false;
    bool bDispModo1 = false;
    int swColorTorus = 0;
    bool recibioDanio = false;
    int vidaJefe = 20;
  

    // Use this for initialization
    void Start()
    {
        Transform torus = GameObject.Find("Torus").GetComponent<Transform>();
        //transform.position = new Vector3(intialX, InitialY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Transform esferaJefe = GameObject.Find("esferaJefe").GetComponent<Transform>();
        esferaJefe.transform.Rotate(new Vector3(0, 0, 50f * Time.deltaTime));
        Transform torus = GameObject.Find("Torus").GetComponent<Transform>();
        torus.transform.Rotate(new Vector3(3f, 2.5f, 3f));
    }
}
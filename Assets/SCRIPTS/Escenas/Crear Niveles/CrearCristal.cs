using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrearCristal : MonoBehaviour {

    GameObject cristal;

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name != "bicho")
        {
            cristal = Resources.Load("cristal") as GameObject;
            Instantiate(cristal, transform.position, transform.rotation);
        }
    }
}

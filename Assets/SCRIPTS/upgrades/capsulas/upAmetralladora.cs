using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upAmetralladora : MonoBehaviour {

    GM scriptGM;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Vaus")//cuando choca con la vaus activa la habilidad
        {
            scriptGM = GameObject.Find("GM").GetComponent<GM>();//busca el objeto del script GM y lo asigna a la variable
            Svaus vaus = GameObject.Find("Vaus").GetComponent<Svaus>();//busca el objeto del script y lo asigna a la variable
            vaus.desactivarUpgrades(); //desactivo cualquier otro upgrade
            vaus.bComprimir = true;
            vaus.tiempoAmetralladora = scriptGM.segundos + 5;
            vaus.bActivarAmetralladora = true;//activa la habilidad ametralladora de la vaus
            
            scriptGM.capsulaEnCaida = false;
            Destroy(gameObject);//y destruye la capsula
        }
    }

    void Update()
    {
        scriptGM = GameObject.Find("GM").GetComponent<GM>();//busca el objeto del script GM y lo asigna a la variable
        if (gameObject.transform.position.y < -4)
        {//destruye la capsula cuando pasa del eje Y<-4
            scriptGM.capsulaEnCaida = false;
            Destroy(gameObject);
        }
    }
}

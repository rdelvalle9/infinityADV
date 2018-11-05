﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upBloque : MonoBehaviour
{

    GM scriptGM;

    void Start()
    {
        scriptGM = GameObject.Find("GM").GetComponent <GM>();//busca el objeto del script GM y lo asigna a la variable
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Vaus")//cuando choca con la vaus activa la habilidad
        {
            Svaus vaus = GameObject.Find("Vaus").GetComponent <Svaus>();//busca el objeto del script y lo asigna a la variable
            vaus.desactivarUpgrades(); //desactivo cualquier otro upgrade
            vaus.bBloque = true;
            scriptGM.capsulaEnCaida = false;
            Destroy(gameObject);//y destruye la capsula
        }
    }

    void Update()
    {
        if (gameObject.transform.position.y < -4)
        {//destruye la capsula cuando pasa del eje Y<-4
            scriptGM.capsulaEnCaida = false;
            Destroy(gameObject);
        }
    }
}
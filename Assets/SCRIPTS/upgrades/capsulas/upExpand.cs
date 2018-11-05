﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class upExpand : MonoBehaviour
{
    GM scriptGM;

    void Start()
    {
        scriptGM = GameObject.Find("GM").GetComponent<GM>();//busca el objeto del script GM y lo asigna a la variable
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Vaus")//cuando choca con la vaus activa la habilidad
        {
            Svaus vaus = GameObject.Find("Vaus").GetComponent<Svaus>();//busca el objeto del script y lo asigna a la variable
            vaus.desactivarUpgrades(); //desactivo cualquier otro upgrade
            vaus.bExpandir = true;//activa la habilidad expandir de la vaus
            scriptGM.capsulaEnCaida = false;
            Slider slider = GameObject.Find("Slider").GetComponent<Slider>();//para q la vaus larga no traspase las paredes
            slider.minValue = -3;
            slider.maxValue = 3;
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

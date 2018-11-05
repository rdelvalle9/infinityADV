using UnityEngine;
using System.Collections;

public class upLaser : MonoBehaviour
{
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
            Svaus vaus = GameObject.Find("Vaus").GetComponent<Svaus>();//busca el objeto del script y lo asigna a la variable
            vaus.desactivarUpgrades(); //desactivo cualquier otro upgrade
            vaus.bComprimir = true;
            vaus.bActivarLaser = true;//activa la habilidad laser de la vaus
            scriptGM = GameObject.Find("GM").GetComponent<GM>();//busca el objeto del script GM y lo asigna a la variable
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

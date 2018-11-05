using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crearCapAmetralladora : MonoBehaviour {

    private contCapsulas scriptContCapsulas;
    GM scriptGM;
    // Use this for initialization
    void Start()
    {
        scriptContCapsulas = GameObject.Find("contenedorCapsulas").GetComponent<contCapsulas>();//busca el objeto del script y lo asigna a la variable
    }

    void OnCollisionEnter(Collision col) //ANGULOS DE LA PELOTA
    {
        if (col.gameObject.name != "bicho")
        {
            scriptGM = GameObject.Find("GM").GetComponent<GM>();//busca el objeto del script GM y lo asigna a la variable
            Vector3 pos = transform.position;
            Svaus scriptVaus = GameObject.Find("Vaus").GetComponent<Svaus>();
            if (scriptGM.capsulaEnCaida == false)
            {
                if (!scriptVaus.bActivarLaser)
                {
                    scriptContCapsulas.crearCapsula(pos,10);
                }
                else
                {
                    scriptContCapsulas.crearCapsulaRepetida(1, pos);
                    Debug.Log("cambio capsula");
                }
                scriptGM.capsulaEnCaida = true;
            }
            Destroy(gameObject);
        }
    }
}

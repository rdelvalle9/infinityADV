using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crearCapIA : MonoBehaviour {
    private contCapsulas scriptContCapsulas;
    GM scriptGM;
    // Use this for initialization
    void Start()
    {
        //busca el objeto del script y lo asigna a la variable
        scriptContCapsulas = GameObject.Find("contenedorCapsulas").GetComponent<contCapsulas>();
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
                if (!scriptVaus.bIA)
                {
                    scriptContCapsulas.crearCapsula(pos,6);
                }
                else
                {
                    scriptContCapsulas.crearCapsulaRepetida(5, pos);
                    Debug.Log("cambio capsula");
                }
                scriptGM.capsulaEnCaida = true;
            }
            Destroy(gameObject);
        }
    }


}
using UnityEngine;
using System.Collections;

public class crearCapVida : MonoBehaviour {
    private contCapsulas scriptContCapsulas;
    GM scriptGM;
    // Use this for initialization
    void Start()
    {
        scriptContCapsulas = GameObject.Find("contenedorCapsulas").GetComponent<contCapsulas>();//busca el objeto del script y lo asigna a la variable
        
    }

    void OnCollisionEnter(Collision col) //ANGULOS DE LA PELOTA
    {
        if(col.gameObject.name != "bicho")
        {
            scriptGM = GameObject.Find("GM").GetComponent<GM>();//busca el objeto del script GM y lo asigna a la variable
            Vector3 pos = transform.position;
            if (scriptGM.capsulaEnCaida == false)
            {
                scriptContCapsulas.crearCapsulaVida(pos);
                scriptGM.capsulaEnCaida = true;
            }
        }
    }
}

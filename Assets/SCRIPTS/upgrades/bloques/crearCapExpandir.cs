using UnityEngine;
using System.Collections;

public class crearCapExpandir : MonoBehaviour
{
    private contCapsulas scriptContCapsulas;
    GM scriptGM;
    // Use this for initialization
    void Start()
    {
        //busca el objeto del script y lo asigna a la variable
        scriptContCapsulas = GameObject.Find("contenedorCapsulas").GetComponent<contCapsulas>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name != "bicho")
        {
            scriptGM = GameObject.Find("GM").GetComponent<GM>();//busca el objeto del script GM y lo asigna a la variable
            Vector3 pos = transform.position;
            Svaus scriptVaus = GameObject.Find("Vaus").GetComponent<Svaus>();
            if (scriptGM.capsulaEnCaida == false)
            {
                if (!scriptVaus.bExpandir)
                {
                    scriptContCapsulas.crearCapsulaExpandirVaus(pos);
                }
                else
                {
                    scriptContCapsulas.crearCapsulaRepetida(4, pos);
                    Debug.Log("cambio capsula");
                }
                scriptGM.capsulaEnCaida = true;
            }
            Destroy(gameObject);
        }
    }


}

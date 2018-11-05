using UnityEngine;
using System.Collections;

public class crearCapLaserDificil : MonoBehaviour {
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
                    scriptContCapsulas.crearCapsulaLaserDificil(pos);
                }
                else
                {
                    scriptContCapsulas.crearCapsulaRepetida(2, pos);
                    Debug.Log("cambio capsula");
                }
                scriptGM.capsulaEnCaida = true;
            }
            Destroy(gameObject);
        }
    }
}

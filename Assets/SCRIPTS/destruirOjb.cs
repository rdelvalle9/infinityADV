using UnityEngine;
using System.Collections;

public class destruirOjb : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        GM scriptGM;
        if (col.gameObject.name != "bicho")
        {
            scriptGM = GameObject.Find("GM").GetComponent<GM>();//busca el objeto del script y lo asigna a la variable

            scriptGM.puntaje++;
            scriptGM.puntajeTotal++;

            GM.esteObjeto.descontarBloque();

            Destroy(gameObject);
        }
    }

}

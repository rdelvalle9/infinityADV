using UnityEngine;
using System.Collections;

public class upVida : MonoBehaviour
{
    GM scriptGM;

    void Start()
    {
        scriptGM = GameObject.Find("GM").GetComponent<GM>();//busca el objeto del script GM y lo asigna a la variable
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Vaus")//cuando choca con la vaus busca el objeto vidas y le aumenta una
        {
            Svaus vaus = GameObject.Find("Vaus").GetComponent<Svaus>();//busca el objeto del script y lo asigna a la variable
            vaus.desactivarUpgrades(); //desactivo cualquier otro upgrade
            scriptGM.vidas++;
            PlayerPrefs.SetInt("vidas", scriptGM.vidas);    //guardo en disco el valor actual de vidas para tener las mismas en el sig. nivel
            scriptGM.capsulaEnCaida = false;
            Destroy(gameObject);
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

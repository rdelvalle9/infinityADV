using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrearCapsula : MonoBehaviour
{
    public int numeroCapsula;
    // Use this for initialization

    public CrearCapsula(int numeroCapsula)
    {
        this.numeroCapsula = numeroCapsula;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name != "bicho")
        {
            Vector3 pos = transform.position;
            if (GM.esteObjeto.capsulaEnCaida == false)
            {
                //if (!Svaus.esteObjeto.bIA)
                //{
                contCapsulas.esteObjeto.crearCapsula(pos, numeroCapsula);
                //}
                //else
                //{
                //    //contCapsulas.esteObjeto.crearCapsulaRepetida(5, pos);
                //    Debug.Log("cambio capsula");
                //}

            }
            Destroy(gameObject);
        }
    }
}

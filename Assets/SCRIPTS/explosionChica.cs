using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionChica : MonoBehaviour
{

    public GameObject explosion;
    public int scoreValue;

    void OnCollisionEnter(Collision col) //ANGULOS DE LA PELOTA
    {
        if (col.gameObject.name == "piso") //al chocar con 
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}

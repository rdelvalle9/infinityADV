using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destruirExp : MonoBehaviour {
    private GM scriptGM;
    public float segundosAcnt;
    // Use this for initialization
    void Start () {
        scriptGM = GameObject.Find("GM").GetComponent<GM>();//busca el objeto del script y lo asigna a la variable
        segundosAcnt = scriptGM.segundos + 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (scriptGM.segundos > segundosAcnt)
        {
            Destroy(gameObject);
        }
    }
}

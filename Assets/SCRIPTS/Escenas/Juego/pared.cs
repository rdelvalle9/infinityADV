using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pared : MonoBehaviour
{
    public static pared esteObjeto = null;
    public GameObject bichoCono1;
    public GameObject bichoCono2;
    public bool bAbrirPuertaArribaIzq = false;
    public bool bCrearBicho = false;
    public bool bJugar;
    public int cntBichos = 0;
    public bool paredFalsa;
    float segundosAesperar = 10;
    public float tiempoDeCreacionBicho;

    void Start()
    {
        if (esteObjeto == null) { esteObjeto = this; } else if (esteObjeto != this) { Destroy(gameObject); }
        bichoCono1 = Resources.Load("bicho1") as GameObject;
        paredFalsa = false;
        if(PlayerPrefs.GetInt("enemigosInGame") == 0)
        {
            paredFalsa = true;
        }
        tiempoDeCreacionBicho = segundosAesperar;
    }

    void Update()
    {
        this.crearBichos();
    }

    // crearBichos = crea los bichos en la pared cuando se abre la puerta
    public void crearBichos()
    {
        if (tiempoDeCreacionBicho < GM.esteObjeto.segundos + .2f && cntBichos < 3 && !paredFalsa)//para abrir la puerta
        {
            GateUpL.esteObjeto.abrirPuerta();
        }
        if (tiempoDeCreacionBicho < GM.esteObjeto.segundos)
        {
            //si la cantidad de bichos es menor a 3 crea otro bicho
            if (!paredFalsa && cntBichos < 3)
            {
                this.crearBicho();
                //si la cantidad de bichos es 3 cierra la puerta
                GateUpL.esteObjeto.cerrarPuerta();

                asignarTiempoParaCrearBicho();
            }
        }
    }

    void crearBicho()
    {
        Vector3 posBicho = new Vector3(-2.56f, 12.42f, 0);
        bichoCono2 = Instantiate(bichoCono1, posBicho, Quaternion.identity) as GameObject;
        bichoCono2.name = "bicho";
        cntBichos++;
    }

    public void asignarTiempoParaCrearBicho()
    {
        tiempoDeCreacionBicho = GM.esteObjeto.segundos + segundosAesperar;
    }
}

#region old
/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pared : MonoBehaviour
{
    public static pared esteObjeto = null;
    public GameObject bichoCono1;
    public GameObject bichoCono2;
    public bool bAbrirPuertaArribaIzq = false;
    public bool bCrearBicho = false;
    public bool bJugar;
    Animator animator;
    public int cntBichos = 0;
    public bool paredFalsa;

    void Start()
    {
        if (esteObjeto == null) { esteObjeto = this; } else if (esteObjeto != this) { Destroy(gameObject); }
        animator = GetComponent<Animator>();
        animator.SetBool("bAbrirPuertaArrIzq", bAbrirPuertaArribaIzq);
        animator.SetBool("bCrearBicho", bCrearBicho);
        animator.SetBool("jugar", bJugar);
        bichoCono1 = Resources.Load("bicho1") as GameObject;
        paredFalsa = true;
        if(PlayerPrefs.GetInt("enemigosInGame") == 0)
        {
            paredFalsa = false;
        }
    }

    void Update()
    {
        crearBichos();
        //asigna las variables para la pared q es la q crea los bichos
        animator.SetBool("bAbrirPuertaArrIzq", bAbrirPuertaArribaIzq);
        animator.SetBool("bCrearBicho", bCrearBicho);
        animator.SetBool("jugar", bJugar);
    }

    // crearBichos = crea los bichos en la pared cuando se abre la puerta
    public void crearBichos()
    {
        if (cntBichos < 3 && paredFalsa == false)
        {
            bAbrirPuertaArribaIzq = true;
            animator.SetBool("bAbrirPuertaArrIzq", bAbrirPuertaArribaIzq);  //HAY Q ASIGNAR DEVUELTA EN EL ANIMATOR PARA Q LO TOME!!!
        }
        //si la cantidad de bichos es menor a 3 crea otro bicho
        if (bCrearBicho == true && bAbrirPuertaArribaIzq == true && cntBichos < 3)
        {
            bCrearBicho = false;
            animator.SetBool("bCrearBicho", bCrearBicho);
            Vector3 posBicho = new Vector3(-2.56f, 12.42f, 0);           
            bichoCono2 = Instantiate(bichoCono1, posBicho, Quaternion.identity) as GameObject;
            bichoCono2.name = "bicho";
            cntBichos++;
            //si la cantidad de bichos es 3 cierra la puerta
            if (cntBichos == 3)
            {
                bAbrirPuertaArribaIzq = false;
                animator.SetBool("bAbrirPuertaArrIzq", bAbrirPuertaArribaIzq);  //HAY Q ASIGNAR DEVUELTA EN EL ANIMATOR PARA Q LO TOME!!!
            }
        }
    }

    public void paredRoja()
    {
        for (int i = 1; i < 27; i++)
        {
            //GameObject.Find("Cylinder_00" + i).GetComponent<>
        }
    }
}
*/
#endregion

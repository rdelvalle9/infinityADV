using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateUpL : MonoBehaviour
{
    public static GateUpL esteObjeto = null;
    public bool bAbrirPuertaArrL = false;
    Animator animator;

    void Start()
    {
        if (esteObjeto == null) { esteObjeto = this; } else if (esteObjeto != this) { Destroy(gameObject); }
        animator = GetComponent<Animator>();
        animator.SetBool("bAbrirPuertaArrL", bAbrirPuertaArrL);
    }

    void Update()
    {
        animator.SetBool("bAbrirPuertaArrL", bAbrirPuertaArrL);
    }

    public void abrirPuerta()
    {
        bAbrirPuertaArrL = true;
        asignarAnimator();
    }

    public void cerrarPuerta()
    {
        bAbrirPuertaArrL = false;
        asignarAnimator();
    }

    void asignarAnimator()
    {
        animator.SetBool("bAbrirPuertaArrL", bAbrirPuertaArrL);
    }

}
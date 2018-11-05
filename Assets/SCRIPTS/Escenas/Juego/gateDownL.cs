using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateDownL : MonoBehaviour {

    public static gateDownL esteObjeto = null;
    public bool bAbrirPuertaAbaL = false;
    Animator animator;
  
    void Start()
    {
        if (esteObjeto == null) { esteObjeto = this; } else if (esteObjeto != this) { Destroy(gameObject); }
        animator = GetComponent<Animator>();
        animator.SetBool("bAbrirPuertaAbaL", bAbrirPuertaAbaL);
    }

    void Update()
    {
        animator.SetBool("bAbrirPuertaAbaL", bAbrirPuertaAbaL);
    }
}

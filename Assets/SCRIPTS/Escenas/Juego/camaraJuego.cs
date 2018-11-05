using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camaraJuego : MonoBehaviour {
    Animator animator;
    Svaus scriptVaus;
    public bool bSeguirVausLaser;
    bool bMovCamara = false;
    bool bMovCamara2 = false;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        bSeguirVausLaser = false;
    }
	
	// Update is called once per frame
	void Update () {
        //scriptVaus = GameObject.Find("Vaus").GetComponent<Svaus>();
        //animator.SetBool("acercarVlaser", scriptVaus.bActivarLaser);
        //animator.SetBool("seguirVausLaser", bSeguirVausLaser);
        //Debug.Log("rot: " + transform.rotation);
        if (bSeguirVausLaser == true)
        {
            seguirConLaCamara();
        }
        else
        {
            bSeguirVausLaser = false;
            transform.position = new Vector3(0, 6.5f, -7);
            if (bMovCamara == true)
            {
                transform.Rotate(new Vector3(30f, 0, 0));
                bMovCamara = false;
            }
            //transform.Rotate(new Vector3(30f, 0, 0) * Time.deltaTime);
        }
    }

    public void seguirConLaCamara()
    {
        //animator.enabled = false;
        scriptVaus = GameObject.Find("Vaus").GetComponent<Svaus>();
        transform.position = new Vector3(scriptVaus.transform.position.x, -0.05f, -2.78f);
        if (bMovCamara == false)
        {
            transform.Rotate(new Vector3(-30f, 0, 0));
            bMovCamara = true;
            //Debug.Log("rot: " + transform.rotation);
        }
        
        if (scriptVaus.bActivarLaser == false)
        {
            
            //animator.enabled = true;
            Debug.Log("rot: " + transform.rotation);
        }
        //Debug.Log("rot: "+ transform.rotation);
    }


}

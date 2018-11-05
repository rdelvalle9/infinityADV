using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaJefe : MonoBehaviour {
    public int vidaJefe;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setVida(int vidaJefe)
    {
        this.vidaJefe = vidaJefe;
    }

    public int getVida()
    {
        return vidaJefe;
    }

    public void restarVida()
    {
        vidaJefe--;
    }
}

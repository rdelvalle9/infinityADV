using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musica : MonoBehaviour {
    public AudioClip menuPrincipal;
    // Use this for initialization
    void Start () {
        //DontDestroyOnLoad(gameObject);
        repMusica();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // repMusica = reproduce la musica
    public void repMusica()
    {
        int m = PlayerPrefs.GetInt("musica");
        AudioSource musica = GameObject.Find("musica").GetComponent<AudioSource>();
        if (m == 0)
        {
            //if (menuPrincipal) AudioSource.PlayClipAtPoint(menuPrincipal, transform.position, 1);
            musica.enabled = false;
        }
    }
}

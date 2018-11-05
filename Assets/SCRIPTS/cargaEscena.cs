//using admob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cargaEscena : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Admob.Instance().loadInterstitial();//CARGA EL VIDEO EN LA RAM
        SceneManager.LoadScene("galaxia1");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuSelecNivel : MonoBehaviour {
    public GameObject miniPared;
    // Use this for initialization
    void Start () {
        GameObject.Find("crear").GetComponent<crearNiveles>().cargarNiveles();
        Debug.Log(PlayerPrefs.GetInt("nivelByGalaxia"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void irAGalaxias()
    {
        SceneManager.LoadScene("galaxias");
    }

    //public void OnClickBtnNivel()
    //{
    //    var go = EventSystem.current.currentSelectedGameObject;
    //    crearNiveles.esteObjeto.guardarNumNivelBtn(go.name);
    //}
}

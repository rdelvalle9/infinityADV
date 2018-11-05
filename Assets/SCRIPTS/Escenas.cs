using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Escenas : MonoBehaviour {
    public GameObject imagencarga;
    public Slider barra;
    AsyncOperation asyn;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void continuarJuego()
    {
        clickCarga("galaxia1");
    }

    // clickCarga = carga la escena pero antes muestra la pantalla de carga
    public void clickCarga(string escena)
    {
        imagencarga.SetActive(true);
        StartCoroutine(Loadlevelslider(escena));
    }

    // Loadlevelslider = funcion de clickCarga
    IEnumerator Loadlevelslider(string escena)
    {
        asyn = SceneManager.LoadSceneAsync(escena);
        while (!asyn.isDone)
        {
            barra.value = asyn.progress;
            yield return null;
        }
    }

    public void irMenuPrincipal()
    {
        SceneManager.LoadScene("menuPrincipal");
    }
}


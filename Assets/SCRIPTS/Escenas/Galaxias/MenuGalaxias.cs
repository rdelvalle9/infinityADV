using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuGalaxias : MonoBehaviour {
    
    // Use this for initialization
    void Start() {
        setParametrosIniciales();
    }

    // Update is called once per frame
    void Update() {

    }

    public void cargarNivel()
    {
        //AdManager.Instance.mostrarVideo(); //para ver los adds de publicidad
        SceneManager.LoadScene("selecNivel");
    }

    //setParametrosIniciales = establece los parametros iniciales de la primera vez q se abre el juego
    public void setParametrosIniciales()
    {
        
        //PlayerPrefs.SetInt("fondoPantalla", 1);
        PlayerPrefs.SetInt("vidas", 3);
        PlayerPrefs.SetInt("puntaje", 0);
        //PlayerPrefs.SetInt("nivelAlcanzado", 1);

        //PlayerPrefs.SetInt("esNivelJefe", 0);
        //PlayerPrefs.SetInt("nivel", 1);
        //PlayerPrefs.SetInt("vidas", 3);
        //PlayerPrefs.SetInt("continuarNivel", 0);
        //PlayerPrefs.SetInt("vidasGuardadas", 0);
    }



    //public void OnClickBtnNivel()
    //{
    //    var go = EventSystem.current.currentSelectedGameObject;
    //    crearNiveles.esteObjeto.guardarNumNivelBtn(go.name);
    //}

}

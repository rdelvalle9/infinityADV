using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// este script es para mostrar la escena cargandose con la barra q se llena
public class barraCarga : MonoBehaviour
{
    public static barraCarga esteObjeto;
    public GameObject imagencarga;
    public Slider barra;

    AsyncOperation asyn;

    void Start()
    {
        if (esteObjeto == null) { esteObjeto = this; } else if (esteObjeto != this) { Destroy(gameObject); }
    }

    public void ClickCarga(int nivel)
    {
        imagencarga.SetActive(true);
        StartCoroutine(Loadlevelslider(nivel));
    }

    IEnumerator Loadlevelslider(int nivel)
    {
        asyn = SceneManager.LoadSceneAsync(nivel);
        while (!asyn.isDone)
        {
            barra.value = asyn.progress;
            yield return null;
        }
    }
}

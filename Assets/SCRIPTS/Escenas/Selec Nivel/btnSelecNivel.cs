using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class btnSelecNivel : MonoBehaviour {
    public int nivel;
    int galaxia;
    public float volumen = 1.0f;
    void Start()
    {
        galaxia = PlayerPrefs.GetInt("nivelByGalaxia");
        mostrarObloquearNivel();
        asignarBoton();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //FFFFFF7B
    void asignarBoton()
    {
        Button btn = GetComponent<Button>();
        Color32 colorBlancoTrans = new Color32(255, 255, 255, 123);
        if (gameObject.GetComponent<Image>().color != colorBlancoTrans)
        {
            btn = GetComponent<Button>();
            btn.onClick.AddListener(delegate {
                //var go = EventSystem.current.currentSelectedGameObject;
                Sonido.esteObjeto.sonidoBtnAceptar();
                PlayerPrefs.SetInt("nivel", (nivel + (15 * galaxia)));
                if (nivel == 16) PlayerPrefs.SetInt("esNivelJefe", 1);
                SceneManager.LoadScene("galaxia1");
            });
        }
        else
        {
            btn.onClick.AddListener(delegate
            {
                Sonido.esteObjeto.sonidoBtnDenegado();
            });
        }
    }

    void mostrarObloquearNivel()
    {
        try
        {
            Color32 transparente = new Color32(255, 255, 255, 0);

            for (int i = 0; i < 16; i++)
            {
                int aux=i;
                if (gameObject.name == "Button (" + i + ")")
                {
                    int nivelAlcanzado = PlayerPrefs.GetInt("nivelAlcanzado");
                    int galaxia = PlayerPrefs.GetInt("nivelByGalaxia");
                    int boton = i + 1;
                    if ( ((15 * galaxia) + boton) <= nivelAlcanzado)
                    {
                        gameObject.GetComponent<Image>().color = transparente;
                    }
                }
            }
            /*for (int i = 0; i < 16; i++)
            {
                if (gameObject.name == "Button (" + i + ")")
                {
                    int nivelAlcanzado = PlayerPrefs.GetInt("nivelAlcanzado");
                    double aux = nivelAlcanzado / 16f;
                    if (i+1 >= aux)
                    {
                        gameObject.GetComponent<Image>().color = transparente;
                    }
                }
            }*/



        }
        catch (System.Exception)
        {

            throw;
        }
    }

    //void asignarBoton()
    //{
    //    Button btn = GetComponent<Button>();
    //    btn = GetComponent<Button>();
    //    btn.onClick.AddListener(delegate {
    //        //var go = EventSystem.current.currentSelectedGameObject;
    //        repSonAceptado();
    //        PlayerPrefs.SetInt("nivel", (nivel + (15 * galaxia)));
    //        if (nivel == 16) PlayerPrefs.SetInt("esNivelJefe", 1);
    //        SceneManager.LoadScene("galaxia1");
    //    });
    //}

    

    //public void OnClickBtnNivel()
    //{
    //    var go = EventSystem.current.currentSelectedGameObject;
    //    crearNiveles.esteObjeto.guardarNumNivelBtn(go.name);
    //}
}

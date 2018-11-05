using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//clase de la escena crear niveles de los 220 botones para crear los bloques y 
//mantener el color seleccionado
public class botonCrear : MonoBehaviour
{
    private crearNiveles crearNivel;
    mantenerColor scriptMantenerColor;
    Button componenteImagen;
    public int nColor;


    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        componenteImagen = GetComponent<Button>();
        crearNiveles scriptCrearNiveles = GameObject.Find("crear").GetComponent<crearNiveles>();
        Material mColor = scriptCrearNiveles.naranja;
        switch (nColor)
        {
            case 0:
                componenteImagen.image.color = new Color32(208, 233, 191, 55);   //objeto buton del boton.componente del buton.parametro color
                break;
            case 1:
                componenteImagen.image.color = Color.red;   //objeto buton del boton.componente del buton.parametro color
                break;
            case 2:
                componenteImagen.image.color = Color.green;   //objeto buton del boton.componente del buton.parametro color
                break;
            case 3:
                componenteImagen.image.color = Color.blue;   //objeto buton del boton.componente del buton.parametro color
                break;
            case 4:
                componenteImagen.image.color = Color.yellow;   //objeto buton del boton.componente del buton.parametro color
                break;
            case 5:
                componenteImagen.image.color = new Color32(255, 153, 0, 255);   //objeto buton del boton.componente del buton.parametro color
                break;
            case 6:
                componenteImagen.image.color = new Color32(150, 84, 0, 255);   //objeto buton del boton.componente del buton.parametro color
                break;
            case 7:
                componenteImagen.image.color = new Color32(7, 114, 7, 255);   //objeto buton del boton.componente del buton.parametro color
                break;
            case 8:
                componenteImagen.image.color = new Color32(0, 234, 255, 255);   //objeto buton del boton.componente del buton.parametro color
                break;
            case 9:
                componenteImagen.image.color = new Color32(251, 154, 255, 255);   //objeto buton del boton.componente del buton.parametro color
                break;
            case 10:
                componenteImagen.image.color = new Color32(229, 0, 116, 255);   //objeto buton del boton.componente del buton.parametro color
                break;
            case 11:
                componenteImagen.image.color = new Color32(255, 255, 255, 255);   //objeto buton del boton.componente del buton.parametro color
                break;
            case 12:
                componenteImagen.image.color = new Color32(141, 141, 141, 255);   //objeto buton del boton.componente del buton.parametro color
                break;
            case 13:
                componenteImagen.image.color = new Color32(0, 0, 0, 255);   //objeto buton del boton.componente del buton.parametro color
                break;
            case 14:
                //componenteImagen.image.color = new Color32(128, 111, 111, 255);   //objeto buton del boton.componente del buton.parametro color
                break;
        }
    }

    public void OnButtonClick()
    {
        scriptMantenerColor = GameObject.Find("crear").GetComponent<mantenerColor>();//busca el objeto del script y lo asigna a la variable
        var go = EventSystem.current.currentSelectedGameObject;
        componenteImagen = GetComponent<Button>();
        botonCrear scriptBotonCrear = GameObject.Find(go.name).GetComponent<botonCrear>();  //le paso el nombre asi busca el objeto boton para traer su componente
        scriptBotonCrear.nColor = scriptMantenerColor.color;  //HAY QUE TRAER ESTA MIERDA TODO HAY QUE TRAER SINO NO LO GUARDA!!
        crearNivel = GameObject.Find("crear").GetComponent<crearNiveles>();//busca el objeto del script y lo asigna a la variable

        crearNivel.guardarBloque(go.name, scriptMantenerColor.color);    //SIEMPRE HAY QUE BUSCAR ANTES EL OBJETO SINO NO FUNCIONA!!-------------------------------------------
    }






}

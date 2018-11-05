using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// mantiene el color de cuando creas en nivel para apretar varios 
//bloques a la vez sin perder el color seleccionado
public class mantenerColor : MonoBehaviour
{
    public int color;

    public void OnButtonClick()
    {
        var go = EventSystem.current.currentSelectedGameObject;
        for (int i = 0; i < 14; i++)
        {
            if (go.name == i.ToString())
            {
                color = i;
            }
        }
    }
}

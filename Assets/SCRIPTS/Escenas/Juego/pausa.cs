using UnityEngine;
using System.Collections;

public class pausa : MonoBehaviour
{
    bool fpausa = false;

    // ponerPausa = pone la pausa en el juego/ reduce el tiempo a 0
    void ponerPausa()
    {
        Debug.Log("funcion Pausa");
        if (fpausa == false)
        {
            Time.timeScale = 0;
            fpausa = true;
            //Debug.Log(fpausa);
        }
        else
        {
            Time.timeScale = 1;
            fpausa = true;
        }
    }
}

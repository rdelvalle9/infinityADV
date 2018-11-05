using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class viewModel : MonoBehaviour
{

    public GameObject vaus;

    // para el funcionamiento del mover la vaus tocando la pantalla
    public void slider_changed(float newValue)
    {
        //movimientoVaus vaus = GameObject.Find("vaus(Clone)").GetComponent<movimientoVaus>();
        vaus = GameObject.Find("Vaus");
        Vector3 pos = vaus.transform.position;
        if (newValue >= 3.1f)
        {
            pos.x = newValue;
            vaus.transform.position = pos;
        }
        else
        {
            pos.x = newValue;
            vaus.transform.position = pos;
        }
    }


}

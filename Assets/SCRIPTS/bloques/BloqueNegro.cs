using UnityEngine;
using System.Collections;

public class BloqueNegro : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

       
    }

    // Update is called once per frame
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name != "bicho")
        {
            Sonido.esteObjeto.sonidoChoqueAbloqueNegro();
        }
    }
}

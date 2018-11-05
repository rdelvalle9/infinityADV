using UnityEngine;
using System.Collections;

//sonido al tocar vaus
public class sonidos : MonoBehaviour
{
    public AudioClip sonido = null;
    public float volumen = 1.0f;
    protected Transform posicion = null;

    void Start()
    {
        posicion = transform;
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Vaus") //al chocar con la vaus
        {
            if (sonido) AudioSource.PlayClipAtPoint(sonido, posicion.position, volumen);
        }
    }


}

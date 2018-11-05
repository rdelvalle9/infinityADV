using UnityEngine;
using System.Collections;

public class sonAlTocarVaus : MonoBehaviour
{
    public AudioClip sonido = null;
    public float volumen = 1.0f;
    protected Transform posicion = null;

    // Use this for initialization
    void Start()
    {
        posicion = transform;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Vaus")
        {
            if (sonido) AudioSource.PlayClipAtPoint(sonido, posicion.position, volumen);
        }
    }
}

using UnityEngine;
using System.Collections;

public class sonChoque : MonoBehaviour {
    public AudioClip sonido = null;
    public float volumen = 1.0f;
    protected Transform posicion = null;

    // Use this for initialization
    void Start()
    {
        posicion = transform;
    }

    // Update is called once per frame
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "pelota")
        {
            if (sonido) AudioSource.PlayClipAtPoint(sonido, posicion.position, volumen);
        }
    }
}

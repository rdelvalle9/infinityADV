using UnityEngine;
using System.Collections;

public class Scono : MonoBehaviour
{
    public float X;
    public float Y;
    public float Z;
    public float segundosAcnt;
    private Rigidbody rb;
    private GM scriptGM;
    public bool vivo = false;
    SphereCollider sc;
    public GameObject explosion;

    // Use this for initialization
    void Start()
    {
        scriptGM = GameObject.Find("GM").GetComponent<GM>();//busca el objeto del script y lo asigna a la variable
        segundosAcnt = scriptGM.segundos + 3;
        vivo = true;
        sc = GetComponent<SphereCollider>();
        sc.enabled = false;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        entrada();
        desplazar();
    }

    // entrada = el bicho cae por gravedad desde la pared
    public void entrada()
    {
        if (transform.position.y < 10.88f && rb.useGravity == true)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
            rb.isKinematic = false;
            sc.enabled = true;
        }
        if (transform.position.y < -2.8f)
        {
            pared scriptPared = GameObject.Find("wall").GetComponent<pared>();
            scriptPared.cntBichos--;
            Destroy(gameObject);
        }
    }

    // desplazar = MUEVE EL BICHO
    public void desplazar()
    {
        if (scriptGM.segundos > segundosAcnt)
        {
            float x, y;
            rb.isKinematic = true;
            rb.isKinematic = false;
            x = Random.Range(-8f, 8f) * Time.deltaTime;
            y = Random.Range(-8f, 8f) * Time.deltaTime;
            Vector3 fuerza = new Vector3(x, y, 0);
            segundosAcnt = scriptGM.segundos + 2;
            rb.AddForce(fuerza);
        }
    }

    void OnCollisionEnter(Collision col)
    { //si choca con algo de eso el bicho hace una explosion
        if (col.gameObject.name == "Vaus" ||
            col.gameObject.name == "balaEnergia(Clone)" ||
            col.gameObject.name == "pelota"
            )
        {
            pared.esteObjeto.asignarTiempoParaCrearBicho();
            pared scriptPared = GameObject.Find("wall").GetComponent<pared>();
            scriptPared.cntBichos--;
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

}
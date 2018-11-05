using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jefe1 : MonoBehaviour {
    
    string nombreJefe = "Jefe1(Clone)";
    public AudioClip sonDispJefe = null;
    public float volumen = 1.0f;
    protected Transform posicion = null;
    float intialX = 0; //posicion inicial en X
    float InitialY = 9; //posicion inicial en Y
    float ax = 0;
    float ay = 9;
    int auxDirect;
    int lapso = 3;
    float Velocidad = 0.05f;
    float auxtime = 3;
    public Material blanco;
    public Material rojo;
    public Material marron;
    bool mover = true;
    Vector3 movi = new Vector3(); // Vector3 de Movimento
    float varCambiarColor;
    int directionMov = 1; //Direccion del moviento del Jefe, inicial 1 = Derecha  2 = Diagonal hacia abajo y al centro   3 = Diagonal hacia arriba y a la izquierda
    public GameObject bala;
    public GameObject explosion;
    float sDisparo;
    float sDisparoFinal;
    bool bDeDisparoFinal = false;
    bool bDeDisparo = false;
    bool bDispModo1 = false;
    int swColorTorus = 0;
    bool recibioDanio = false;
    public int modoDisparo = 1;
    public int cantDeVida = 10;
    float tiempoDestruirJefe;
    // Use this for initialization

    private void Awake()
    {
        
    }
    void Start () {
        Transform torus = GameObject.Find("Torus").GetComponent<Transform>();
        transform.position = new Vector3(intialX,InitialY,0);
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        Transform esferaJefe = GameObject.Find("esferaJefe").GetComponent<Transform>();
        esferaJefe.transform.Rotate(new Vector3(0,0,50f * Time.deltaTime));
        moverJefe();
        cambiarColores();
        destruirJefe();
    }

    void OnCollisionEnter(Collision col)
    {
        GM scriptGM = GameObject.Find("GM").GetComponent<GM>();
        if (col.gameObject.name == "pelota")
        {
            Vector3 posPelota = GameObject.Find("pelota").transform.position;
            Instantiate(explosion, posPelota, transform.rotation);
            varCambiarColor = scriptGM.segundos + .1f;
            recibioDanio = true;
            swColorTorus = 1;
            cantDeVida--;
            if (cantDeVida == 10)
            {
                modoDisparo++;
            }
        }
        TextMesh textMesh = GameObject.Find("txtFx").GetComponent<TextMesh>();
        //textMesh.text = vidaJefe.getVida().ToString();
    }

    void moverJefe()
    {
        movi = new Vector3(ax, ay, 0);
        GM scriptGM = GameObject.Find("GM").GetComponent<GM>();
        Rigidbody rbJefe = GameObject.Find(nombreJefe).GetComponent<Rigidbody>();
        Transform torus = GameObject.Find("Torus").GetComponent<Transform>();
        if (!mover)
        {
            directionMov = 4;
        }

        if (directionMov != 4)
        {
            torus.transform.Rotate(new Vector3(2f, 1.5f, 3f));
            if (scriptGM.segundos >= auxtime)
            {
                mover = false;
                auxtime += lapso;
                auxDirect = directionMov;
                torus.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            //el Switch Siempre comienza en 1
            switch (directionMov)
            {
                case 1:
                    transform.position = movi;
                    ax += Velocidad; //El aumento de X da el movimiento hacia la derecha hasta que llega al margen X==3
                    if (ax == 3 || ax > 3)
                    {
                        directionMov = 2;
                    }
                    break;
                case 2:
                    transform.position = movi;
                    ax -= Velocidad; // Se resta en X y se resta en Y para dar el movimiento diagonal
                    ay -= Velocidad; // pero el tringulo no es perfecto!! O.o
                    if (ay == 5 || ay < 5)
                    {
                        directionMov = 3;
                    }
                    break;
                case 3:
                    transform.position = movi;
                    ax -= Velocidad;
                    ay += Velocidad;
                    if (ax == -3 || ax < -3)
                    {
                        directionMov = 1;
                    }
                    break;
            }
            sDisparo = scriptGM.segundos;
            bDeDisparo = false;
            disparoFinal();
        }
        else
        {
            torus.transform.Rotate(new Vector3(0f, 0f, 2f));
            if (modoDisparo == 1)
            {
                dispararModo1();
            }
            else if (modoDisparo == 2)
            {
                dispararModo2();
            }
            if (scriptGM.segundos >= auxtime)
            {
                mover = true;
                auxtime += lapso;
                directionMov = auxDirect;
            }
        }
    }

    //cambiarColores =  cambia los colores cuando recibe danio o dispara
    void cambiarColores()
    {
        GM scriptGM = GameObject.Find("GM").GetComponent<GM>();
        Renderer colorTorus = GameObject.Find("cilindroJefe").GetComponent<Renderer>();
        if (recibioDanio)
        {
            if (scriptGM.segundos >= varCambiarColor)
            {
                recibioDanio = false;
                if (bDeDisparo && !bDispModo1)
                {
                    swColorTorus = 2;
                }
                else
                {
                    swColorTorus = 0;
                }
            }
        }
        switch (swColorTorus)
        {
            case 0:
                colorTorus.sharedMaterial = marron;
                break;
            case 1:
                colorTorus.sharedMaterial = blanco;
                break;
            case 2:
                colorTorus.sharedMaterial = rojo;
                break;
        }
    }

    void dispararModo1()
    {
        Renderer colorTorus = GameObject.Find("Torus").GetComponent<Renderer>();
        if (!bDeDisparo)
        {
            sDisparo += 1;
            bDeDisparo = true;
            swColorTorus = 2;
        }
        GM scriptGM = GameObject.Find("GM").GetComponent<GM>();
        if (scriptGM.segundos > sDisparo)
        {
            Vector3 posBala1 = transform.position;
            Vector3 posBala2 = transform.position;
            Vector3 posBala3 = transform.position;
            posBala1.x += -1f;
            posBala1.y += -2f;
            posBala2.x += 0f;
            posBala2.y += -4f;
            posBala3.x += 1f;
            posBala3.y += -2f;
            Instantiate(bala, posBala1, Quaternion.identity);
            Instantiate(bala, posBala2, Quaternion.identity);
            Instantiate(bala, posBala3, Quaternion.identity);
            sDisparo += scriptGM.segundos + 1;
            swColorTorus = 0;
            bDispModo1 = true;
            if (sonDispJefe) AudioSource.PlayClipAtPoint(sonDispJefe, transform.position, 2);
            if (sonDispJefe) AudioSource.PlayClipAtPoint(sonDispJefe, transform.position, 2);
        }
    }

    void dispararModo2()
    {
        Renderer colorTorus = GameObject.Find("Torus").GetComponent<Renderer>();
        if (!bDeDisparo)
        {
            sDisparo += 1;
            bDeDisparo = true;
            swColorTorus = 2;
        }
        GM scriptGM = GameObject.Find("GM").GetComponent<GM>();
        if (scriptGM.segundos > sDisparo)
        {
            Vector3 posBala1 = transform.position;
            Vector3 posBala2 = transform.position;
            Vector3 posBala3 = transform.position;
            Vector3 posBala4 = transform.position;
            Vector3 posBala5 = transform.position;
            Vector3 posBala6 = transform.position;
            posBala1.x += -1f;
            posBala2.x = posBala1.x + .5f;
            posBala3.x = posBala2.x + .5f;
            posBala4.x = posBala3.x + .5f;
            posBala5.x = posBala4.x + .5f;
            posBala6.x = posBala5.x + .5f;

            posBala1.y += -2f;
            posBala2.y += -2f;
            posBala3.y += -2f;
            posBala4.y += -2f;
            posBala5.y += -2f;
            posBala6.y += -2f;

            Instantiate(bala, posBala1, Quaternion.identity);
            Instantiate(bala, posBala2, Quaternion.identity);
            Instantiate(bala, posBala3, Quaternion.identity);
            Instantiate(bala, posBala4, Quaternion.identity);
            Instantiate(bala, posBala5, Quaternion.identity);
            Instantiate(bala, posBala6, Quaternion.identity);
            sDisparo += scriptGM.segundos + 1;
            swColorTorus = 0;
            bDispModo1 = true;
            if (sonDispJefe) AudioSource.PlayClipAtPoint(sonDispJefe, transform.position, 2);
            if (sonDispJefe) AudioSource.PlayClipAtPoint(sonDispJefe, transform.position, 2);
            if (sonDispJefe) AudioSource.PlayClipAtPoint(sonDispJefe, transform.position, 2);
            if (sonDispJefe) AudioSource.PlayClipAtPoint(sonDispJefe, transform.position, 2);
        }
    }

    void dispararModo3()
    {
        if (sonDispJefe) AudioSource.PlayClipAtPoint(sonDispJefe, transform.position, 2);
        if (sonDispJefe) AudioSource.PlayClipAtPoint(sonDispJefe, transform.position, 2);
        if (sonDispJefe) AudioSource.PlayClipAtPoint(sonDispJefe, transform.position, 2);
        Renderer colorTorus = GameObject.Find("Torus").GetComponent<Renderer>();
        swColorTorus = 2;
        GM scriptGM = GameObject.Find("GM").GetComponent<GM>();
        Vector3 posBala1 = transform.position;
        Vector3 posBala2 = transform.position;
        Vector3 posBala3 = transform.position;
        Vector3 posBala4 = transform.position;
        Vector3 posBala5 = transform.position;
        Vector3 posBala6 = transform.position;
        posBala1.x += -1f;
        posBala2.x = posBala1.x + .5f;
        posBala3.x = posBala2.x + .5f;
        posBala4.x = posBala3.x + .5f;
        posBala5.x = posBala4.x + .5f;
        posBala6.x = posBala5.x + .5f;

        posBala1.y += -2f;
        posBala2.y += -2f;
        posBala3.y += -2f;
        posBala4.y += -2f;
        posBala5.y += -2f;
        posBala6.y += -2f;

        //Instantiate(bala, posBala1, Quaternion.identity);
        Instantiate(bala, posBala2, Quaternion.identity);
        Instantiate(bala, posBala3, Quaternion.identity);
        Instantiate(bala, posBala4, Quaternion.identity);
        Instantiate(bala, posBala5, Quaternion.identity);
        //Instantiate(bala, posBala6, Quaternion.identity);
        //sDisparo += scriptGM.secondsCounter + 1;
        swColorTorus = 0;
        //bDispModo1 = true;
    }

    void disparoFinal()
    {
        GM scriptGM = GameObject.Find("GM").GetComponent<GM>();
        if (cantDeVida <= 5 && !bDeDisparoFinal)
        {
            sDisparoFinal = scriptGM.segundos + 1f;
            bDeDisparoFinal = true;
        }
        if(scriptGM.segundos >= sDisparoFinal && bDeDisparoFinal)
        {
            dispararModo3();
            bDeDisparoFinal = false;
        }
    }

    void destruirJefe()
    {
        
        if(cantDeVida == 0)
        {
            cantDeVida = -1;
            tiempoDestruirJefe = GM.esteObjeto.segundos + 1.2f;
        }
        else if(cantDeVida <= -1 && GM.esteObjeto.segundos < tiempoDestruirJefe)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            
        }
        else if(cantDeVida <= -1 && GM.esteObjeto.segundos > tiempoDestruirJefe)
        {
            Destroy(gameObject);
            GM.esteObjeto.mostrarPopUp(true);
        }
    }
}


        //if ( < .1f)//Izquierda
        //{
        //    rbJefe.isKinematic = true;
        //    rbJefe.isKinematic = false;
        //    rbJefe.AddForce(new Vector3(-40000, 0, 0));
        //    cuadranteMov = 4;
        //}
        //if(transform.position.x <= -3 && transform.position.y == 9)//abajo derecha
        //{
        //    rbJefe.isKinematic = true;
        //    rbJefe.isKinematic = false;
        //    rbJefe.AddForce(new Vector3(40000, -40000, 0));
        //}
        //if (transform.position.x >= -0.2 && transform.position.x <= 0.2
        //    && transform.position.y >= 6 && transform.position.y <= 6.3 
        //    && scriptGM.secondsCounter > .5f)//arriba derecha
        //{
        //    rbJefe.isKinematic = true;
        //    rbJefe.isKinematic = false;
        //    rbJefe.AddForce(new Vector3(40000, 40000, 0));
        //    cuadranteMov = 7;
        //}
        //if (transform.position.x >= 3 && transform.position.y >= 9 
        //    && scriptGM.secondsCounter > .5f)//izquierda
        //{
        //    rbJefe.isKinematic = true;
        //    rbJefe.isKinematic = false;
        //    rbJefe.AddForce(new Vector3(-40000, 0, 0));
        //    cuadranteMov = 4;
        //}
  
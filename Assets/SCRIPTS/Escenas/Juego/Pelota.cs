using UnityEngine;

public class Pelota : MonoBehaviour
{
    #region ~atributos~
    float velocInicialPelota = 10;
    private Rigidbody rb;
    public bool ballInPlay;
    static float xInic = 1.28f;
    static float yInic = 0.454f;
    static float zInic = 0;
    public Vector3 playerPos;
    public float volumen = 1.0f;
    protected Transform posicion = null;
    private GM scriptGM;
    private Svaus vaus;
    const float esperaLargada = 2.3f;
    float segLargada = esperaLargada;
    public bool esPelotaDeLanzamiento = false;
    public bool color = false;
    float cntSegTrabado;
    public float tiempoDeUpGrade = 0;
    public float tiempoDeColor = 0;
    float cntSegTotalmenteTrabado;
    float posXPelotaTrabada;
    float posYPelotaTrabada;
    public float fx = 0;
    float segImanLanzar;
    float distDeVaus; // la distancia q esta la pelota al tocar la vaus cuando es iman
    Vector3[] cuadrante = new Vector3[22];
    float varCantSegundosTrabado = 10;
    int nuevoColor = 0;
    #endregion

    void Start()
    {
        vaus = GameObject.Find("Vaus").GetComponent<Svaus>();//busca el objeto del script y lo asigna a la variable
        vaus.reiniciarFuerzaDeRebote();
        cargarVecCuadrantes();
        scriptGM = GameObject.Find("GM").GetComponent<GM>();//busca el objeto del script y lo asigna a la variable
        cntSegTrabado = scriptGM.segundos;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        vaus = GameObject.Find("Vaus").GetComponent<Svaus>();//busca el objeto del script y lo asigna a la variable
        posicion = transform;
        pelotaTrabada();
        pelotaTotalmenteTrabada();
        reiniciarFX();
        lanzarPelota();
        pelotaPerdida();
        //cambiarColor();

        if (ballInPlay == false)
        {
            seguirVaus();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        choqueConVaus(col);
        choqueConBicho(col);
        choqueConPelota(col);
    }

    /// <summary>
    /// le va dando cada vez mas fuerza a la vaus para acelerar la pelota cuando rebote
    /// </summary>
    void acelerarPelota()
    {
        //normal a los 2 += 1.5f
        vaus.fuerzaY += 2f;
        vaus.fuerzaX += 2f;
        //acelero la musica
        AudioSource musica = Sonido.esteObjeto.GetComponent<AudioSource>();
        musica.pitch += .001f;
    }

    /* lanzarPelota = lanza la pelota desde la vaus y tambien la lanza a los segundos
       si es q se tiene el iman de la vaus activado */
    void lanzarPelota()
    {
        if (!Svaus.esteObjeto.llegoAlaPos)
        {
            segLargada = scriptGM.segundos + esperaLargada;//al caer la ultima pelota reinicio el contador para q espera al largar la pelota
        }

        if (ballInPlay == false && vaus.bIman == false)//verifica si la pelota no esta en juego la lanza
        {
            if (scriptGM.segundos > segLargada && esPelotaDeLanzamiento != true)
            {
                transform.parent = null;
                ballInPlay = true;
                rb.isKinematic = false;
                rb.AddForce(50f, 200f, 0);
                cntSegTrabado = scriptGM.segundos + varCantSegundosTrabado;
                cntSegTotalmenteTrabado = scriptGM.segundos + 1f;
                posYPelotaTrabada = transform.position.y;
                posXPelotaTrabada = transform.position.x;
            }
        }
        else if (ballInPlay == false && vaus.bIman == true && fx != 0)//lanza la pelota en la posicion q habia chocado por el iman
        {
            if (scriptGM.segundos > segImanLanzar)
            {
                ballInPlay = true;
                rb.isKinematic = false;
                rb.AddForce(fx, vaus.fuerzaY, 0);
                cntSegTrabado = scriptGM.segundos + varCantSegundosTrabado;
                cntSegTotalmenteTrabado = scriptGM.segundos + 1f;
                posYPelotaTrabada = transform.position.y;
                posXPelotaTrabada = transform.position.x;
                fx = 0;
            }
        }
    }

    /// <summary>
    /// funcion de cuando se cae la pelota
    /// </summary>
    void pelotaPerdida()
    {
        if (transform.position.y < -4) //AL PERDER UNA PELOTA
        {
            if (scriptGM.cntPelotas != 1) //AL HABER MAS DE UNA PELOTA
            {
                scriptGM.cntPelotas--;   //resto una pelota a la variable GM
                Destroy(gameObject);
            }
            else //AL NO HABER PELOTAS EN JUEGO(se cayo la ultima)
            {
                scriptGM.contadorVidasPerdidas++;
                scriptGM.vidasTotalesPerdidas++;
                posicionarPelota();
                vaus.desactivarUpgrades();
                vaus.reiniciarFuerzaDeRebote();
                Pelota scriptPelota = GameObject.Find("pelota").GetComponent<Pelota>();
                scriptPelota.segLargada = scriptGM.segundos + esperaLargada;//al caer la ultima pelota reinicio el contador para q espere al largar la pelota
                scriptGM.vidas--;  //al caer la ultima pelota descuento una vida
                PlayerPrefs.SetInt("vidas", scriptGM.vidas);    //guardo en disco el valor actual de vidas para tener las mismas en el sig. nivel
                AudioSource camara = GameObject.Find("Main Camera").GetComponent<AudioSource>();
                camara.pitch = 1f; //la velocidad de la musica vuelve a la normalidad
                Instantiate(GM.esteObjeto.explosion, Svaus.esteObjeto.transform.position, Quaternion.identity);
                Sonido.esteObjeto.vausSonidoDeSuExplosion();
                Svaus.esteObjeto.iniciarVaus();
            }
        }
    }

    /// <summary>
    /// prepara la pelota para ser lanzada
    /// </summary>
    public void posicionarPelota()
    {
        ballInPlay = false;
        rb.isKinematic = true;
    }

    /// <summary>
    /// sigue a la vaus antes de ser lanzada
    /// </summary>
    public void seguirVaus()
    {
        if (vaus.bIman == false)
        {
            playerPos = new Vector3(0, 0, 0);
            playerPos = vaus.transform.position;
            playerPos.x += 0.42f;
            playerPos.y += 0.45f;
            transform.position = playerPos;
        }
        else
        {
            playerPos = new Vector3(0, 0, 0);
            playerPos = vaus.transform.position;
            playerPos.x += distDeVaus;
            playerPos.y += 0.45f;
            transform.position = playerPos;
        }
    }

    /// <summary>
    /// funcion de rebote cuando toca la vaus
    /// </summary>
    public void rebotarPelota()
    {
        AudioSource camara = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        cntSegTrabado = scriptGM.segundos + varCantSegundosTrabado;
        ballInPlay = false;
        rb.isKinematic = true;
        ballInPlay = true;
        rb.isKinematic = false;
        rb = GetComponent<Rigidbody>();
        float pelotaX = transform.position.x;
        float vausX = vaus.transform.position.x;

        acelerarPelota();

        if (pelotaX >= vausX)
        {
            float fx = 0;
            float porcentaje = 0;
            do
            {
                vausX += .1f;
                porcentaje += .1f;
                fx = vaus.fuerzaX * porcentaje;
            } while (pelotaX >= vausX);
            rb.AddForce(fx, vaus.fuerzaY, 0);
            camara.pitch += .002f; //acelerar la musica
        }
        else
        {
            float fx = 0;
            float porcentaje = 0;
            do
            {
                vausX -= .1f;
                porcentaje -= .1f;
                fx = vaus.fuerzaX * porcentaje;
            } while (pelotaX <= vausX);
            rb.AddForce(fx, vaus.fuerzaY, 0);
            camara.pitch += .002f;
        }
    }

    /// <summary>
    /// cuando toca a la vaus iman pone un contador para q rebote despues
    /// </summary>
    public void tocoVausIman()
    {
        segImanLanzar = scriptGM.segundos + 1f;
        AudioSource camara = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        cntSegTrabado = scriptGM.segundos + 7;
        ballInPlay = false;
        rb.isKinematic = true;
        rb = GetComponent<Rigidbody>();
        float pelotaX = transform.position.x;
        float vausX = vaus.transform.position.x;

        acelerarPelota();

        distDeVaus = pelotaX - vausX;
      
        if (pelotaX >= vausX)
        {
            float porcentaje = 0;
            do
            {
                vausX += .1f;
                porcentaje += .1f;
                fx = vaus.fuerzaX * porcentaje;
            } while (pelotaX >= vausX);
        }
        else
        {
            float porcentaje = 0;
            do
            {
                vausX -= .1f;
                porcentaje -= .1f;
                fx = vaus.fuerzaX * porcentaje;
            } while (pelotaX <= vausX);
        }
    }

    /// <summary>
    /// si la pelota esta sin tocar la vaus por 7 segundos le da fuerza aleatoria para destrabarla
    /// </summary>
    public void pelotaTrabada()
    {
        int numeroRandom;
        if (ballInPlay == true)
        {
            if (scriptGM.segundos > cntSegTrabado && transform.position.y > 0
                && vaus.fuerzaX >= 150)//(antes 220)si la pelota se traba y no paso tanto tiempo para q llegue a la fuerza de 220 no va a entrar como me paso recien 28/07/18
            {
                cntSegTrabado = scriptGM.segundos + 7f;
                rb.isKinematic = true;
                rb.isKinematic = false;
                numeroRandom = Random.Range(0, 22);
                rb.AddForce(cuadrante[numeroRandom]);
                Debug.Log("Pelota trabada 1");
            }
        }
    }

    /// <summary>
    /// aveces la pelota choca y queda estatica en el lugar esta funcion hace q se mueva enseguida
    /// </summary>
    public void pelotaTotalmenteTrabada()//pelota loca upgrade
    {
        int numeroRandom;
        if (ballInPlay == true)
        {
            if (scriptGM.segundos > cntSegTotalmenteTrabado)
            {
                cntSegTotalmenteTrabado = scriptGM.segundos + .1f;
                if (posYPelotaTrabada - transform.position.y == 0 &&
                    posXPelotaTrabada - transform.position.x == 0)
                {
                    float tr = transform.position.y;
                    float res = posYPelotaTrabada - tr;
                    rb.isKinematic = true;
                    rb.isKinematic = false;
                    numeroRandom = Random.Range(0, 22);
                    rb.AddForce(cuadrante[numeroRandom]);
                    Debug.Log("Pelota totalmente trabada ");
                }
                else
                {
                    posYPelotaTrabada = transform.position.y;
                    posXPelotaTrabada = transform.position.x;
                }
            }
        }
    }

    /// <summary>
    /// los movimientos q hace la pelota al chocar con un bicho
    /// </summary>
    void cargarVecCuadrantes()
    {
        for (int i = 0; i < 22; i++)
        {
            cuadrante[i] = new Vector3();
        }
        float f = 50;
        cuadrante[0] = new Vector3(150f -f, 200f - f, 0);
        cuadrante[1] = new Vector3(175f - f, 200f - f, 0);
        cuadrante[2] = new Vector3(200f - f, 200f - f, 0);
        cuadrante[3] = new Vector3(225 - f, 200f - f, 0);
        cuadrante[4] = new Vector3(250f - f, 200f - f, 0);
        cuadrante[5] = new Vector3(0f, 200f - f, 0);
        cuadrante[6] = new Vector3(-150f + f, 200f - f, 0);
        cuadrante[7] = new Vector3(-175f + f, 200f - f, 0);
        cuadrante[8] = new Vector3(-200f + f, 200f - f, 0);
        cuadrante[9] = new Vector3(-225f + f, 200f - f, 0);
        cuadrante[10] = new Vector3(-250f + f, 200f - f, 0);
        /////////////////////////////////////////////
        cuadrante[11] = new Vector3(150f - f, -200f + f, 0);
        cuadrante[12] = new Vector3(175f - f, -200f + f, 0);
        cuadrante[13] = new Vector3(200f - f, -200f + f, 0);
        cuadrante[14] = new Vector3(225f - f, -200f + f, 0);
        cuadrante[15] = new Vector3(250f - f, -200f + f, 0);
        cuadrante[16] = new Vector3(0f, -200f + f, 0);
        cuadrante[17] = new Vector3(-150f + f, -200f + f, 0);
        cuadrante[18] = new Vector3(-175f + f, -200f + f, 0);
        cuadrante[19] = new Vector3(-200f + f, -200f + f, 0);
        cuadrante[20] = new Vector3(-225f + f, -200f + f, 0);
        cuadrante[21] = new Vector3(-250f + f, -200f + f, 0);
    }

    /// <summary>
    /// para solucion el problema q aveces funciona mal el iman
    /// </summary>
    void reiniciarFX()
    {
        if (vaus.bIman == false)
        {
            fx = 0;
        }
    }

    private void choqueConVaus(Collision col)
    {
        if (col.gameObject.name == "Vaus" && transform.position.y > 0)
        {
            if (vaus.bIman && fx == 0)
            {
                tocoVausIman();
            }
            else
            {
                rebotarPelota();
            }
        }
    }

    private void choqueConBicho(Collision col)
    {
        if (col.gameObject.name == "bicho")
        {
            rb.isKinematic = true;
            rb.isKinematic = false;
            int numeroRandom = Random.Range(0, 22);
            rb.AddForce(cuadrante[numeroRandom]);
            cntSegTrabado = scriptGM.segundos + varCantSegundosTrabado;
        }
    }

    private void choqueConPelota(Collision col)
    {
        if (col.gameObject.name == "pelota")
        {
            cntSegTrabado = scriptGM.segundos + varCantSegundosTrabado;
        }
    }
    //para el upgrade q falta de los colores de la pelota
    /*
    void cambiarColor()
    {
        if (color && GM.esteObjeto.segundos < tiempoDeUpGrade)
        {
                if (GM.esteObjeto.segundos > tiempoDeColor)
                {
                    nuevoColor++;
                    if (nuevoColor > 13) nuevoColor = 0;
                    tiempoDeColor += 2;
                }

                switch (nuevoColor)
                {
                    case 0:
                        {
                            Renderer color = GameObject.Find("pelota").GetComponent<Renderer>();
                            color.material = crearNiveles.esteObjeto.azul;
                        }
                        break;
                    case 1:
                        {
                            Renderer color = GameObject.Find("pelota").GetComponent<Renderer>();
                            color.material = crearNiveles.esteObjeto.amarillo;
                        }
                        break;
                    case 2:
                        {
                            Renderer color = GameObject.Find("pelota").GetComponent<Renderer>();
                            color.material = crearNiveles.esteObjeto.blanco;
                        }
                        break;
                    case 3:
                        {
                            Renderer color = GameObject.Find("pelota").GetComponent<Renderer>();
                            color.material = crearNiveles.esteObjeto.celeste;
                        }
                        break;
                    case 4:
                        {
                            Renderer color = GameObject.Find("pelota").GetComponent<Renderer>();
                            color.material = crearNiveles.esteObjeto.gris;
                        }
                        break;
                    case 5:
                        {
                            Renderer color = GameObject.Find("pelota").GetComponent<Renderer>();
                            color.material = crearNiveles.esteObjeto.marron;
                        }
                        break;
                    case 6:
                        {
                            Renderer color = GameObject.Find("pelota").GetComponent<Renderer>();
                            color.material = crearNiveles.esteObjeto.naranja;
                        }
                        break;
                    case 7:
                        {
                            Renderer color = GameObject.Find("pelota").GetComponent<Renderer>();
                            color.material = crearNiveles.esteObjeto.negro;
                        }
                        break;
                    case 8:
                        {
                            Renderer color = GameObject.Find("pelota").GetComponent<Renderer>();
                            color.material = crearNiveles.esteObjeto.rojo;
                        }
                        break;
                    case 9:
                        {
                            Renderer color = GameObject.Find("pelota").GetComponent<Renderer>();
                            color.material = crearNiveles.esteObjeto.rosa;
                        }
                        break;
                    case 10:
                        {
                            Renderer color = GameObject.Find("pelota").GetComponent<Renderer>();
                            color.material = crearNiveles.esteObjeto.verde;
                        }
                        break;
                    case 11:
                        {
                            Renderer color = GameObject.Find("pelota").GetComponent<Renderer>();
                            color.material = crearNiveles.esteObjeto.verdeOscuro;
                        }
                        break;
                    case 12:
                        {
                            Renderer color = GameObject.Find("pelota").GetComponent<Renderer>();
                            color.material = crearNiveles.esteObjeto.violeta;
                        }
                        break;
                }
        }

        if (GM.esteObjeto.segundos > tiempoDeUpGrade && color)
        {
            color = false;
            Renderer colorOriginal = GameObject.Find("pelota").GetComponent<Renderer>();
            colorOriginal.material = crearNiveles.esteObjeto.blanco;
        }
    }*/
}
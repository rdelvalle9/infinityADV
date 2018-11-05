using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class crearNiveles : MonoBehaviour
{
    int nivel = 1;
    posBloque[] vPosBloques;
    public Bloque[] bloque;
    public GameObject objBloque;
    public Material rojo;
    public Material verde;
    public Material azul;
    public Material amarillo;
    public Material naranja;
    public Material marron;
    public Material verdeOscuro;
    public Material celeste;
    public Material rosa;
    public Material violeta;
    public Material blanco;
    public Material gris;
    public Material negro;
    public AudioClip sonidoBloqueComun = null;
    public AudioClip sonidoBloqueGris = null;
    public AudioClip sonidoBloqueNegro = null;
    public GameObject expRojo;
    public GameObject expVerde;
    public GameObject expAzul;
    public GameObject expAmarillo;
    public GameObject expNaranja;
    public GameObject expMarron;
    public GameObject expVerdeOscuro;
    public GameObject expCeleste;
    public GameObject expRosa;
    public GameObject expVioleta;
    public GameObject expBlanco;
    public GameObject expGris;
    public Bloque[] bloquesRestantes;
    public static crearNiveles esteObjeto = null;
    int capsulasTotalesExistentes;

    [Serializable]
    public class Bloque
    {
        int Pos;
        int Color;

        public int pos
        {
            get
            {
                return Pos;
            }
            set
            {
                Pos = value;
            }
        }

        public int color
        {
            get
            {
                return Color;
            }
            set
            {
                Color = value;
            }
        }
    }

    class posBloque
    {
        float X;
        float Y;

        public float y
        {
            get
            {
                return Y;
            }
            set
            {
                Y = value;
            }
        }

        public float x
        {
            get
            {
                return X;
            }
            set
            {
                X = value;
            }
        }


    }

    void Start()
    {
        capsulasTotalesExistentes = contCapsulas.esteObjeto.capsulasTotales;
        cargarVectores();

        if (esteObjeto== null)
        {
            esteObjeto= this;
        }
        else if (esteObjeto!= this)
        {
            Destroy(gameObject);
        }
    }

    
    // cargarVectores = carga las posiciones donde van a ir los bloques al cargar el nivel
    void cargarVectores()
    {
        float X = -4f;
        float Y = 9.8f;
        int cFilas = 0;
        bloque = new Bloque[220];
        vPosBloques = new posBloque[220];
        for (int i = 0; i < 220; i++)
        {
            bloque[i] = new Bloque(); // lo lleno con objetos 
            vPosBloques[i] = new posBloque();// lo lleno con objetos
            vPosBloques[i].x = X;
            vPosBloques[i].y = Y;

            X += 0.8f;
            cFilas++;
            if (cFilas == 11)
            {
                Y -= 0.4f;
                X = -4f;
                cFilas = 0;
            }
        }
    }

    // cargarCeros = carga ceros para llenar los espacios faltantes
    string cargarCeros(int num)
    {
        string st = num.ToString();
        if (st.Length == 1)
        {
            st = "00" + num;
        }
        else if (st.Length == 2)
        {
            st = "0" + num;
        }
        else
        {
            return st;
        }
        return st;
    }

    // guardarBloque = cada vez q se toca un bloque nuevo este lo guarda al vector 
    public void guardarBloque(string nomBloque, int color)
    {
        for (int i = 0; i < 220; i++)
        {
            if (nomBloque == "Button (" + i + ")")
            {
                botonCrear bt = GameObject.Find(nomBloque).GetComponent<botonCrear>();
                bloque[i].pos = (byte)i;
                bloque[i].color = (byte)bt.nColor;
            }
        }
    }

    // guardarNivelCreado = guarda el nivel creado en formato .bytes
    public void guardarNivelCreado()
    {
        int n = PlayerPrefs.GetInt("nivelCreado");
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(Application.persistentDataPath + "/nivelNuevo" + n + ".bytes", FileMode.Create, FileAccess.Write, FileShare.None);
        n++;
        string nivelSTR = "";
        PlayerPrefs.SetInt("nivelCreado", n);
        int pos;
        int color;
        for (int i = 0; i < 220; i++)
        {
            pos = bloque[i].pos;
            nivelSTR += cargarCeros(pos) + ",";
            color = bloque[i].color;
            nivelSTR += cargarCeros(color) + "|";
        }
        formatter.Serialize(stream, nivelSTR);
        stream.Close();
    }

    // cargarParticulas = CARGO LAS CAPSULAS 
    public void cargarParticulas(string nombre, posYcolor scriptPosYcolor)
    {
        bloque Bloque = GameObject.Find(nombre + "(Clone)").GetComponent<bloque>();
        switch (scriptPosYcolor.color)
        {
            case 1:
                Bloque.explosion = expRojo;
                break;
            case 2:
                Bloque.explosion = expVerde;
                break;
            case 3:
                Bloque.explosion = expAzul;
                break;
            case 4:
                Bloque.explosion = expAmarillo;
                break;
            case 5:
                Bloque.explosion = expNaranja;
                break;
            case 6:
                Bloque.explosion = expMarron;
                break;
            case 7:
                Bloque.explosion = expVerdeOscuro;
                break;
            case 8:
                Bloque.explosion = expCeleste;
                break;
            case 9:
                Bloque.explosion = expRosa;
                break;
            case 10:
                Bloque.explosion = expVioleta;
                break;
            case 11:
                Bloque.explosion = expBlanco;
                break;
            case 12:
                //Bloque.explosion = expGris;
                break;
                //case 13:
                //    rend.sharedMaterial = negro;
                //    break;
        }

    }

    // cargarCristales = mete los cristales a los bloques
    void cargarCristales(Renderer bloqueDeJuego)
    {
        int numeroRandom = UnityEngine.Random.Range(0, 2);
        if (numeroRandom == 1)
        {
           // bloqueDeJuego.gameObject.AddComponent<CrearCristal>();
        }
    }

    // cargarCapsulas = CARGO LAS CAPSULAS Y LOS SCRIPTS A LOS BLOQUES
    public void cargarCapsulas(string nombre)
    {
        int numeroRandom = UnityEngine.Random.Range(1, 25);

        Renderer bloqueDeJuego = GameObject.Find(nombre + "(Clone)").GetComponent<Renderer>();
        //si el bloque es gris le carga el script de bloque gris
        if (bloqueDeJuego.sharedMaterial == gris)
        {
            bloqueDeJuego.gameObject.AddComponent<bloqueGris>();
            bloqueGris Bloque = GameObject.Find(nombre + "(Clone)").GetComponent<bloqueGris>();
            Bloque.sonido = sonidoBloqueGris;
            Bloque.explosion = expGris;
        }
        //si el bloque no es negro le carga el script de bloque comun
        else if (bloqueDeJuego.sharedMaterial != negro)
        {
            bloqueDeJuego.gameObject.AddComponent<bloque>();
            bloque Bloque = GameObject.Find(nombre + "(Clone)").GetComponent<bloque>();
            Bloque.sonido = sonidoBloqueComun;
            cargarCristales(bloqueDeJuego);
        }
        //si el bloque es negro 
        else if (bloqueDeJuego.sharedMaterial == negro)
        {
            bloqueDeJuego.gameObject.AddComponent<BloqueNegro>();
            BloqueNegro Bloque = GameObject.Find(nombre + "(Clone)").GetComponent<BloqueNegro>();
        }

        //aca es donde se le agrega al bloque la capsula q le toco
        if (bloqueDeJuego.sharedMaterial != gris && bloqueDeJuego.sharedMaterial != negro)
        {
            if (numeroRandom <= capsulasTotalesExistentes)
            {
                bloqueDeJuego.gameObject.AddComponent<CrearCapsula>().numeroCapsula = numeroRandom;
            }
            
            //bloqueDeJuego.gameObject.GetComponent<CrearCapsula>().numeroCapsula = 1;

            //switch (numeroRandom)
            //{
            //    case 0:
            //        bloqueDeJuego.gameObject.AddComponent<crearCapLaser>();
            //        break;
            //    case 1:
            //        bloqueDeJuego.gameObject.AddComponent<crearCapLaserDificil>();
            //        break;
            //    case 2:
            //        bloqueDeJuego.gameObject.AddComponent<crearCapPelotas>();
            //        break;
            //    case 3:
            //        bloqueDeJuego.gameObject.AddComponent<crearCapExpandir>();
            //        break;
            //    case 4:
            //        bloqueDeJuego.gameObject.AddComponent<crearCapVida>();
            //        break;
            //    case 5:
            //        bloqueDeJuego.gameObject.AddComponent<crearCapIman>();
            //        break;
            //    case 6:
            //        bloqueDeJuego.gameObject.AddComponent<crearCapIA>();
            //        break;
            //    case 7:
            //        //bloqueDeJuego.gameObject.AddComponent<crearCapColor>();
            //        break;
            //    case 8:
            //        bloqueDeJuego.gameObject.AddComponent<crearCapBloque>();
            //        break;
            //    case 9:
            //        //bloqueDeJuego.gameObject.AddComponent<crearCapSuperBall>();
            //        break;
            //    case 10:
            //        bloqueDeJuego.gameObject.AddComponent<crearCapAmetralladora>();
            //        break;
            //}
        }
    }

    // cargar = carga el nivel lo lee del disco(los bloques al juego)
    public void cargar()
    {
        //PlayerPrefs.SetInt("nivel",16);
        int n = PlayerPrefs.GetInt("nivel");
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension("nivel" + n);
        TextAsset textAsset = Resources.Load("niveles/" + fileNameWithoutExtension) as TextAsset;
        Stream stream = new MemoryStream(textAsset.bytes);
        BinaryFormatter formatter = new BinaryFormatter();
        string nivelString = (string)formatter.Deserialize(stream);
        int contadorBloques = 0;
        char[] nivelChar = nivelString.ToCharArray();
        int pos;
        int color;
        string[] posChar = new string[3];
        string[] colorChar = new string[3];
        int e = 0;
        while (e < nivelChar.Length)
        {
            posChar[0] = nivelChar[e].ToString();
            posChar[1] = nivelChar[++e].ToString();
            posChar[2] = nivelChar[++e].ToString();
            colorChar[0] = nivelChar[e += 2].ToString();
            colorChar[1] = nivelChar[++e].ToString();
            colorChar[2] = nivelChar[++e].ToString();
            pos = int.Parse(posChar[0].ToString() + posChar[1].ToString() + posChar[2].ToString());
            color = int.Parse(colorChar[0].ToString() + colorChar[1].ToString() + colorChar[2].ToString());
            e += 2;

            //si no es 0 quiere decir q hay un bloque en la posicion
            if (color != 0)
            {
                Vector3 posBloque = new Vector3(vPosBloques[pos].x, vPosBloques[pos].y, 0);
                Renderer rend = objBloque.GetComponent<Renderer>();
                switch (color)
                {
                    case 1:
                        rend.sharedMaterial = rojo;
                        break;
                    case 2:
                        rend.sharedMaterial = verde;
                        break;
                    case 3:
                        rend.sharedMaterial = azul;
                        break;
                    case 4:
                        rend.sharedMaterial = amarillo;
                        break;
                    case 5:
                        rend.sharedMaterial = naranja;
                        break;
                    case 6:
                        rend.sharedMaterial = marron;
                        break;
                    case 7:
                        rend.sharedMaterial = verdeOscuro;
                        break;
                    case 8:
                        rend.sharedMaterial = celeste;
                        break;
                    case 9:
                        rend.sharedMaterial = rosa;
                        break;
                    case 10:
                        rend.sharedMaterial = violeta;
                        break;
                    case 11:
                        rend.sharedMaterial = blanco;
                        break;
                    case 12:
                        rend.sharedMaterial = gris;
                        break;
                    case 13:
                        rend.sharedMaterial = negro;
                        break;
                }
                objBloque.name = pos.ToString();
                Instantiate(objBloque, posBloque, Quaternion.identity);       //CREO EL BLOQUE 
                Renderer bloqueDeJuego = GameObject.Find(pos + "(Clone)").GetComponent<Renderer>();       //BUSCO EL BLOQUE CREADO PARA 
                bloqueDeJuego.gameObject.AddComponent<posYcolor>();                                              //AGREGARLE EL COMPONENTE posYcolor
                posYcolor scriptPosYcolor = GameObject.Find(pos + "(Clone)").GetComponent<posYcolor>(); //LO BUSCO Y ASIGNO LOS VALORES
                scriptPosYcolor.pos = pos;
                scriptPosYcolor.color = color;
                cargarCapsulas(objBloque.name);      //CARGO LAS CAPSULAS DEL NIVEL
                cargarParticulas(objBloque.name, scriptPosYcolor);
                if (color != 13)
                    contadorBloques++;      //SI EL COLOR ES DISTINTO DE NEGRO LO CUENTA COMO BLOQUE
            }
        }
        stream.Close();
        GM scriptGM = GameObject.Find("GM").GetComponent<GM>();
        scriptGM.bloques = contadorBloques;
    }

    //continuarJuego = carga el juego del archivo slot.bin de la carpeta saves
    public void continuarJuego()
    {
        IFormatter formatter = new BinaryFormatter();
        int n = PlayerPrefs.GetInt("continuarNivel");
        Stream stream = new FileStream(Application.persistentDataPath + "/saves/slot1.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
        string nivelString = (string)formatter.Deserialize(stream);
        int contadorBloques = 0;
        char[] nivelChar = nivelString.ToCharArray();
        int pos;
        int color;
        string[] posChar = new string[3];
        string[] colorChar = new string[3];
        int e = 0;
        while (e < nivelChar.Length)
        {
            posChar[0] = nivelChar[e].ToString();
            posChar[1] = nivelChar[++e].ToString();
            posChar[2] = nivelChar[++e].ToString();
            colorChar[0] = nivelChar[e += 2].ToString();
            colorChar[1] = nivelChar[++e].ToString();
            colorChar[2] = nivelChar[++e].ToString();
            pos = int.Parse(posChar[0].ToString() + posChar[1].ToString() + posChar[2].ToString());
            color = int.Parse(colorChar[0].ToString() + colorChar[1].ToString() + colorChar[2].ToString());
            e += 2;

            //si no es 0 quiere decir q hay un bloque en la posicion
            if (color != 0)
            {
                Vector3 posBloque = new Vector3(vPosBloques[pos].x, vPosBloques[pos].y, 0);
                Renderer rend = objBloque.GetComponent<Renderer>();
                switch (color)
                {
                    case 1:
                        rend.sharedMaterial = rojo;
                        break;
                    case 2:
                        rend.sharedMaterial = verde;
                        break;
                    case 3:
                        rend.sharedMaterial = azul;
                        break;
                    case 4:
                        rend.sharedMaterial = amarillo;
                        break;
                    case 5:
                        rend.sharedMaterial = naranja;
                        break;
                    case 6:
                        rend.sharedMaterial = marron;
                        break;
                    case 7:
                        rend.sharedMaterial = verdeOscuro;
                        break;
                    case 8:
                        rend.sharedMaterial = celeste;
                        break;
                    case 9:
                        rend.sharedMaterial = rosa;
                        break;
                    case 10:
                        rend.sharedMaterial = violeta;
                        break;
                    case 11:
                        rend.sharedMaterial = blanco;
                        break;
                    case 12:
                        rend.sharedMaterial = gris;
                        break;
                    case 13:
                        rend.sharedMaterial = negro;
                        break;
                }
                objBloque.name = pos.ToString();
                Instantiate(objBloque, posBloque, Quaternion.identity);       //CREO EL BLOQUE 
                Renderer bloqueDeJuego = GameObject.Find(pos + "(Clone)").GetComponent<Renderer>();       //BUSCO EL BLOQUE CREADO PARA 
                bloqueDeJuego.gameObject.AddComponent<posYcolor>();                                              //AGREGARLE EL COMPONENTE posYcolor
                posYcolor scriptPosYcolor = GameObject.Find(pos + "(Clone)").GetComponent<posYcolor>(); //LO BUSCO Y ASIGNO LOS VALORES
                scriptPosYcolor.pos = pos;
                scriptPosYcolor.color = color;
                cargarCapsulas(objBloque.name);      //CARGO LAS CAPSULAS DEL NIVEL
                cargarParticulas(objBloque.name, scriptPosYcolor);
                if (color != 13)
                    contadorBloques++;      //SI EL COLOR ES DISTINTO DE NEGRO LO CUENTA COMO BLOQUE
            }
        }
        stream.Close();
        GM scriptGM = GameObject.Find("GM").GetComponent<GM>();
        scriptGM.bloques = contadorBloques;
    }

    //continuarJuego = carga el juego del archivo slot.bin de la carpeta saves
    public void continuarJuego2()
    {
        IFormatter formatter = new BinaryFormatter();
        int n = PlayerPrefs.GetInt("continuarNivel");
        Stream stream = new FileStream(Application.persistentDataPath + "/saves/slot1.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
        int contadorBloques = 0;
        for (byte i = 0; i < 220; i++)
        {
            bloque[i] = (Bloque)formatter.Deserialize(stream);
            if (bloque[i].color != 0)
            {
                Vector3 pos = new Vector3(vPosBloques[bloque[i].pos].x, vPosBloques[bloque[i].pos].y, 0);
                Renderer rend = objBloque.GetComponent<Renderer>();
                switch (bloque[i].color)
                {
                    case 1:
                        rend.sharedMaterial = rojo;
                        break;
                    case 2:
                        rend.sharedMaterial = verde;
                        break;
                    case 3:
                        rend.sharedMaterial = azul;
                        break;
                    case 4:
                        rend.sharedMaterial = amarillo;
                        break;
                    case 5:
                        rend.sharedMaterial = naranja;
                        break;
                    case 6:
                        rend.sharedMaterial = marron;
                        break;
                    case 7:
                        rend.sharedMaterial = verdeOscuro;
                        break;
                    case 8:
                        rend.sharedMaterial = celeste;
                        break;
                    case 9:
                        rend.sharedMaterial = rosa;
                        break;
                    case 10:
                        rend.sharedMaterial = violeta;
                        break;
                    case 11:
                        rend.sharedMaterial = blanco;
                        break;
                    case 12:
                        rend.sharedMaterial = gris;
                        break;
                    case 13:
                        rend.sharedMaterial = negro;
                        break;
                }
                objBloque.name = i.ToString();
                Instantiate(objBloque, pos, Quaternion.identity);       //CREO EL BLOQUE 
                Renderer bloqueDeJuego = GameObject.Find(i.ToString() + "(Clone)").GetComponent<Renderer>();       //BUSCO EL BLOQUE CREADO PARA 
                bloqueDeJuego.gameObject.AddComponent<posYcolor>();                                              //AGREGARLE EL COMPONENTE posYcolor
                posYcolor scriptPosYcolor = GameObject.Find(i.ToString() + "(Clone)").GetComponent<posYcolor>(); //LO BUSCO Y ASIGNO LOS VALORES
                scriptPosYcolor.pos = i;
                scriptPosYcolor.color = bloque[i].color;
                cargarCapsulas(objBloque.name);      //CARGO LAS CAPSULAS DEL NIVEL
                cargarParticulas(objBloque.name, scriptPosYcolor);
                if (bloque[i].color != 13)
                    contadorBloques++;      //SI EL COLOR ES DISTINTO DE NEGRO LO CUENTA COMO BLOQUE
            }
        }
        stream.Close();
        GM scriptGM = GameObject.Find("GM").GetComponent<GM>();
        scriptGM.bloques = contadorBloques;
    }

    // guardarJuego = guarda las posiciones y los colores de los bloques
    public bool guardarJuego()
    {
        string sGuardado="";        
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(Application.persistentDataPath + "/saves/slot1.bin", FileMode.Create, FileAccess.Write, FileShare.None);
        int pos;
        int color;
        for (int i = 0; i < 220; i++)
        {
            try    //BUSCO LOS BLOQUES QUE HAYAN QUEDADO EN EL JUEGO
            {
                posYcolor scriptPosYcolor = GameObject.Find(i.ToString() + "(Clone)").GetComponent<posYcolor>();
                pos = scriptPosYcolor.pos;
                color = scriptPosYcolor.color;
                sGuardado += cargarCeros(pos) + ",";
                sGuardado += cargarCeros(color) + "|";
            }
            catch
            {
                sGuardado += cargarCeros(i) + ",";
                sGuardado += cargarCeros(0) + "|";
            }
        }
        try   
        {
            formatter.Serialize(stream, sGuardado);
            stream.Close();
            return true;
        }
        catch
        {
            return false;
        }
    }

    GameObject crearObjetoVacio(int i)
    {
        GameObject contenedorNivel = new GameObject();
        contenedorNivel.name = "cont"+i;
        return contenedorNivel;
    }
    //public void cargarNiveles()
    //{
    //    cargarVectores();
    //    float x = -4.24f; float y = 11.57f;
    //    int galaxia = PlayerPrefs.GetInt("nivelByGalaxia");
    //    for (int i = 1 * galaxia; i <= 15 * galaxia; i++)
    //    {
    //        GameObject contenedorNivel = crearObjetoVacio(i);
    //        contenedorNivel.transform.position = new Vector3(x, y);
    //        mostrarNivel(i, contenedorNivel);
    //        if (i % 4 == 0)
    //        {
    //            x = -4.24f; y -= 2.94f;
    //        }
    //        else
    //        {
    //            x += 2.84f;
    //        }
    //    }
    //}
    public void cargarNiveles()
    {
        cargarVectores();
        float x = -4.24f; float y = 11.57f;
        int galaxia = PlayerPrefs.GetInt("nivelByGalaxia");
        int cntPos = 1;
        for(int i=1 + (15 * galaxia); i<=15 + (15 * galaxia); i++)
        {
            GameObject contenedorNivel = crearObjetoVacio(cntPos);            
            contenedorNivel.transform.position = new Vector3(x, y);
            mostrarNivel(i,contenedorNivel);
            if (cntPos % 4 == 0)
            {
                x = -4.24f; y -= 2.94f;
            }
            else
            {
                x += 2.84f;
            }
            cntPos++;
        }
    }

    public void mostrarNivel(int nivel, GameObject contenedorNivel)
    {
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension("nivel" + nivel );
        TextAsset textAsset = Resources.Load("niveles/" + fileNameWithoutExtension) as TextAsset;
        Stream stream = new MemoryStream(textAsset.bytes);
        BinaryFormatter formatter = new BinaryFormatter();
        string nivelString = (string)formatter.Deserialize(stream);
        char[] nivelChar = nivelString.ToCharArray();
        int pos;
        int color;
        string[] posChar = new string[3];
        string[] colorChar = new string[3];
        int e = 0;
        while (e < nivelChar.Length)
        {
            posChar[0] = nivelChar[e].ToString();
            posChar[1] = nivelChar[++e].ToString();
            posChar[2] = nivelChar[++e].ToString();
            colorChar[0] = nivelChar[e += 2].ToString();
            colorChar[1] = nivelChar[++e].ToString();
            colorChar[2] = nivelChar[++e].ToString();
            pos = int.Parse(posChar[0].ToString() + posChar[1].ToString() + posChar[2].ToString());
            color = int.Parse(colorChar[0].ToString() + colorChar[1].ToString() + colorChar[2].ToString());
            e += 2;

            //si no es 0 quiere decir q hay un bloque en la posicion
            if (color != 0)
            {
                Vector3 posBloque = new Vector3(vPosBloques[pos].x, vPosBloques[pos].y, 0);
                Renderer rend = objBloque.GetComponent<Renderer>();
                switch (color)
                {
                    case 1:
                        rend.sharedMaterial = rojo;
                        break;
                    case 2:
                        rend.sharedMaterial = verde;
                        break;
                    case 3:
                        rend.sharedMaterial = azul;
                        break;
                    case 4:
                        rend.sharedMaterial = amarillo;
                        break;
                    case 5:
                        rend.sharedMaterial = naranja;
                        break;
                    case 6:
                        rend.sharedMaterial = marron;
                        break;
                    case 7:
                        rend.sharedMaterial = verdeOscuro;
                        break;
                    case 8:
                        rend.sharedMaterial = celeste;
                        break;
                    case 9:
                        rend.sharedMaterial = rosa;
                        break;
                    case 10:
                        rend.sharedMaterial = violeta;
                        break;
                    case 11:
                        rend.sharedMaterial = blanco;
                        break;
                    case 12:
                        rend.sharedMaterial = gris;
                        break;
                    case 13:
                        rend.sharedMaterial = negro;
                        break;
                }
                objBloque.name = pos.ToString();
                var newNivel = Instantiate(objBloque, posBloque, Quaternion.identity);       //CREO EL BLOQUE 
                newNivel.transform.parent = contenedorNivel.transform;
            }
        }
        stream.Close();
        contenedorNivel.GetComponent<Transform>().transform.localScale = new Vector3(.215f, .235f, .235f);
    }

    // 
    public void guardarNumNivelBtn(string nomBloque)
    {
        for (int i = 0; i < 16; i++)
        {
            if (nomBloque == "Button (" + i + ")")
            {
                botonCrear bt = GameObject.Find(nomBloque).GetComponent<botonCrear>();
                PlayerPrefs.SetInt("nivel", i + 1);
                SceneManager.LoadScene("galaxia1");
            }
        }
    }
}

/*
 public void convertirNivelAByte(int cnt)
    {
        cargarVectores();
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(Application.persistentDataPath + "/niveles/nivel"+cnt+".bin", FileMode.Open, FileAccess.Read, FileShare.Read);
        Bloque[] bl = new Bloque[220];
        for (byte i = 0; i < 220; i++)
        {
            bl[i] = new Bloque();
            bl[i] = (Bloque)formatter.Deserialize(stream);
        }
        stream.Close();
        if (cnt == 1)
        {
            PlayerPrefs.SetInt("nivelCreado", 1);
        }
        int r = PlayerPrefs.GetInt("nivelCreado");
        IFormatter formatter2 = new BinaryFormatter();
        Stream stream2 = new FileStream(Application.persistentDataPath + "/nivel" + r + ".bytes", FileMode.Create, FileAccess.Write, FileShare.None);
        r++;
        string nivelSTR = "";
        PlayerPrefs.SetInt("nivelCreado", r);
        string pos;
        string color;
        for (int i = 0; i < 220; i++)
        {
            pos = cargarCeros(bl[i].pos);
            nivelSTR += pos;
            nivelSTR += ",";
            color = cargarCeros(bl[i].color);
            nivelSTR += color + "|";
        }
        formatter2.Serialize(stream2, nivelSTR);
        stream2.Close();
    }

    public void convertirNivelesABytes()
    {
        int cnt = 1;
        for(cnt = 1; cnt<=44; cnt++)
        {
            convertirNivelAByte(cnt);
        }
    }
    

public void mostrarNivel()
    {
        cargarVectores();
        IFormatter formatter = new BinaryFormatter();
        int n = PlayerPrefs.GetInt("nivel");
        Stream stream = new FileStream(Application.persistentDataPath + "/nivel" + nivel + ".bin", FileMode.Open, FileAccess.Read, FileShare.Read);
        int contadorBloques = 0;
        for (byte i = 0; i < 220; i++)
        {
            bloque[i] = (Bloque)formatter.Deserialize(stream);
            if (bloque[i].color != 0)
            {
                Vector3 pos = new Vector3(vPosBloques[bloque[i].pos].x, vPosBloques[bloque[i].pos].y, 0);
                Renderer rend = objBloque.GetComponent<Renderer>();
                switch (bloque[i].color)
                {
                    case 1:
                        rend.sharedMaterial = rojo;
                        break;
                    case 2:
                        rend.sharedMaterial = verde;
                        break;
                    case 3:
                        rend.sharedMaterial = azul;
                        break;
                    case 4:
                        rend.sharedMaterial = amarillo;
                        break;
                    case 5:
                        rend.sharedMaterial = naranja;
                        break;
                    case 6:
                        rend.sharedMaterial = marron;
                        break;
                    case 7:
                        rend.sharedMaterial = verdeOscuro;
                        break;
                    case 8:
                        rend.sharedMaterial = celeste;
                        break;
                    case 9:
                        rend.sharedMaterial = rosa;
                        break;
                    case 10:
                        rend.sharedMaterial = violeta;
                        break;
                    case 11:
                        rend.sharedMaterial = blanco;
                        break;
                    case 12:
                        rend.sharedMaterial = gris;
                        break;
                    case 13:
                        rend.sharedMaterial = negro;
                        break;
                }
                Instantiate(objBloque, pos, Quaternion.identity);
                objBloque.name = i.ToString();
                contadorBloques++;
            }
        }
        //Debug.Log("bloque: " + bloque[0].pos + " color: " + bloque[0].color);
        //Debug.Log("bloque: " + bloque[1].pos+" color: "+bloque[1].color);
        stream.Close();
    }

    */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#region noSeUsaMas
/*
public class crea: MonoBehaviour
{
    //public GameObject BLOQUE;
    //private GameObject bloque;
    posBloque[] vPosBloques = new posBloque[220];
    // Use this for initialization
    Bloque[] bloque = new Bloque[220];
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

    [Serializable]
    public class Bloque
    {
        byte Pos;
        byte Color;

        public byte pos
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

        public byte color
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
        float X = -4f;
        float Y = 9.8f;
        int cFilas = 0;
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

    public void guardar()
    {
        int n = PlayerPrefs.GetInt("nivelCreado");
        IFormatter formatter = new BinaryFormatter();
        //Stream stream = new FileStream("C:/Users/ramiro/Desktop/proyectos unity/Arkadroid/niveles/nivel" + n + ".bin", FileMode.Create, FileAccess.Write, FileShare.None);
        Stream stream = new FileStream(Application.persistentDataPath + "/nivel" + n + ".bin", FileMode.Create, FileAccess.Write, FileShare.None);
        n++;
        PlayerPrefs.SetInt("nivelCreado", n);
        for (byte i = 0; i < 220; i++)
        {
            formatter.Serialize(stream, bloque[i]);
        }
        stream.Close();
        //Debug.Log(Application.persistentDataPath + "/nivel0.bin");
    }

    public void cargar()
    {
        IFormatter formatter = new BinaryFormatter();
        int n = PlayerPrefs.GetInt("nivel");
        Stream stream = new FileStream(Application.persistentDataPath + "/nivel" + n + ".bin", FileMode.Open, FileAccess.Read, FileShare.Read);
        //___________sacar para base de datos
        int contadorBloques = 0;
        for (byte i = 0; i < 220; i++)
        {
            bloque[i] = (Bloque)formatter.Deserialize(stream);
            if (bloque[i].color != 0)
            {
                Vector3 pos = new Vector3(vPosBloques[bloque[i].pos].x, vPosBloques[bloque[i].pos].y, 0);
                Renderer rend = objBloque.GetComponent<Renderer>();
                //Debug.Log("bloqueee: " + i + " color: " + bloque[i].color);
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
                contadorBloques++;
            }
        }
        //Debug.Log("bloque: " + bloque[0].pos + " color: " + bloque[0].color);
        //Debug.Log("bloque: " + bloque[1].pos+" color: "+bloque[1].color);
        stream.Close();
        GM scriptGM = GameObject.Find("GM").GetComponent<GM>();
        scriptGM.bloques = contadorBloques;
        //sacar en base de datos

    }

    public void continuarJuego()
    {
        IFormatter formatter = new BinaryFormatter();
        int n = PlayerPrefs.GetInt("continuarNivel");
        Stream stream = new FileStream(Application.persistentDataPath + "/nivel" + n + ".bin", FileMode.Open, FileAccess.Read, FileShare.Read);
        //___________sacar para base de datos
        int contadorBloques = 0;
        for (byte i = 0; i < 220; i++)
        {
            bloque[i] = (Bloque)formatter.Deserialize(stream);
            if (bloque[i].color != 0)
            {
                Vector3 pos = new Vector3(vPosBloques[bloque[i].pos].x, vPosBloques[bloque[i].pos].y, 0);
                Renderer rend = objBloque.GetComponent<Renderer>();
                //Debug.Log("bloqueee: " + i + " color: " + bloque[i].color);
                //switch (bloque[i].color)
                //{
                //    case 1:
                //        rend.sharedMaterial = rojo;
                //        break;
                //    case 2:
                //        rend.sharedMaterial = verde;
                //        break;
                //    case 3:
                //        rend.sharedMaterial = azul;
                //        break;
                //    case 4:
                //        rend.sharedMaterial = amarillo;
                //        break;
                //    case 5:
                //        rend.sharedMaterial = naranja;
                //        break;
                //    case 6:
                //        rend.sharedMaterial = marron;
                //        break;
                //    case 7:
                //        rend.sharedMaterial = verdeOscuro;
                //        break;
                //    case 8:
                //        rend.sharedMaterial = celeste;
                //        break;
                //    case 9:
                //        rend.sharedMaterial = rosa;
                //        break;
                //    case 10:
                //        rend.sharedMaterial = violeta;
                //        break;
                //    case 11:
                //        rend.sharedMaterial = blanco;
                //        break;
                //    case 12:
                //        rend.sharedMaterial = gris;
                //        break;
                //    case 13:
                //        rend.sharedMaterial = negro;
                //        break;
                //}
                Instantiate(objBloque, pos, Quaternion.identity);
                contadorBloques++;
            }
        }
        //Debug.Log("bloque: " + bloque[0].pos + " color: " + bloque[0].color);
        //Debug.Log("bloque: " + bloque[1].pos+" color: "+bloque[1].color);
        stream.Close();
        GM scriptGM = GameObject.Find("GM").GetComponent<GM>();
        scriptGM.bloques = contadorBloques;
        //sacar en base de datos

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void guardarBloque(string nomBloque, int color)
    {

        for (int i = 0; i < 220; i++)
        {
            if (nomBloque == "Button (" + i + ")")
            {
                botonCrear bt = GameObject.Find(nomBloque).GetComponent<botonCrear>();
                bloque[i].pos = (byte)i;
                bloque[i].color = (byte)bt.nColor;
                //Debug.Log("bloque: "+i+" color: "+ bloque[i].color);
            }
        }
    }








}
*/
#endregion
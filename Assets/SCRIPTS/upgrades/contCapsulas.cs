using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class contCapsulas : MonoBehaviour
{
    public static contCapsulas esteObjeto = null;
    public int capsulasTotales;

    private GameObject capBloque;
    private GameObject capColor;
    private GameObject capExpandirVaus;
    private GameObject capIA;
    private GameObject capIman;
    private GameObject capLaser;
    private GameObject capLaserDificil;
    private GameObject capPelotas;
    private GameObject capVida;
    private GameObject capSuperBall;
    private GameObject capAmetralladora;
    List<GameObject> capsulas = new List<GameObject>();

    private void Awake()
    {
        if (esteObjeto == null) { esteObjeto = this; } else if (esteObjeto != this) { Destroy(gameObject); }
        this.cargarCapsulasResourses(capsulas);
    }

    private void cargarCapsulasResourses(List<GameObject> capsulas)
    {
        capsulas.Add(Resources.Load("upgrades/upPelotas") as GameObject);      //1
        capsulas.Add(Resources.Load("upgrades/upLaser") as GameObject);        //2
        capsulas.Add(Resources.Load("upgrades/upLaserDificil") as GameObject); //3
        capsulas.Add(Resources.Load("upgrades/upVida") as GameObject);         //4
        capsulas.Add(Resources.Load("upgrades/upExpand") as GameObject);       //5
        capsulas.Add(Resources.Load("upgrades/upIA") as GameObject);           //6
        capsulas.Add(Resources.Load("upgrades/upAmetralladora") as GameObject);//7
        capsulas.Add(Resources.Load("upgrades/upBloque") as GameObject);       //8
        capsulas.Add(Resources.Load("upgrades/upIman") as GameObject);         //9
        capsulasTotales = 9; //CAMBIAR ESTE DATO CUANDO SE AGREGUEN NUEVAS CAPSULAS
    }

    /// <summary>
    /// ESTA FUNCION TRAE EL TIPO DE CAPSULA Y LA POS Y CREA OTRA CAPSULA PARA Q NO CAIGAN 2 IGUALES SI YA SE TIENE LA HABILIDAD EN LA VAUS
    /// </summary>
    /// <param name="numCapsula"></param>
    /// <param name="pos"></param>
    public void crearCapsulaRepetida(int numCapsula, Vector3 pos)
    {
        int numeroRandom = Random.Range(0, capsulasTotales);
        while (numCapsula == numeroRandom)
        {
            numeroRandom = Random.Range(0, capsulasTotales);
        }
        switch (numeroRandom)
        {
            case 0:
                crearCapsulaPelotas(pos);
                break;
            case 1:
                crearCapsulaLaser(pos);
                break;
            case 2:
                crearCapsulaLaserDificil(pos);
                break;
            case 3:
                crearCapsulaVida(pos);
                break;
            case 4:
                crearCapsulaExpandirVaus(pos);
                break;
            case 5:
                crearCapsulaIman(pos);
                break;
            case 6:
                crearCapsulaIA(pos);
                break;
        }
    }

    public void crearCapsula(Vector3 posBloque, int tipoCapsula)
    {   //posiciono a la capsula en el mismo lugar donde estaba el bloque
        foreach (var capsula in capsulas)
        {
            if (capsulas.IndexOf(capsula) + 1 == tipoCapsula)
            {
                Instantiate(capsula, posBloque, Quaternion.identity);
                GM.esteObjeto.capsulaEnCaida = true;//TODO quedo medio mal lo de q caiga 1 sola capsula por vez tratar de arreglarlo
            }
        }
        //switch (tipoCapsula)
        //{
        //    case 0:

        //        Instantiate(capPelotas, posBloque, Quaternion.identity);
        //        break;
        //    case 1:
        //        Instantiate(capLaser, posBloque, Quaternion.identity);
        //        break;
        //    case 2:
        //        Instantiate(capLaserDificil, posBloque, Quaternion.identity);
        //        break;
        //    case 3:
        //        Instantiate(capVida, posBloque, Quaternion.identity);
        //        break;
        //    case 4:
        //        Instantiate(capExpandirVaus, posBloque, Quaternion.identity);
        //        break;
        //    case 5:
        //        Instantiate(capExpandirVaus, posBloque, Quaternion.identity);
        //        break;
        //    case 6:
        //        Instantiate(capIA, posBloque, Quaternion.identity);
        //        break;
        //    case 7:
        //        Instantiate(capColor, posBloque, Quaternion.identity);
        //        break;
        //    case 8:
        //        Instantiate(capBloque, posBloque, Quaternion.identity);
        //        break;
        //    case 9:
        //        Instantiate(capSuperBall, posBloque, Quaternion.identity);
        //        break;
        //    case 10:
        //        Instantiate(capAmetralladora, posBloque, Quaternion.identity);
        //        break;
        //}
    }

    #region old
    public void crearCapsulaPelotas(Vector3 posBloque)
    {   //posiciono a la capsula en el mismo lugar donde estaba el bloque
        Instantiate(capPelotas, posBloque, Quaternion.identity);
    }

    public void crearCapsulaLaser(Vector3 posBloque)
    {   //posiciono a la capsula en el mismo lugar donde estaba el bloque
        Instantiate(capLaser, posBloque, Quaternion.identity);
    }

    public void crearCapsulaLaserDificil(Vector3 posBloque)
    {   //posiciono a la capsula en el mismo lugar donde estaba el bloque
        Instantiate(capLaserDificil, posBloque, Quaternion.identity);
    }

    public void crearCapsulaVida(Vector3 posBloque)
    {   //posiciono a la capsula en el mismo lugar donde estaba el bloque
        Instantiate(capVida, posBloque, Quaternion.identity);
    }

    public void crearCapsulaExpandirVaus(Vector3 posBloque)
    {   //posiciono a la capsula en el mismo lugar donde estaba el bloque
        Instantiate(capExpandirVaus, posBloque, Quaternion.identity);
    }

    public void crearCapsulaIman(Vector3 posBloque)
    {   //posiciono a la capsula en el mismo lugar donde estaba el bloque
        Instantiate(capIman, posBloque, Quaternion.identity);
    }

    public void crearCapsulaIA(Vector3 posBloque)
    {   //posiciono a la capsula en el mismo lugar donde estaba el bloque
        Instantiate(capIman, posBloque, Quaternion.identity);
    }
    #endregion

}

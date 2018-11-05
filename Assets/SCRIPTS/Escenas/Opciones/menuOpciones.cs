using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.ComponentModel;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;


public class menuOpciones : MonoBehaviour
{
    Toggle tgMusica;
    Toggle tgEnemigos;
    AudioSource parlante;
    int musica;
    int enemigosInGame;
    public AudioClip btnAceptar = null;
    public AudioClip btnCancelar = null;
    public float volumen = 1.0f;
    protected Transform posicion = null;
    String path;
    MailMessage mail = new MailMessage();
    public Font fontDefault;
    public Font fontArka;

    private void Awake()
    {

    }
    void Start()
    {
        posicion = transform;
        guardarDificultad();
        musica = PlayerPrefs.GetInt("musica");
        enemigosInGame = PlayerPrefs.GetInt("enemigosInGame");
        tgMusica = GameObject.Find("tgMusica").GetComponent<Toggle>();
        tgEnemigos = GameObject.Find("tgEnemigos").GetComponent<Toggle>();

        tgMusica.isOn = false;
        if (musica == 1) //chek box q reproduce la musica o no
        {
            tgMusica.isOn = true;
        }

        tgEnemigos.isOn = false;
        if (enemigosInGame == 1) //check box q activa los bichos en los niveles
        {
            tgEnemigos.isOn = true;
        }

        parlante = GameObject.Find("parlante").GetComponent<AudioSource>();

        Image imBajo = GameObject.Find("btnBajo").GetComponent<Image>();
        Image imMedio = GameObject.Find("btnMedio").GetComponent<Image>();
        Image imMaximo = GameObject.Find("btnMaximo").GetComponent<Image>();
        int dificultad = PlayerPrefs.GetInt("dificultadJuego");
        if (dificultad == 1)
        {
            //txt.text = "1";
            imBajo.color = Color.green;
        }
        if (dificultad == 2)
        {
            //txt.text = "2";
            imMedio.color = Color.green;
        }
        if (dificultad == 3)
        {
            //txt.text = "3";
            imMaximo.color = Color.green;
        }

    }

    void Update()
    {
        if (!tgMusica.isOn) // para cortar la musica al momento de tocar el checkbox
        {
            parlante.enabled = false;
        }
        else
        {
            parlante.enabled = true;
        }
    }

    public void irAopciones()
    {
        SceneManager.LoadScene("opciones");
    }

    public void irACrearNiveles()
    {
        SceneManager.LoadScene("crearNivel");
    }

    public void irAMenuPrincipal()
    {
        SceneManager.LoadScene("menuPrincipal");
    }

    public void guardarPreferencias()
    {
        PlayerPrefs.SetInt("musica", 0);
        PlayerPrefs.SetInt("enemigosInGame", 0);

        if (tgMusica.isOn)
        {
            PlayerPrefs.SetInt("musica", 1);
        }
        if (tgEnemigos.isOn)
        {
            PlayerPrefs.SetInt("enemigosInGame", 1);
        }
    }

    public void cambiarLenguaje()
    {
        int idioma = GameObject.Find("ddLenguaje").GetComponent<Dropdown>().value;
        if (idioma != 0)
        {
            PlayerPrefs.SetInt("idioma", idioma - 2);
            SceneManager.LoadScene(0);
        }
    }

    public void cambiarLenguajeBtn()
    {
        /*
        try
        {
            Button btnIdiomass = GameObject.Find("btn-1").GetComponent<Button>();
            btnIdiomass.onClick.AddListener(
                                delegate
                                {
                                    PlayerPrefs.SetInt("idioma", -1);
                                    SceneManager.LoadScene(0);
                                });
            List<Button> vBtnIdioma = new List<Button>();
            int i;
            for (i=0; i<6; i++)
            {
                Button btn = GameObject.Find("btn" + i).GetComponent<Button>();
                btn.onClick.AddListener(
                                delegate
                                {
                                    PlayerPrefs.SetInt("idioma", i);
                                    SceneManager.LoadScene(0);
                                });
                //vBtnIdioma.Add(btn);
            }
            i = 0;
            //foreach (var item in vBtnIdioma)
            //{
            //    item.onClick.AddListener(
            //                    delegate
            //                    {
            //                        PlayerPrefs.SetInt("idioma", i);
            //                        SceneManager.LoadScene(0);
            //                    });
            //    i++;
            //}
            
            
        }
        catch (Exception)
        {

            throw;
        }*/


        Button btnIdioma0 = GameObject.Find("btn0").GetComponent<Button>();
        Button btnIdioma1 = GameObject.Find("btn1").GetComponent<Button>();
        Button btnIdioma2 = GameObject.Find("btn2").GetComponent<Button>();
        Button btnIdioma3 = GameObject.Find("btn3").GetComponent<Button>();
        Button btnIdioma4 = GameObject.Find("btn4").GetComponent<Button>();
        Button btnIdioma5 = GameObject.Find("btn5").GetComponent<Button>();
        Button btnIdioma6 = GameObject.Find("btn6").GetComponent<Button>();
        Button btnIdioma7 = GameObject.Find("btn7").GetComponent<Button>();
        Button btnIdioma8 = GameObject.Find("btn8").GetComponent<Button>();
        Button btnIdioma9 = GameObject.Find("btn9").GetComponent<Button>();

        Button btnIdiomass = GameObject.Find("btn-1").GetComponent<Button>();
        btnIdiomass.onClick.AddListener(
                            delegate
                            {
                                PlayerPrefs.SetInt("idioma", -1);
                                SceneManager.LoadScene(0);
                            });
        btnIdioma0.onClick.AddListener(
                            delegate
                            {
                                PlayerPrefs.SetInt("idioma", 0);
                                SceneManager.LoadScene(0);
                            });
        btnIdioma1.onClick.AddListener(
                            delegate
                            {
                                PlayerPrefs.SetInt("idioma", 1);
                                SceneManager.LoadScene(0);
                            });

        btnIdioma2.onClick.AddListener(
                            delegate
                            {
                                PlayerPrefs.SetInt("idioma", 2);
                                SceneManager.LoadScene(0);
                            });

        btnIdioma3.onClick.AddListener(
                            delegate
                            {
                                PlayerPrefs.SetInt("idioma", 3);
                                SceneManager.LoadScene(0);
                            });
        btnIdioma4.onClick.AddListener(
                            delegate
                            {
                                PlayerPrefs.SetInt("idioma", 4);
                                SceneManager.LoadScene(0);
                            });
        btnIdioma5.onClick.AddListener(
                            delegate
                            {
                                PlayerPrefs.SetInt("idioma", 5);
                                SceneManager.LoadScene(0);
                            });
        btnIdioma6.onClick.AddListener(
                            delegate
                            {
                                PlayerPrefs.SetInt("idioma", 6);
                                SceneManager.LoadScene(0);
                            });
        btnIdioma7.onClick.AddListener(
                            delegate
                            {
                                PlayerPrefs.SetInt("idioma", 7);
                                SceneManager.LoadScene(0);
                            });
        btnIdioma8.onClick.AddListener(
                            delegate
                            {
                                PlayerPrefs.SetInt("idioma", 8);
                                SceneManager.LoadScene(0);
                            });
        btnIdioma9.onClick.AddListener(
                            delegate
                            {
                                PlayerPrefs.SetInt("idioma", 9);
                                SceneManager.LoadScene(0);
                            });

        //Button btnIdiomas;

        //for (int i = 0; i < 4; i++)
        //{
        //    btnIdiomas = GameObject.Find("btn" + i.ToString()).GetComponent<Button>();
        //    btnIdiomas.onClick.AddListener(
        //                        delegate
        //                        {
        //                            Debug.Log("asd");
        //                            PlayerPrefs.SetInt("idioma", i);
        //                            SceneManager.LoadScene(0);
        //                        });
        //}


    }

    public void SendMail()
    {
        Application.OpenURL("mailto=" + "infinitycr.arkadroid3d@gmail.com" + "?subject:" + "holaa" + "&body:" + "cuerpoo");
        /*
        string aFrom; string aTo; string aSubject; string aBody; string aPassword;
        aFrom = "ramiroprk@gmail.com";
        aTo="infinitycr.arkadroid3d@gmail.com";
        aSubject = "asd";
        aBody = "dsa";
        aPassword = "RamdelprkK69";
        if (!aTo.Contains("@") && !aTo.ToLower().Contains(".com"))
            return;
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(aFrom);
        mail.To.Add(aTo);
        mail.Subject = aSubject;
        mail.Body = aBody;

        SmtpClient smtpServer = new SmtpClient();
        smtpServer.Host = "smtp.gmail.com";
        smtpServer.Port = 587;
        smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpServer.Credentials = new System.Net.NetworkCredential(aFrom, aPassword) as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };
        smtpServer.Send(mail);*/
    }

    public void mandarAmail()
    {
        Application.OpenURL("https://mail.google.com/mail/u/0/#inbox?compose=164143cb7d48e691");
    }

    public void repSonAceptar()
    {
        if (btnAceptar) AudioSource.PlayClipAtPoint(btnAceptar, posicion.position, volumen);
    }

    public void repSonCancelar()
    {
        if (btnCancelar) AudioSource.PlayClipAtPoint(btnCancelar, posicion.position, volumen);
    }

    public void guardarDificultad()
    {
        //TextMesh txt = GameObject.Find("txt").GetComponent<TextMesh>();
        GameObject goBajo = GameObject.Find("btnBajo");
        GameObject goMedio = GameObject.Find("btnMedio");
        GameObject goMaximo = GameObject.Find("btnMaximo");
        Image imBajo = GameObject.Find("btnBajo").GetComponent<Image>();
        Image imMedio = GameObject.Find("btnMedio").GetComponent<Image>();
        Image imMaximo = GameObject.Find("btnMaximo").GetComponent<Image>();
        Button btnBajo = goBajo.GetComponent<Button>();
        Button btnMedio = goMedio.GetComponent<Button>();
        Button btnMaximo = goMaximo.GetComponent<Button>();
        btnBajo.onClick.AddListener(
                            delegate
                            {
                                imBajo.color = Color.green;
                                imMedio.color = Color.white;
                                imMaximo.color = Color.white;
                                PlayerPrefs.SetInt("dificultadJuego", 1);
                                //txt.text = PlayerPrefs.GetInt("dificultadJuego").ToString();
                            });
        btnMedio.onClick.AddListener(
                            delegate
                            {
                                imBajo.color = Color.white;
                                imMedio.color = Color.green;
                                imMaximo.color = Color.white;
                                PlayerPrefs.SetInt("dificultadJuego", 2);
                                //txt.text = PlayerPrefs.GetInt("dificultadJuego").ToString();
                            });
        btnMaximo.onClick.AddListener(
                            delegate
                            {
                                imBajo.color = Color.white;
                                imMedio.color = Color.white;
                                imMaximo.color = Color.green;
                                PlayerPrefs.SetInt("dificultadJuego", 3);
                                //txt.text = PlayerPrefs.GetInt("dificultadJuego").ToString();
                            });

    }

    
}

/*
public void graficos0()
{
    QualitySettings.SetQualityLevel(0);

}

public void graficos1()
{
    QualitySettings.SetQualityLevel(1);

}

public void graficos2()
{
    QualitySettings.SetQualityLevel(2);

}
public void graficos3()
{
    QualitySettings.SetQualityLevel(3);

}
public void graficos4()
{
    QualitySettings.SetQualityLevel(4);

}
public void graficos5()
{
    QualitySettings.SetQualityLevel(5);

}

public void graficos6()
{
    QualitySettings.SetQualityLevel(6);

}
*/



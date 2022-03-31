using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    //Variable
    private GameObject personaje;
    private GameObject variables;
    
    //Monedas
    private int monedas;

    //Vidas
    private int vidas;

    //GameOver
    private GameObject panelGameOver;
    private GameObject panelNext;
    private Text textoVidas;
    private Text textoMonedas;
    
    // Start is called before the first frame update
    void Start()
    {
        //Iniciar Variables
        variables = GameObject.FindWithTag("Variables");
        personaje = GameObject.FindGameObjectWithTag("Player");
        vidas = variables.GetComponent<Variables>().getVidas();
        textoVidas = GameObject.Find("Contador_Vidas").GetComponent<Text>();
        textoMonedas = GameObject.Find("Contador_Monedas").GetComponent<Text>();
        panelGameOver = GameObject.Find("Panel_GameOver");
        panelNext = GameObject.Find("Panel_Next");
        panelGameOver.SetActive(false);
        panelNext.SetActive(false);
        monedas = variables.GetComponent<Variables>().getMonedas();
    }

    // Update is called once per frame
    void Update()
    {
        //Enviamos las vidas y las monedas a la pantalla
        textoVidas.text = Convert.ToString(vidas);
        textoMonedas.text = Convert.ToString(monedas);

        //Si las vidas bajan a cero termina la partida
        if (vidas <= 0)
        {
            personaje.GetComponent<Jugador>().setMuerto(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemigo"));
            personaje.SetActive(false);
            panelGameOver.SetActive(true);
        }
    }

    //Método para final 
    public void finNivel()
    {
        personaje.GetComponent<Jugador>().setGanar(true);
        personaje.GetComponent<Jugador>().setMuerto(true);
        panelNext.SetActive((true));
    }

    //Reaniciar
    public void reset()
    {
        variables.GetComponent<Variables>().setVidas(vidas);
        variables.GetComponent<Variables>().setMonedas(monedas);
        SceneManager.LoadScene(0);
    }

    //Método para el final de un nivel
    public void next()
    {
        variables.GetComponent<Variables>().setVidas(vidas);
        variables.GetComponent<Variables>().setMonedas(monedas);
        SceneManager.LoadScene(2);
    }

    
    public void final()
    {
        Application.Quit();
    }
    //Por cada 10 monedas le sumamos 1 vida
    public void increMonedas()
    {
        gameObject.GetComponent<AudioSource>().Play();
        monedas++;
        if (monedas == 10)
        {
            monedas = 0;
            vidas++;
        }
    }

    //La seta especial de vidas
    public void especial()
    {
        vidas += 1000;
        gameObject.GetComponent<AudioSource>().Play();
    }
    
    //Para restar 1 vida
    public void decreVidas()
    {
        vidas--;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muerte : MonoBehaviour
{
    //Controlar la muerte
    private GameObject personaje; 
    private GameObject cabezaPersonaje; 
    private GameObject piesPersonaje; 
    private GameObject camara;
    private GameObject gameMaster;
    private Vector3 inicioCamara;
    private Vector3 inicioPersonaje;
    private Vector3 inicioAsesino;
    private bool libreMatar;

    void Start()
    {
        //Iniciar variables
        personaje = GameObject.FindWithTag("Player");
        cabezaPersonaje = GameObject.FindWithTag("Cabeza");
        piesPersonaje = GameObject.FindWithTag("Pies");
        camara = GameObject.FindWithTag("MainCamera");
        gameMaster = GameObject.FindWithTag("GameController");
        inicioCamara = camara.transform.position;
        inicioPersonaje = personaje.transform.position;
        inicioAsesino = gameObject.transform.position;
        libreMatar = true;
    }

    //Si colisiona con el personaje
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && libreMatar)
        {
            //Sonido muerte
            personaje.GetComponent<AudioSource>().Play();
            
            //Cambiamos el estado del personaje
            personaje.GetComponent<BoxCollider2D>().enabled = false;
            cabezaPersonaje.GetComponent<PolygonCollider2D>().enabled = false;
            piesPersonaje.GetComponent<PolygonCollider2D>().enabled = false;

            //personaje.GetComponentInChildren<PolygonCollider2D>().enabled = false;
            personaje.GetComponent<Jugador>().setMuerto(true);
            float posicionY = Mathf.PingPong(0, 0.4f);
            
            //Movimiento mientras realiza la animación de muerte
            personaje.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            personaje.transform.position = new Vector3(personaje.transform.position.x, posicionY, 0);
            
            //Para volver a la posición inicial
            StartCoroutine(PauseMuerte());
        }

        //Si el enemigo se cae del suelo tambien muere
        if (gameObject.CompareTag("Enemigo") && other.gameObject.CompareTag("Muerte"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator PauseMuerte()
    {
        //Le establecemos el tiempo de espera en segundos, una vez pase ese tiempo ejecuatara las lineas siguientes
        yield return new WaitForSeconds(1f);
        
        //Movemos el asesino donde empezo
        gameObject.transform.position = inicioAsesino;
        
        //La camara vuelva a su posicion inicial
        camara.transform.position = inicioCamara;

        //Devolvemos al personaje al principio 
        personaje.transform.position = inicioPersonaje;

        //Cambiamos el estado del personaje a muerte = false
        personaje.GetComponent<Jugador>().setMuerto(false);
        
        //Acitvamos su BoxCollider del personaje 
        personaje.GetComponent<BoxCollider2D>().enabled = true;
        cabezaPersonaje.GetComponent<PolygonCollider2D>().enabled = true;
        piesPersonaje.GetComponent<PolygonCollider2D>().enabled = true;

        //Vidas
        gameMaster.GetComponent<GameMaster>().decreVidas();
    }

    public void setLibreMatar(bool valor)
    {
        libreMatar = valor;
    }
}

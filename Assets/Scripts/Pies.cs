using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pies : MonoBehaviour
{
    //Variables
    private GameObject jugador;
    
    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");
    }
    
    //Comprobamos si es un enemigo y lo matamos
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemigo"))
        {
            other.gameObject.GetComponentInParent<Muerte>().setLibreMatar(false);
            other.gameObject.GetComponentInParent<Enemigo>().setMuerto(true);
            jugador.GetComponent<Jugador>().setAsesino(true);
        }
    }
}

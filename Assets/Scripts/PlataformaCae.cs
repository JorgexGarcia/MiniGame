using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlataformaCae : MonoBehaviour
{
    //Tempoarizador para caer y reaparecer
    private float temporizador;
    private float temporizadorReset;
    private Vector3 inicio;
    
    private Rigidbody2D rigiPlataforma;
    private BoxCollider2D boxColPlataforma ;
    
    // Start is called before the first frame update
    void Start()
    {
        //Iniciar variables
        rigiPlataforma = gameObject.GetComponent<Rigidbody2D>();
        boxColPlataforma = gameObject.GetComponent<BoxCollider2D>();
        temporizador = 2.5f;
        temporizadorReset = 6f;
        inicio = gameObject.transform.position;
    }

    //Cuando la plataforma detecta que esta el personaje sobre ella
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pies"))
        {
            Invoke("Caer", temporizador);
            Invoke("Reset", (temporizador + temporizadorReset));
        }
    }

    //Método para caer la plataforma
    void Caer()
    {
        rigiPlataforma.isKinematic = false;
        boxColPlataforma.isTrigger = true;
    }

    //Reset
    void Reset()
    {
        gameObject.transform.position = inicio;
        rigiPlataforma.isKinematic = true;
        rigiPlataforma.velocity = Vector2.zero;
        boxColPlataforma.isTrigger = false;
    }
}

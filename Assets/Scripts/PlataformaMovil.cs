using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlataformaMovil : MonoBehaviour
{
    //Movimiento plataforma
    public GameObject objetivo;
    private GameObject jugador;
    private float velocidad;
    private Vector3 inicio;
    private Vector3 final;

    // Start is called before the first frame update
    void Start()
    {
        velocidad = 1.5f;
        inicio = gameObject.transform.position;
        final = objetivo.transform.position;
        jugador = GameObject.FindGameObjectWithTag("Player");
        //Para que el objetivo no se mueva con su Parent
        if (objetivo != null)
        {
            objetivo.transform.parent = null;
        }
    }

    private void FixedUpdate()
    {
        //La plataforma se mueve a buscar un objetivo, cuando llega a el, el objetivo cambia a su posición inicial
        float fixedVelocidad = velocidad * Time.deltaTime;
        if (objetivo != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, objetivo.transform.position, fixedVelocidad);
        }
        if (transform.position == objetivo.transform.position)
        {
            objetivo.transform.position = (objetivo.transform.position == inicio) ? final : inicio;
        }
    }
    
    //Si el personaje se coloca sobre ella, se convierte en su padre para que el personaje se mueva junto con ella 
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            jugador.transform.parent = gameObject.transform;
        }
    }

    //Cuando el personaje se va de ella
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            jugador.transform.parent = null;
        }
    }
}

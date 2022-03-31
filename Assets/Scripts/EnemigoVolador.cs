using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoVolador : MonoBehaviour
{
    //Variables
    private GameObject personaje;
    private float velocidad;
    private Vector3 posicionObjetivo;
    private Vector3 posicionInicial;
    private Rigidbody2D rigiEnemigo;
    private SpriteRenderer sprite;

    private bool move;
    private Animator animaciones;


    // Start is called before the first frame update
    void Start()
    {
        //Iniciar variables
        personaje = GameObject.FindWithTag("Player");
        velocidad = 0.5f;
        move = false;
        animaciones = gameObject.GetComponent<Animator>();
        rigiEnemigo = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        posicionInicial = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        animaciones.SetBool("Move", move);
    }
    
    private void FixedUpdate()
    {
        // Si mata al personaje vuelve a su posición inicial
        if (personaje.GetComponent<Jugador>().getMuerto())
        {
            gameObject.transform.position = posicionInicial;
        }
        
        //Si no esta muerto y ve al personaje puede mover, seguira al personaje hasta que le de caza
        if (!personaje.GetComponent<Jugador>().getMuerto() && move)
        {
            posicionObjetivo = personaje.transform.position;
            float fixedVelocidad = velocidad * Time.deltaTime;
            if (posicionObjetivo != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, posicionObjetivo, fixedVelocidad);
            }
        }
        //Aqui giramos la imagen dependiendo donde se encuentre el personaje
        if ((personaje.transform.position.x - gameObject.transform.position.x) <= 0)
        {
            sprite.flipX = true;
        }
        else 
        {
            sprite.flipX = false;
        }

        //Incrementa su velocidad mientras este siguiendo al personaje 
        if (move && velocidad <= 200f)
        {
            float num = 0.1f * Time.deltaTime;
            velocidad += num;
        }
        //Cuando deja de seguir al personaje baja a su velocidad inicial
        if (!move)
        {
            velocidad = 0.5f;
        }
    }

    //Si entra dentro de la visión del personaje
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name.CompareTo("Vision") == 0)
        {
            move = true;
        }
    }

    //Si se sale de su visión
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.CompareTo("Vision") == 0)
        {
            move = false;
        }
    }
}

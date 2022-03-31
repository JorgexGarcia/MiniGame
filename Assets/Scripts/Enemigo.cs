using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Enemigo : MonoBehaviour
{
    //Variables para el movimiento del enemigo
    private float velocidad;
    private float maxVelocidad;
    private Rigidbody2D rigiEnemigo;
    private SpriteRenderer sprite;
    private GameObject jugador;
    private bool puedoMover;
    
    //Muerto
    private bool muerto;
    private float temporizador;
    private Animator animacion;

    /**
     * Le añadimos una velocidad y el Rigidbody del enemigo 
     */
    void Start()
    {
        //Iniciar variables
        velocidad = 1f;
        maxVelocidad = 1f;
        rigiEnemigo = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        muerto = false;
        animacion = gameObject.GetComponent<Animator>();
        temporizador = 0.5f;
        jugador = GameObject.FindWithTag("Player");
        puedoMover = false;
    }

    /**
     * Le añadimos una fuerza para el movmiento, una velocidad dentro de unos límites. 
     */
    void FixedUpdate()
    {
        //Animaciones
        animacion.SetFloat("Velocidad", Mathf.Abs(rigiEnemigo.velocity.x));
        animacion.SetBool("Muerto", muerto);
        
        //Si no esta muerto y esta dentro del rango de movimiento, el rango de movimiento es si esta cerca del personaje
        //De esta manera hasta que el personaje no este en la zona de ellos no se mueven
        if(!muerto && puedoMover)
        {
            rigiEnemigo.AddForce(Vector2.right * velocidad);
            float limiteVelocidad = Mathf.Clamp(rigiEnemigo.velocity.x, -maxVelocidad, maxVelocidad);
            rigiEnemigo.velocity = new Vector2(limiteVelocidad, rigiEnemigo.velocity.y);

            //Aqui le cambiamos de sentido cuando golpea con algun objeto 
            if ((rigiEnemigo.velocity.x > -0.01f && rigiEnemigo.velocity.x < 0.01f))
            {
                velocidad = -velocidad;
                rigiEnemigo.velocity = new Vector2(velocidad, rigiEnemigo.velocity.y);
            }
        }
        //Aqui giramos la imagen según su valor en x
        if (rigiEnemigo.velocity.x >= 0.1f)
        {
            sprite.flipX = true;
        }
        else if (rigiEnemigo.velocity.x <= -0.1f)
        {
            sprite.flipX = false;
        }
        //Muerto
        if (muerto)
        {
            rigiEnemigo.velocity = Vector2.zero;
            sprite.sortingOrder = -1;
            animacion.SetBool("Muerto", true);
            Invoke("Muerto", temporizador);
        }
    }

    private void Muerto()
    {
        Destroy(gameObject);
    }
    public void setMuerto(bool estado)
    {
        muerto = estado;
        if (muerto)
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

    //Visión del personaje
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.CompareTo("Vision") == 0)
        {
            puedoMover = true;
        }
    }

    //Si no esta cerca del personaje
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.CompareTo("Vision") == 0)
        {
            puedoMover = false;
        }
    }

    //Límite de final de la escena
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name.CompareTo("Limite_I") == 0)
        {
            Destroy(gameObject);
        }
    }
}

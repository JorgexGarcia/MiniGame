using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seta : MonoBehaviour
{
    //Movmiento de la seta
    private bool mover;
    private float velocidad;
    private float maxVelocidad;
    private Rigidbody2D rigiSeta;
    private GameObject gameMaster;
    
    
    // Start is called before the first frame update
    void Start()
    {
        mover = false;
        velocidad = 1f;
        maxVelocidad = 5f;
        rigiSeta = gameObject.GetComponent<Rigidbody2D>();
        gameMaster = GameObject.FindWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        //Cuando sale la seta se mueve en una dirección, va aumentando hasta que el personaje no llega a ella
        if (mover)
        {
            rigiSeta.AddForce(Vector2.left * velocidad);
            float limiteVelocidad = Mathf.Clamp(rigiSeta.velocity.x, -maxVelocidad, maxVelocidad);
            rigiSeta.velocity = new Vector2(limiteVelocidad, rigiSeta.velocity.y);
        }
    }

    public void setMover()
    {
        mover = true;
    }

    //Cuando el personaje la toca
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameMaster.GetComponent<GameMaster>().especial();
            Destroy(gameObject);
        }

        //Si llega al final de la escena
        if (other.gameObject.name.CompareTo("Limite_I") == 0)
        {
            Destroy(gameObject);
        }
    }
}

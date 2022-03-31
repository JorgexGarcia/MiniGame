using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    //Ganar
    private bool ganar;

    public void setGanar(bool valor)
    {
        ganar = valor;
    }
    
    //Suelo
    private bool tocaSuelo;
    private LayerMask capaSuelo;
    private Transform piesPersonajes;
    private float radio;
    private bool dobleSalto;
    private bool puedoSaltar;
    private bool puedoDobleSalto;

    //Salto
    private float powerSalto;
    
    //Velocidad
    private float velocidad;
    private float maxVelocidad;
    
    //Rigidbody
    private Rigidbody2D rigiPlayer;
    
    //Animaciones
    private Animator animacion;

    //Dar la vuelta a la animacion cuando se mueve 
    private SpriteRenderer sprite;

    //Muerto
    private bool muerto;

    private bool asesino;
    
    // Start is called before the first frame update
    void Start()
    {
        //Iniciar variables
        velocidad = 175f;
        maxVelocidad = 4f;
        rigiPlayer = gameObject.GetComponent<Rigidbody2D>();
        animacion = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        powerSalto = 250f;
        tocaSuelo = true;
        muerto = false;
        capaSuelo = LayerMask.GetMask("Suelo");
        piesPersonajes = GameObject.Find("Pies_Jugador").transform;
        radio = 0.25f;
        dobleSalto = false;
        puedoSaltar = false;
        puedoDobleSalto = false;
        ganar = false;
        asesino = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Salto
        if (tocaSuelo)
        {
            dobleSalto = false;
        }
        tocaSuelo = Physics2D.OverlapCircle(piesPersonajes.position, radio, capaSuelo);
        
        //Animaciones
        animacion.SetFloat("Speed", Mathf.Abs(rigiPlayer.velocity.x));
        animacion.SetBool("TocaSuelo", tocaSuelo);
        animacion.SetBool("Muerto", muerto);
        animacion.SetBool("Win", ganar);

        //Para dar la vuelta a la animacion, segun la direccion del movimiento
        if (rigiPlayer.velocity.x >= 0.1f)
        {
            sprite.flipX = false;
        }
        else if (rigiPlayer.velocity.x <= -0.1f)
        {
            sprite.flipX = true;
        }
        
        //Movimientos
        if (!muerto)
        {
            //Salto
            if ((Input.GetKeyDown(KeyCode.Space) && tocaSuelo) )
            {
                puedoSaltar = true;
            }
            
            if (Input.GetKeyDown(KeyCode.Space) && !tocaSuelo && !dobleSalto)
            {
                dobleSalto = true;
                puedoDobleSalto = true;
            }
        }
    }
    
    private void FixedUpdate()
    {
        //Friccion para quedarse quieto despúes de soltar las teclas
        Vector3 fixedVelocity = rigiPlayer.velocity;
        fixedVelocity.x *= 0.9f;
        if (tocaSuelo)
        {
            rigiPlayer.velocity = fixedVelocity;
        }

        //Solo se podra mover si no esta muerto o ha terminado el escenario
        if (!muerto && !ganar)
        {
            //Movimientos
            float direccion = 0;
            if (Input.GetAxis("Horizontal") > 0)
            {
                rigiPlayer.velocity = new Vector2(0,rigiPlayer.velocity.y);
                direccion = 1;
            }

            if (Input.GetAxis("Horizontal") < 0)
            {
                rigiPlayer.velocity = new Vector2(0,rigiPlayer.velocity.y);
                direccion = -1;
            }
        
            //Fuerzas
            rigiPlayer.AddForce(Vector2.right * (velocidad * direccion));

            //Para limitar la velocidad máxima, Con el Clamp le digo el valor máximo y mínimo que tendra
            float limiteVelocidad = Mathf.Clamp(rigiPlayer.velocity.x, -maxVelocidad, maxVelocidad);
            rigiPlayer.velocity = new Vector2(limiteVelocidad, rigiPlayer.velocity.y);
        
            //Salto
            if (puedoSaltar)
            {
                rigiPlayer.AddForce(Vector2.up * powerSalto);
                puedoSaltar = false;
            }
            if (puedoDobleSalto)
            {
                rigiPlayer.AddForce(Vector2.up * powerSalto);
                puedoDobleSalto = false;
            }
            if (asesino)
            {
                rigiPlayer.AddForce(Vector2.up * powerSalto);
                asesino = false;
            }
        }
    }
    
    public void setMuerto(bool valor)
    {
        muerto = valor;
    }

    public bool getMuerto()
    {
        return muerto;
    }

    public void setAsesino(bool valor)
    {
        asesino = valor;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name.CompareTo("Pasillo") == 0)
        {
            velocidad *= 2f;
        }
    }
}

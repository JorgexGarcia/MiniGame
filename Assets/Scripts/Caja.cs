using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caja : MonoBehaviour
{
    //Para saber si la caja ha sido golpeada
    private bool vacio;
    private bool especial;

    private Animator animacion;
    
    // Start is called before the first frame update
    void Start()
    {
        //Iniciar variables
        animacion = gameObject.GetComponent<Animator>();
        
        //Si la caja se llama cajavidas indicamos que es especial
        if (gameObject.name.CompareTo("CajaVidas") == 0)
        {
            especial = true;
        }
        else
        {
            especial = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        animacion.SetBool("Vacia", vacio);
    }

    public void setVacio()
    {
        vacio = true;
    }

    public bool getVacio()
    {
        return vacio;
    }

    public bool getEspecial()
    {
        return especial;
    }
}

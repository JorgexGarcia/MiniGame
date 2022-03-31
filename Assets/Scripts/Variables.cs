using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{
    //Variables para pasar entre escenas
    private int vidas;
    private int monedas;

    public void setVidas(int num)
    {
        vidas = num;
    }

    public void setMonedas(int num)
    {
        monedas = num;
    }

    public int getMonedas()
    {
        return monedas;
    }

    public int getVidas()
    {
        return vidas;
    }

    //Al principio del juego se empieza con 3 vidas y 0 monedas
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        monedas = 0;
        vidas = 3;
    }
}

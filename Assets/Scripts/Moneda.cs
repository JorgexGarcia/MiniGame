using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    //Variables
    private GameObject gameMaster;
    
    // Start is called before the first frame update
    void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GameController");
    }

    //Si la moneda es tocada por el personaje
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameMaster.GetComponent<GameMaster>().increMonedas();
            Destroy(gameObject);
        }
    }
}

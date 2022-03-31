using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabeza : MonoBehaviour
{
    //Variables
    private GameObject gameMaster;
    private GameObject seta;

    private void Start()
    {
        //Iniciar variables 
        gameMaster = GameObject.FindGameObjectWithTag("GameController");
        seta = GameObject.Find("Seta");
    }

    //Si golpea con la cabeza una caja
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Caja"))
        {
            if (!other.gameObject.GetComponent<Caja>().getVacio())
            {
                other.gameObject.GetComponent<Caja>().setVacio();
                if (other.gameObject.GetComponent<Caja>().getEspecial())
                {
                    StartCoroutine(setaEspecial(other));
                }
                else
                {
                    gameMaster.GetComponent<GameMaster>().increMonedas();
                }
            }
        }
    }

    //Si la caja es especial y tiene una seta de vidas
    IEnumerator setaEspecial(Collider2D other)
    {
        yield return new WaitForSeconds(1);
        seta.GetComponent<SpriteRenderer>().enabled = true;
        seta.GetComponent<Seta>().setMover();
        other.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limite : MonoBehaviour
{
    //Variables
    private GameObject gameMaster;
    
    // Start is called before the first frame update
    void Start()
    {
        //Iniciar variables
        gameMaster = GameObject.FindGameObjectWithTag("GameController");
    }
    
    //Límite de final de nivel
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameMaster.GetComponent<GameMaster>().finNivel();
        }
    }
}

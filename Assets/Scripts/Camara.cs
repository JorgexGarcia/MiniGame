using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    //Variables
    private Transform posicionCamara;
    private Transform posicionPersonaje;
    private float minPosicionX;
    private float minPosicionY;
    private float maxPosicionX;
    private float maxPosicionY;
    private Vector2 maxPosicion;

    // Start is called before the first frame update
    void Start()
    {
        //Iniciar variables
        posicionCamara = gameObject.transform;
        posicionPersonaje = GameObject.FindGameObjectWithTag("Player").transform;
        minPosicionX = 0;
        minPosicionY = 0;
        maxPosicionX = 195;
        maxPosicionY = 200;
    }

    private void LateUpdate()
    {
        //Camara sigue al personaje, pero tiene unos límites de actuación
        float posx = posicionPersonaje.position.x;
        float posy = posicionPersonaje.position.y;
        
        gameObject.transform.position = new Vector3(Mathf.Clamp(posx, minPosicionX, maxPosicionX),
            Mathf.Clamp(posy, minPosicionY, maxPosicionY), posicionCamara.position.z);
    }
}

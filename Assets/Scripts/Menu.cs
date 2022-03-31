using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //Menu inicial
    public void empezar()
    {
        SceneManager.LoadScene(1);
    }
    
    public void abandonar()
    {
        Application.Quit();
    }
}

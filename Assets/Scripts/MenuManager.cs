using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menuInicio;
    public GameObject menuGameOver;
    private int estado;
    public FinDeJuego juego;

    private void Awake()
    {
        estado = 0;
    }

    private void Update()
    {
        Inicio();
        Juego();
        GameOver();
    }

    public void Inicio()
    {
        if (Input.GetKeyDown(KeyCode.Return) && estado == 0)
        {
            if (menuInicio != null)
            {
                IniciarPartida();
            }
        }       
    }
    public void IniciarPartida()
    {
        menuInicio.SetActive(false);
        juego.IniciarPartida();
        estado = 1;
    }

    public void Juego()
    {
        if (estado == 1 && juego.GameOver())
        {
            estado = 2;
            menuGameOver.SetActive(true);
        }
    }

    public void GameOver()
    {
        if (estado == 2 && !Input.anyKeyDown )
        {            
            estado = 3;
        }

        if (Input.GetKeyDown(KeyCode.Space) && estado == 3)
        {
            ReiniciarPartida();
        }
    }

    public void ReiniciarPartida()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}

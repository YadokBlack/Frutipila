using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinDeJuego : MonoBehaviour
{
    public float tiempoMaximoPermanencia = 2.0f; 
    private float tiempoActual = 0.0f;
    private bool colisionStayActivada = false;
    private bool gameOver;
    private bool iniciaPartida;

    private void Awake()
    {
        gameOver = false;
        iniciaPartida = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Objeto")) return;

        Colision c = other.GetComponent<Colision>();
        if (c.colisionSuelo)
        {
            if (!colisionStayActivada)
            {
                colisionStayActivada = true;
                tiempoActual = 0.0f;
            }

            tiempoActual += Time.deltaTime;

            Debug.Log("Tiempo de permanencia: " + tiempoActual);

            if (tiempoActual >= tiempoMaximoPermanencia)
            {
                Debug.Log("FINAL DE JUEGO por stay DEBIDO A " + other.transform.name);
                gameOver = true;
            }
        }
        else
        {
            colisionStayActivada = false;
        }
    }

    public void IniciarPartida()
    {
        iniciaPartida = true;
    }

    public bool Iniciada()
    {
        return iniciaPartida;
    }

    public bool GameOver()
    {
        return gameOver;
    }
}

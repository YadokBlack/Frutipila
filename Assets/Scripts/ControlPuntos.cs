using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlPuntos : MonoBehaviour
{
    public int puntos;

    public TextMeshProUGUI mostrarPuntos;
    public TextMeshProUGUI mostrarResultadoPuntos;

    private void Awake()
    {
        puntos = 0;
        ActualizarTextoPuntuacion();
    }

    public void SumarPuntos(int masPuntos)
    {
        puntos += masPuntos;
        ActualizarTextoPuntuacion();
    }

    private void ActualizarTextoPuntuacion()
    {
        mostrarPuntos.text = puntos.ToString();
        mostrarResultadoPuntos.text = puntos.ToString();
    }


}

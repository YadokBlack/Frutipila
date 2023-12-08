using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoBurbuja : MonoBehaviour
{
    private Vector2 posicionInicial;
    [SerializeField]
    private float desplazamientoMaximo;
    [SerializeField]
    private float velocidad;
    private float desplazamiento;

    private void Awake()
    {
        posicionInicial = transform.position;
    }

    private void Update()
    {
        desplazamiento = desplazamientoMaximo * Mathf.Sin(Time.time * velocidad);
        transform.position = new Vector2(posicionInicial.x, posicionInicial.y + desplazamiento);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoBurbuja : MonoBehaviour
{
    private Vector3 posicionInicial;
    private float desplazamientoMaximo;
    private float velocidad;
    private float desplazamiento;

    private void Awake()
    {
        posicionInicial = transform.position;
    }

    private void Update()
    {
        desplazamiento = desplazamientoMaximo * Mathf.Sin(Time.deltaTime * velocidad);
        transform.position = new Vector3(posicionInicial.x, posicionInicial.y + desplazamiento, posicionInicial.z);
    }
}

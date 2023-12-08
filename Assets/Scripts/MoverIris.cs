using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverIris : MonoBehaviour
{
    public int movimiento = 2;
    private Vector3 posicionRaton;
    private Vector3 aumentoPoscionIris;
    private Vector3 posicionInicial;
    private float tiempoAcumulado = 0f;
    public float intervaloActualizacion = 0.5f;
    public float variacion = 0.5f;
    private float intervaloActualiza;

    void Awake()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        tiempoAcumulado += Time.deltaTime;
        if (tiempoAcumulado < intervaloActualiza) return;

        posicionInicial = transform.parent.position;
        Mirar();
        tiempoAcumulado = 0;
        intervaloActualiza = Random.Range(intervaloActualizacion, intervaloActualizacion + variacion);
    }

    void Mirar()
    {
        ObtenerPosicionRaton();
        aumentoPoscionIris = Vector3.zero;
        if (posicionRaton.x > posicionInicial.x) aumentoPoscionIris.x = movimiento;
        if (posicionRaton.x < posicionInicial.x) aumentoPoscionIris.x = -movimiento;
        if (posicionRaton.y > posicionInicial.y) aumentoPoscionIris.y = movimiento;        
        if (posicionRaton.y < posicionInicial.y) aumentoPoscionIris.y = -movimiento;
        transform.position = posicionInicial + aumentoPoscionIris;
    }

    void ObtenerPosicionRaton()
    {
        Vector3 posicion = Input.mousePosition;
        posicionRaton.x = posicion.x;
        posicionRaton.y = posicion.y;
    }
}

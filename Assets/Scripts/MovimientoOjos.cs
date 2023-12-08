using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoOjos : MonoBehaviour
{
    public Transform blancoOjos;
    public Transform iris;
    public float radioBlancoOjos = 20f;
    public float velocidadMovimiento = 5f;
    public float intervaloActualizacion = 0.5f;
    public float variacion = 0.5f;
    private float intervaloActualiza;
    private float tiempoAcumulado = 0f;
    private Vector3 nuevaPosicionIris;
    private Vector3 ultimaPosicionPersonaje;

    private void Awake()
    {
        ultimaPosicionPersonaje = transform.position;
        InicializaNuevaPosicion();
    }

    void Update()
    {
        if (ultimaPosicionPersonaje != transform.position)
        {
            CalculaNuevaPosicion();
            ultimaPosicionPersonaje = transform.position;
        }

        tiempoAcumulado += Time.deltaTime;
        if (tiempoAcumulado > intervaloActualiza) 
        {
            if ( nuevaPosicionIris != null ) iris.position = Vector3.Lerp(iris.position, nuevaPosicionIris, velocidadMovimiento * Time.deltaTime);
            return; 
        }        
        CalculaNuevaPosicion();        
    }

    private void CalculaNuevaPosicion()
    {
        Vector3 posicionRaton = Input.mousePosition;
        posicionRaton.z = 0f;

        float diferenciaVertical = Mathf.Clamp(posicionRaton.y - iris.position.y, -radioBlancoOjos, radioBlancoOjos);

        float nuevaPosicionVertical = blancoOjos.position.y + diferenciaVertical;

        float diferenciaHorizontal = Mathf.Clamp(posicionRaton.x - iris.position.x, -radioBlancoOjos, radioBlancoOjos);

        float nuevaPosicionHorizontal = blancoOjos.position.x + diferenciaHorizontal;

        nuevaPosicionIris.Set(nuevaPosicionHorizontal, nuevaPosicionVertical, 0f);

        iris.position = Vector3.Lerp(iris.position, nuevaPosicionIris, velocidadMovimiento * Time.deltaTime);

        tiempoAcumulado = 0;
        intervaloActualiza = Random.Range(intervaloActualizacion, intervaloActualizacion + variacion);
    }

    private void InicializaNuevaPosicion()
    {
        Vector3 posicionRaton = Input.mousePosition;
        posicionRaton.z = 0f;

        Vector3 posicionBlancoOjos = blancoOjos.position;
        Vector3 posicionIris = iris.position;

        float diferenciaVertical = Mathf.Clamp(posicionRaton.y - posicionIris.y, -radioBlancoOjos, radioBlancoOjos);

        float nuevaPosicionVertical = posicionBlancoOjos.y + diferenciaVertical;

        float diferenciaHorizontal = Mathf.Clamp(posicionRaton.x - posicionIris.x, -radioBlancoOjos, radioBlancoOjos);

        float nuevaPosicionHorizontal = posicionBlancoOjos.x + diferenciaHorizontal;

        nuevaPosicionIris = new Vector3(nuevaPosicionHorizontal, nuevaPosicionVertical, 0f);

        iris.position = Vector3.Lerp(iris.position, nuevaPosicionIris, velocidadMovimiento * Time.deltaTime);

        tiempoAcumulado = 0;
        intervaloActualiza = Random.Range(intervaloActualizacion, intervaloActualizacion + variacion);
    }
}

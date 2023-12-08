using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Fruta {
    Uva,
    Cereza,
    fresa,
    Pera,
    Melocoton,    
    Naranja,
    Limon,
    Manzana,
    Piña,
    Platano,
    Tajada,
    Sandia,
    Coco
}


public class NextFruit : MonoBehaviour
{
    public Image imagenFruta;
    private int proximaFruta;
    [SerializeField]
    private Fruta[] frutas;
    [SerializeField]
    private GameObject[] ListaFrutas; 

    void Start()
    {
        frutas = (Fruta[])Enum.GetValues(typeof(Fruta));        
        ObtenerFrutaAleatoria();
    }

    public GameObject ObtenerObjetoFruta()
    {
        return ListaFrutas[ObtenerFrutaActual()];
    }

    public GameObject ObtenerUnObjetoFruta(int n)
    {
        return ListaFrutas[n];
    }

    private void MuestraFruta()
    {
        imagenFruta.sprite = ListaFrutas[proximaFruta].GetComponent<Image>().sprite;
    }

    private int ObtenerFrutaActual()
    {
        int devuelve = proximaFruta;
        ObtenerFrutaAleatoria();
        return devuelve;
    }

    private void ObtenerFrutaAleatoria()
    {        
        proximaFruta = UnityEngine.Random.Range(0, frutas.Length);
        MuestraFruta();
    }
}
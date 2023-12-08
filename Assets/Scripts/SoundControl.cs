using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    public AudioSource reproductor;
    public AudioClip[] sonidos;
    public AudioSource reproductorColision;
    public AudioSource reproductorCombinacion;

    public void ReproduceSonido(int num)
    {
        if (num >= 0 && num < sonidos.Length)
        {
            reproductor.PlayOneShot(sonidos[num]);
        }
        else
        {
            Debug.LogError("Número de sonido fuera de rango");
        }
    }
    
    public void ReproduceSonidoColision()
    {
        reproductorColision.Play();
    }

    public void ReproduceSonidoCombinacion()
    {
        reproductorCombinacion.Play();
    }
}

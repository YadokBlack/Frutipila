using UnityEngine;

public class ControladorMusica : MonoBehaviour
{
    public AudioSource reproductor1;
    public AudioSource reproductor2;
    public AudioClip cancion1;
    public AudioClip cancion2;
    public int repeticionesAntesDeCambiar = 2;
    public float duracionCrossfade = 2f;

    private int contadorRepeticiones = 0;
    private float tiempoCancionActual = 0f;
    private bool cambiandoCancion = false;

    void Start()
    {
        if (reproductor1 == null || reproductor2 == null || cancion1 == null || cancion2 == null)
        {
            Debug.LogError("Asegúrate de asignar los componentes AudioSource y las canciones en el Inspector.");
            return;
        }

        reproductor1.clip = cancion1;
        reproductor2.clip = cancion2;
        reproductor1.Play();
    }

    void Update()
    {
        if (cambiandoCancion)
        {
            RealizaCrossfade();
        }
        else
        {
            tiempoCancionActual += Time.deltaTime;

            if (tiempoCancionActual >= reproductor1.clip.length)
            {
                ContarCambioCanciones();
            }
        }
    }

    private void ContarCambioCanciones()
    {
        contadorRepeticiones++;
        tiempoCancionActual = 0f;

        if (contadorRepeticiones > repeticionesAntesDeCambiar)
        {
            IniciaCambioCancion();
        }
    }

    void RealizaCrossfade()
    {
        float volumenCancion1 = Mathf.Lerp(1f, 0f, Time.deltaTime / duracionCrossfade);
        float volumenCancion2 = Mathf.Lerp(0f, 1f, Time.deltaTime / duracionCrossfade);

        reproductor1.volume = Mathf.Clamp01(reproductor1.volume - volumenCancion1);
        reproductor2.volume = Mathf.Clamp01(reproductor2.volume + volumenCancion2);

        if (reproductor1.volume == 0f)
        {
            CambiaCancion();
        }
    }

    void IniciaCambioCancion()
    {
        tiempoCancionActual = 0f;
        contadorRepeticiones = 0;

        if (repeticionesAntesDeCambiar == 2)
        {
            repeticionesAntesDeCambiar = 1;
        }
        else
        {
            repeticionesAntesDeCambiar = 2;
        }

        cambiandoCancion = true;
    }

    void CambiaCancion()
    {
        reproductor1.Stop();

        var temp = reproductor1;
        reproductor1 = reproductor2;
        reproductor2 = temp;
        reproductor1.Play();

        reproductor1.volume = 1f;
        reproductor2.volume = 0f;

        cambiandoCancion = false;
    }
}

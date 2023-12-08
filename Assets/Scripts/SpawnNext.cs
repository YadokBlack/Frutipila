using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnNext : MonoBehaviour
{
    public ProximaFruta proximaFruta;
    public GameObject spawnPoint;
    public GameObject padreSpawn;
//    public float tiempoDeEspera; 
//    private float tiempoUltimoLanzamiento;
    private SoundControl controlaSonidos;
    public GameObject linea;
    private Image imagen;
    private GameObject ultimaFrutaLanzada;
    private Colision colisionFrutaLanzada;
    public FinDeJuego Partida;

    private void Awake()
    {
        ultimaFrutaLanzada = null;
        controlaSonidos = FindObjectOfType<SoundControl>();
      //  tiempoUltimoLanzamiento = -tiempoDeEspera;
        if (proximaFruta == null) Debug.LogError("En spawnNext no está asignada proximaFruta");
        imagen = linea.GetComponent<Image>();
    }

    void Update()
    {
        if (Partida.GameOver() || !Partida.Iniciada()) return;

        imagen.color = PuedeLanzar() ? Color.green : Color.red;
        if (Input.GetMouseButtonDown(0))
        {
            if (PuedeLanzar())
            {
                ultimaFrutaLanzada = proximaFruta.LanzarFruta(spawnPoint.transform.position, padreSpawn.transform);
                colisionFrutaLanzada = ultimaFrutaLanzada.GetComponent<Colision>();
              //  tiempoUltimoLanzamiento = Time.time;
            }
            else
            {
                controlaSonidos.ReproduceSonido(3);
            }            
        }
    }

    bool PuedeLanzar()
    {
        if (ultimaFrutaLanzada == null) return true;

       // return Time.time - tiempoUltimoLanzamiento > tiempoDeEspera;
       
    //   Debug.Log( "-" + colisionFrutaLanzada.colisionSuelo );
        return colisionFrutaLanzada.colisionSuelo;
    }
}

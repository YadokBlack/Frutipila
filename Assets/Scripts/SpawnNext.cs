using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpawnNext : MonoBehaviour
{
    public ProximaFruta proximaFruta;
    public GameObject spawnPoint;
    public GameObject padreSpawn;
    private SoundControl controlaSonidos;
    public GameObject linea;
    private Image imagen;
    private GameObject ultimaFrutaLanzada;
    private Colision colisionFrutaLanzada;
    public FinDeJuego Partida;
    public LayerMask capaUI;

    private void Awake()
    {
        ultimaFrutaLanzada = null;
        controlaSonidos = FindObjectOfType<SoundControl>();
        if (proximaFruta == null) Debug.LogError("En spawnNext no está asignada proximaFruta");
        imagen = linea.GetComponent<Image>();
    }

    void Update()
    {
        if (Partida.GameOver() || !Partida.Iniciada()) return;

        if (PunteroSobreCapaUI()) return;

        imagen.color = PuedeLanzar() ? Color.green : Color.red;
        if (Input.GetMouseButtonDown(0))
        {
            if (PuedeLanzar())
            {
                ultimaFrutaLanzada = proximaFruta.LanzarFruta(spawnPoint.transform.position, padreSpawn.transform);
                colisionFrutaLanzada = ultimaFrutaLanzada.GetComponent<Colision>();
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

        return colisionFrutaLanzada.colisionSuelo;
    }

    bool PunteroSobreCapaUI()
    {
        PointerEventData eventos = new PointerEventData(EventSystem.current);
        eventos.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        List<RaycastResult> resultados = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventos, resultados);

        foreach (RaycastResult resultado in resultados)
        {
            if (capaUI == (capaUI | (1 << resultado.gameObject.layer)))
            {
                return true; 
            }
        }
        return false; 
    }
}

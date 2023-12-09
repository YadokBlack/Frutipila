using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colision : MonoBehaviour
{
    public int puntosConseguidos=0;
    private int doblepuntos;
    [SerializeField]
    private GameObject resultadoCombinarFruta;
    private bool colisionManejada = false;
    public GameObject efectoColision;
    public float tiempoEfecto = 3f;

    private ControlPuntos controlaPuntuacion;
    private SoundControl controlaSonidos;

    public bool colisionSuelo;

    private void Awake()
    {
        colisionSuelo = false;
        doblepuntos = puntosConseguidos + puntosConseguidos;
        controlaPuntuacion = FindObjectOfType<ControlPuntos>();
        controlaSonidos = FindObjectOfType<SoundControl>();
        if (controlaPuntuacion == null) Debug.LogError("No encontrado el componente ControlPuntos en Colision.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Objeto")) return;
        controlaSonidos.ReproduceSonidoColision();

        if (!colisionSuelo) 
        { 
            colisionSuelo = true;
        }

        if (!colisionManejada && other.gameObject.name == gameObject.name)
        {
            Colision otroScript = other.GetComponent<Colision>();
            if (!otroScript.EstaManejado())
            {
                otroScript.Utilizado();
                if (resultadoCombinarFruta != null) Combinar(other.gameObject);
            }            
        }    
    }

    private void Combinar(GameObject otroObjeto)
    {
        colisionManejada = true;
        
    //    GameObject efecto1 = Instantiate(efectoColision, transform.position, Quaternion.identity, transform.parent);
    //    GameObject efecto2 = Instantiate(efectoColision, otroObjeto.transform.position, Quaternion.identity, transform.parent);
        controlaSonidos.ReproduceSonidoCombinacion();
        Vector3 posicionMedia = (transform.position + otroObjeto.transform.position) / 2f;
        GameObject objetoCombinado = Instantiate(resultadoCombinarFruta, posicionMedia, Quaternion.identity, transform.parent);
        
        Destroy(gameObject);
        Destroy(otroObjeto);
    //    Destroy(efecto1,tiempoEfecto);
    //    Destroy(efecto2, tiempoEfecto);
        controlaPuntuacion.SumarPuntos(doblepuntos);
    }

    void Utilizado()
    {
        colisionManejada = true;
    }

    bool EstaManejado()
    {
        return colisionManejada;
    }
}

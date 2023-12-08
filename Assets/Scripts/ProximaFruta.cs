using System;
using UnityEngine;

public enum Fruta
{
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

public class ProximaFruta : MonoBehaviour
{
    public GameObject proximaFruta;
    public int velocidadCaidaFruta;
    public int cuantasNoPuedeLanzar;
    [SerializeField]
    private Fruta[] frutas;
    [SerializeField]
    private GameObject[] ListaFrutas;
    [SerializeField]
    private GameObject ubicacion;
    private Rigidbody2D rigidbodyFruta;
    public AnimationCurve curvaProbabilidad;
    private int ultimoNumeroGenerado = -1;    
    private Vector3 velocidadCaidaInicial;
    private SoundControl controlaSonidos;
    private int contador;

    const int frutaSinMostrar = 3;

    private void Awake()
    {
        contador = 0;
        controlaSonidos = FindObjectOfType<SoundControl>();
        velocidadCaidaInicial = new Vector3 (0, -velocidadCaidaFruta, 0);
        frutas = (Fruta[])Enum.GetValues(typeof(Fruta));
        GeneraFrutaAleatoria();
    }

    int GenerarNumeroSemiAleatorio(int maximo)
    {
        contador++;
        float valorAleatorio = UnityEngine.Random.value;
        float probabilidadNormalizada = curvaProbabilidad.Evaluate(valorAleatorio);

        if (ultimoNumeroGenerado == maximo) maximo -= 1;

        int numeroSemiAleatorio = Mathf.RoundToInt(probabilidadNormalizada * maximo);
        if (SeRepite(numeroSemiAleatorio))
        {
            if (ultimoNumeroGenerado == 1) return 0;         
            if (ultimoNumeroGenerado == 0) return 1;
            numeroSemiAleatorio = UnityEngine.Random.Range(0, ultimoNumeroGenerado - 1);
        }

        if (contador > 6)
        {
            numeroSemiAleatorio = UnityEngine.Random.Range(0, ListaFrutas.Length - frutaSinMostrar);
            contador = 0;
        }

        return numeroSemiAleatorio;
    }

    private bool SeRepite(int num)
    {
        return num == ultimoNumeroGenerado;
    }

    private void GeneraFrutaAleatoria()
    {
        int frutaNumero = GenerarNumeroSemiAleatorio(frutas.Length - cuantasNoPuedeLanzar);
        ultimoNumeroGenerado = frutaNumero;
        proximaFruta = Instantiate(ListaFrutas[frutaNumero], transform.position, Quaternion.identity, transform.parent);
        AjustarEscala(proximaFruta, gameObject);
        rigidbodyFruta = proximaFruta.GetComponent<Rigidbody2D>();
        rigidbodyFruta.isKinematic = true;        
    }

    void AjustarEscala(GameObject objetoInstanciado, GameObject objetoOriginal)
    {
        RectTransform rectTransformInstanciado = objetoInstanciado.GetComponent<RectTransform>();
        RectTransform rectTransformOriginal = objetoOriginal.GetComponent<RectTransform>();

        if (rectTransformInstanciado != null && rectTransformOriginal != null)
        {
            float widthOriginal = rectTransformOriginal.rect.width;
            float heightOriginal = rectTransformOriginal.rect.height;
            float scaleX = widthOriginal / rectTransformInstanciado.rect.width;
            float scaleY = heightOriginal / rectTransformInstanciado.rect.height;
            rectTransformInstanciado.localScale = new Vector3(scaleX, scaleY, 1f);
        }
        else
        {
            Debug.LogError("Los objetos deben tener un componente RectTransform para ajustar la escala en ProximaFruta");
        }
    }

    public GameObject LanzarFruta(Vector3 positition, Transform padre)
    {
        proximaFruta.transform.localScale = Vector3.one;
        rigidbodyFruta.velocity = velocidadCaidaInicial;
        rigidbodyFruta.isKinematic = false;
        proximaFruta.transform.SetParent(padre, false);
        proximaFruta.transform.position = positition;        
        controlaSonidos.ReproduceSonido(1);
        GameObject frutaLanzada = proximaFruta;
        GeneraFrutaAleatoria();
        return frutaLanzada;
    }
}

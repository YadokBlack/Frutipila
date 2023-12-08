using System.Collections;
using UnityEngine;

public class SeguirRatonHorizontal : MonoBehaviour
{
    public float velocidad = 6f;
    public float limiteDerechoNormalizado = 0.7f;  
    public float limiteIzquierdoNormalizado = 0.3f; 
    private Vector3 posicionDestino = Vector3.zero;
    public FinDeJuego AdministradorFinJuego;

    void Start()
    {
        Camera.main.orthographicSize = Screen.height / 2f;
    }

    void Update()
    {
        if (AdministradorFinJuego.GameOver()) return;

        if (DeberiaMoverConRaton()) ActualizarPosicionDestino();

        if (EstaLejosDelDestino()) MoverHaciaPosicionDestino();
    }

    private void MoverHaciaPosicionDestino()
    {
        transform.position = Vector3.Lerp(transform.position, posicionDestino, velocidad * Time.deltaTime);
    }

    private bool EstaLejosDelDestino()
    {
        return posicionDestino != Vector3.zero && Vector3.Distance(transform.position, posicionDestino) > 0.5f;
    }

    private bool DeberiaMoverConRaton()
    {
        return posicionDestino == Vector3.zero || Input.GetAxis("Mouse X") != 0f;
    }

    private void ActualizarPosicionDestino()
    {
        Vector3 posicionRaton = Input.mousePosition;
        posicionRaton.z = 0f;
        posicionRaton.y = transform.position.y;
        posicionRaton.x = Mathf.Clamp(posicionRaton.x, Screen.width * limiteIzquierdoNormalizado, Screen.width * limiteDerechoNormalizado);
        posicionDestino = posicionRaton;
    }
}

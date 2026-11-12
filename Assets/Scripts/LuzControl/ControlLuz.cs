using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ControlLuz : MonoBehaviour
{
    [Header("Luz")]
    public Light2D luz;

    public bool encendida = true;

    [Header("Linterna")]
    public bool tieneLinterna = false;

    [Header("Combustible")]
    public float combustibleMaximo = 100f;
    public float combustibleActual = 100f;

    public float consumoPorSegundo = 5f;

    void Start()
    {
        combustibleActual = combustibleMaximo;

        // Si aún no tiene la linterna, ocultarla
        if (!tieneLinterna)
        {
            encendida = false;
            luz.enabled = false;
        }
    }

    void Update()
    {
        // Si no tiene linterna, no puede usarla
        if (!tieneLinterna)
        {
            return;
        }

        // Consumir combustible
        if (encendida)
        {
            combustibleActual -=
                consumoPorSegundo * Time.deltaTime;

            combustibleActual =
                Mathf.Clamp(
                    combustibleActual,
                    0,
                    combustibleMaximo
                );

            // Sin combustible
            if (combustibleActual <= 0)
            {
                combustibleActual = 0;
                encendida = false;
                luz.enabled = false;
            }
        }

        // Encender o apagar
        if (Input.GetMouseButtonDown(1))
        {
            if (combustibleActual > 0)
            {
                encendida = !encendida;
                luz.enabled = encendida;
            }
        }
    }

    public void Recargar(float cantidad)
    {
        combustibleActual += cantidad;

        combustibleActual =
            Mathf.Clamp(
                combustibleActual,
                0,
                combustibleMaximo
            );
    }

    // Llamar cuando el jugador obtenga la linterna
    public void ObtenerLinterna()
    {
        tieneLinterna = true;

        encendida = true;
        luz.enabled = true;
    }
}
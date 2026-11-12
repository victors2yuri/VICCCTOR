using UnityEngine;

public class Mentora : MonoBehaviour
{
    private bool jugadorCerca;

    public string mensajeSinLinterna =
        "Las sombras son peligrosas. Toma esta linterna.";

    public string mensajeConLinterna =
        "No olvides vigilar el combustible.";

    void Update()
    {
        if (jugadorCerca &&
            Input.GetKeyDown(KeyCode.E))
        {
            Hablar();
        }
    }

    void Hablar()
    {
        ControlLuz luz =
            FindFirstObjectByType<ControlLuz>();

        if (luz == null)
            return;

        // Primera vez
        if (!luz.tieneLinterna)
        {
            Debug.Log(
                mensajeSinLinterna
            );

            luz.ObtenerLinterna();

            // NUEVO
            EstadoJuego.instancia
                .tieneLinterna = true;
        }
        else
        {
            Debug.Log(
                mensajeConLinterna
            );
        }
    }

    private void OnTriggerEnter2D(
        Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
        }
    }

    private void OnTriggerExit2D(
        Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
        }
    }
}

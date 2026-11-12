using UnityEngine;

public class LlaveAntigua : MonoBehaviour
{
    public AudioClip sonidoRecoger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (EstadoJuego.instancia != null)
        {
            EstadoJuego.instancia.tieneLlaveAntigua = true;
        }

        // Mostrar mensaje UI
        MensajeJuego mensaje =
            FindFirstObjectByType<MensajeJuego>();

        if (mensaje != null)
        {
            mensaje.MostrarMensaje(
                "Obtuviste la Llave Antigua"
            );
        }

        // Reproducir sonido
        AudioSource.PlayClipAtPoint(
            sonidoRecoger,
            transform.position
        );

        Destroy(gameObject);
    }
}

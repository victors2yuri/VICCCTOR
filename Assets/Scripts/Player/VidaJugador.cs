using UnityEngine;

public class VidaJugador : MonoBehaviour
{
    public int vidaMaxima = 100;
    public int vidaActual;

    public bool muerto = false;

    private PlayerController playerController;

    void Start()
    {
        vidaActual = vidaMaxima;

        playerController =
            GetComponent<PlayerController>();
    }

    public void RecibirDanio(int cantidad)
    {
        if (muerto)
            return;

        vidaActual -= cantidad;

        Debug.Log(
            "Vida actual: " + vidaActual
        );

        if (vidaActual <= 0)
        {
            Morir();
        }
    }
    public void RecuperarVida(int cantidad)
    {
        if (muerto)
            return;

        vidaActual += cantidad;

        if (vidaActual > vidaMaxima)
            vidaActual = vidaMaxima;

        Debug.Log("Vida recuperada: " + vidaActual);
    }

    void Morir()
    {
        muerto = true;

        Debug.Log("GAME OVER");

        if (playerController != null)
        {
            playerController.enabled = false;
        }

        Rigidbody2D rb =
            GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }
    }
}

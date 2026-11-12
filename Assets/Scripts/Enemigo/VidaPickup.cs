using UnityEngine;

public class VidaPickup : MonoBehaviour
{
    public int vidaRecuperada = 25;

    private void OnTriggerEnter2D(Collider2D other)
    {
        VidaJugador vida =
            other.GetComponent<VidaJugador>();

        if (vida != null)
        {
            vida.RecuperarVida(vidaRecuperada);
            Destroy(gameObject);
        }
    }
}
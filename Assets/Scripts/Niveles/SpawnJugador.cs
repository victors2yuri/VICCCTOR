using UnityEngine;

public class SpawnJugador : MonoBehaviour
{
    public string idSpawn;

    void Start()
    {
        if (
            TransicionNivel.puntoSpawn
            != idSpawn
        )
        {
            return;
        }

        GameObject jugador =
            GameObject.FindGameObjectWithTag(
                "Player"
            );

        if (jugador != null)
        {
            jugador.transform.position =
                transform.position;
        }
    }
}
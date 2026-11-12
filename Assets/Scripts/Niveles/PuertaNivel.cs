using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaNivel : MonoBehaviour
{
    public string escenaDestino;

    [Header("Spawn en escena destino")]
    public string spawnDestino;

    [Header("Requisitos")]
    public bool requiereLinterna;

    public bool requiereLlave;

    public int enemigosNecesarios;

    public bool requiereJefeDerrotado;

    private EstadoJuego estado;

    void Start()
    {
        estado =
            FindFirstObjectByType<EstadoJuego>();
    }

    private void OnTriggerEnter2D(
        Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        // LINERNA
        if (requiereLinterna)
        {
            if (!estado.tieneLinterna)
            {
                Debug.Log(
                    "Necesito la linterna."
                );

                return;
            }
        }

        // LLAVE
        if (requiereLlave)
        {
            if (!estado.tieneLlaveAntigua)
            {
                Debug.Log(
                    "Necesito una llave."
                );

                return;
            }
        }

        // ENEMIGOS
        if (estado.enemigosDerrotados <
            enemigosNecesarios)
        {
            Debug.Log(
                "Aún faltan enemigos."
            );

            return;
        }

        // JEFE
        if (requiereJefeDerrotado)
        {
            if (!estado.jefeDerrotado)
            {
                Debug.Log(
                    "Debo derrotar al jefe."
                );

                return;
            }
        }

        TransicionNivel.puntoSpawn =
            spawnDestino;

        SceneManager.LoadScene(
            escenaDestino
        );
    }
}
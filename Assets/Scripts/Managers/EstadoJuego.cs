using UnityEngine;

public class EstadoJuego : MonoBehaviour
{
    public static EstadoJuego instancia;

    [Header("Objetos importantes")]
    public bool tieneLinterna = false;

    public bool tieneLlaveAntigua = false;

    public bool jefeDerrotado = false;

    [Header("Progreso")]
    public int enemigosDerrotados = 0;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;

            DontDestroyOnLoad(
                gameObject
            );
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

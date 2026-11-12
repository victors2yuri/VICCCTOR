using UnityEngine;

public class EstadoEnemigoOscuridad : MonoBehaviour
{
    private OscuridadSombras oscuridad;
    private EnemigoOscuridad enemigo;

    public bool hostil;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
            return;

        oscuridad = player.GetComponentInChildren<OscuridadSombras>();
        enemigo = GetComponent<EnemigoOscuridad>();
    }

    void Update()
    {
        if (oscuridad == null || enemigo == null)
            return;

        // 1. leer estado de luz/oscuridad
        hostil = oscuridad.oscuridadExtrema;

        // 2. enviar estado al enemigo
        enemigo.SetHostilidad(hostil);
    }
}

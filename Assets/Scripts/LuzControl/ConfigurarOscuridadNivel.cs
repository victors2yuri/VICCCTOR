using UnityEngine;

public class ConfigurarOscuridadNivel : MonoBehaviour
{
    public bool activarOscuridad = true;

    void Start()
    {
        OscuridadSombras oscuridad =
            FindFirstObjectByType<OscuridadSombras>();

        if (oscuridad != null)
        {
            oscuridad.oscuridadActiva =
                activarOscuridad;
        }
    }
}

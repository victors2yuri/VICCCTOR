using UnityEngine;

public class MinijuegoExpulsion
    : MonoBehaviour
{
    public bool activo;

    public int pulsaciones = 10;

    private int contador;


    public void Activar()
    {
        activo = true;

        contador = 0;

        Debug.Log(
        "¡Pulsa E rápidamente!"
        );
    }


    void Update()
    {
        if (!activo)
            return;


        if (
        Input.GetKeyDown(
        KeyCode.E
        ))
        {
            contador++;

            Debug.Log(

            contador

            + "/"

            + pulsaciones

            );


            if (
            contador >=
            pulsaciones
            )
            {
                activo = false;

                Debug.Log(
                "Escapaste"
                );
            }
        }
    }
}
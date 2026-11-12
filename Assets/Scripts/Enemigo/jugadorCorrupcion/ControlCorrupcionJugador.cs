using UnityEngine;
using System.Collections;

public class ControlCorrupcionJugador
    : MonoBehaviour
{
    public bool corrompido;

    public float duracion = 8f;

    public int danoDirecto = 15;

    private Coroutine rutina;


    public void Corromper()
    {
        if (corrompido)
            return;

        if (rutina != null)
        {
            StopCoroutine(
                rutina
            );
        }

        rutina =
        StartCoroutine(
        Corrupcion()
        );
    }


    IEnumerator Corrupcion()
    {
        corrompido = true;

        Debug.Log(
        "La sombra controla tu mente"
        );

        yield return new WaitForSeconds(
        duracion
        );

        corrompido = false;

        Debug.Log(
        "Has recuperado el control"
        );
    }


    public void RecibirAtaqueDirecto()
    {
        VidaJugador vida =

        GetComponent<VidaJugador>();


        if (vida != null)
        {
            vida.RecibirDanio(
            danoDirecto
            );
        }
    }
}
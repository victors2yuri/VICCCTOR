using UnityEngine;
using TMPro;
using System.Collections;

public class MensajeJuego : MonoBehaviour
{
    public GameObject panelMensaje;

    public TextMeshProUGUI textoMensaje;

    public float tiempoVisible = 2f;

    public void MostrarMensaje(string mensaje)
    {
        StopAllCoroutines();

        textoMensaje.text = mensaje;

        panelMensaje.SetActive(true);

        StartCoroutine(
            OcultarMensaje()
        );
    }

    IEnumerator OcultarMensaje()
    {
        yield return new WaitForSeconds(
            tiempoVisible
        );

        panelMensaje.SetActive(false);
    }
}
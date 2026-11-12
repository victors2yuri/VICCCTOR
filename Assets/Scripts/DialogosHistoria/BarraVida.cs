using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;


public class BarraVida : MonoBehaviour
{
    public Image rellenoBarraVida;
    private VidaJugador vidaJugador;
    private float vidaMaxima;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vidaJugador = GameObject.Find("Jugador").GetComponent<VidaJugador>();
        vidaMaxima = vidaJugador.vidaActual;
    }

    // Update is called once per frame
    void Update()
    {
        rellenoBarraVida.fillAmount = vidaJugador.vidaActual / vidaMaxima;
    }
}

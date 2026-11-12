using UnityEngine;
using UnityEngine.UI;

public class BarraCombustible : MonoBehaviour
{
    public Image rellenoBarraCombustible;

    private ControlLuz controlLuz;
    private float combustibleMaximo;

    void Start()
    {
        controlLuz = GameObject.Find("Jugador").GetComponent<ControlLuz>();

        combustibleMaximo = controlLuz.combustibleMaximo;
    }

    void Update()
    {
        rellenoBarraCombustible.fillAmount =
            controlLuz.combustibleActual / combustibleMaximo;
    }
}

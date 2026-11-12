using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LuzParpadeante : MonoBehaviour
{
    private Light2D luz;

    [SerializeField] private float intensidadMin = 0.5f;
    [SerializeField] private float intensidadMax = 1.5f;
    [SerializeField] private float velocidad = 0.05f;

    private void Start()
    {
        luz = GetComponent<Light2D>();
        InvokeRepeating(nameof(Parpadear), 0f, velocidad);
    }

    private void Parpadear()
    {
        luz.intensity = Random.Range(intensidadMin, intensidadMax);
    }
}

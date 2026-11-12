using UnityEngine;

public class MentoraMovimiento : MonoBehaviour
{
    public float distanciaMaxima = 2f;
    public float velocidad = 1f;

    private Vector3 origen;

    private Vector3 destino;

    void Start()
    {
        origen = transform.position;

        ElegirDestino();
    }

    void Update()
    {
        transform.position =
            Vector3.MoveTowards(
                transform.position,
                destino,
                velocidad * Time.deltaTime
            );

        if (
            Vector3.Distance(
                transform.position,
                destino
            ) < 0.1f
        )
        {
            ElegirDestino();
        }
    }

    void ElegirDestino()
    {
        float x =
            Random.Range(
                -distanciaMaxima,
                distanciaMaxima
            );

        destino =
            origen +
            new Vector3(
                x,
                0,
                0
            );
    }
}
using UnityEngine;

public class SombraIA : MonoBehaviour
{
    public Transform jugador;
    public ControlLuz controlLuz;

    public float velocidad = 1.5f;

    void Update()
    {
        // Si la linterna está encendida, la sombra desaparece
        if (controlLuz.encendida)
        {
            gameObject.SetActive(false);
            return;
        }

        // Si la linterna está apagada, aparece
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        transform.position = Vector2.MoveTowards(
            transform.position,
            jugador.position,
            velocidad * Time.deltaTime
        );
    }
}
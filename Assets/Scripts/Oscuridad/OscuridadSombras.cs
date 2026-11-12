using UnityEngine;

public class OscuridadSombras : MonoBehaviour
{
    public ControlLuz controlLuz;
    public SpriteRenderer spriteRenderer;
    public Sprite[] sombras;

    [Header("Activación")]
    public bool oscuridadActiva = true;

    // NUEVA VARIABLE
    public bool oscuridadExtrema;

    [Header("Oscuridad")]
    public float tiempoPorNivel = 3f;
    public float velocidadRetroceso = 3f;

    [Header("Desvanecido")]
    public bool usarDesvanecido = true;
    public float alphaMinimo = 0.92f;
    public float alphaMaximo = 1f;

    [Header("Daño")]
    public int danoPorGolpe = 10;

    [Header("Referencia")]
    public VidaJugador vidaJugador;

    [Header("Corazón")]
    public float velocidadInicial = 1f;
    public float aumentoVelocidad = 0.3f;
    public float velocidadMaxima = 6f;

    private float tiempoOscuridad;

    private int palpitaciones = 0;

    private float velocidadActual;

    private float ultimoValorSeno;

    void Start()
    {
        velocidadActual = velocidadInicial;

        if (vidaJugador == null)
        {
            vidaJugador =
                FindFirstObjectByType<VidaJugador>();
        }
    }

    void Update()
    {
        // NIVEL SEGURO (Nivel 1)
        if (!oscuridadActiva)
        {
            oscuridadExtrema = false;

            tiempoOscuridad = 0;

            if (sombras.Length > 0)
            {
                spriteRenderer.sprite =
                    sombras[0];
            }

            Color c =
                spriteRenderer.color;

            c.a = 1f;

            spriteRenderer.color = c;

            palpitaciones = 0;

            velocidadActual =
                velocidadInicial;

            return;
        }

        // Si el jugador murió
        if (vidaJugador != null
            && vidaJugador.muerto)
        {
            oscuridadExtrema = false;

            spriteRenderer.sprite =
                sombras[
                sombras.Length - 1
                ];

            Color negro =
                spriteRenderer.color;

            negro.a = 1f;

            spriteRenderer.color =
                negro;

            return;
        }

        // Avanza oscuridad
        if (!controlLuz.encendida)
        {
            tiempoOscuridad +=
                Time.deltaTime;
        }
        else
        {
            tiempoOscuridad -=
                Time.deltaTime *
                velocidadRetroceso;
        }

        tiempoOscuridad =
            Mathf.Clamp(

            tiempoOscuridad,

            0,

            tiempoPorNivel *
            (sombras.Length - 1)
            );

        // Seleccionar sprite

        int nivel =
            Mathf.FloorToInt(

            tiempoOscuridad /
            tiempoPorNivel
            );

        nivel =
            Mathf.Clamp(

            nivel,

            0,

            sombras.Length - 1
            );

        spriteRenderer.sprite =
            sombras[nivel];

        // OSCURIDAD MÁXIMA
        if (!controlLuz.encendida &&
            usarDesvanecido &&
            nivel >= sombras.Length - 1)
        {
            oscuridadExtrema = true;

            Color c =
                spriteRenderer.color;

            float seno =

                Mathf.Sin(

                Time.time *
                velocidadActual
                );

            float alpha =

                Mathf.Lerp(

                alphaMinimo,

                alphaMaximo,

                (seno + 1f)
                * 0.5f
                );

            c.a = alpha;

            spriteRenderer.color = c;

            // Detectar palpitación
            if (ultimoValorSeno < 0f
                && seno >= 0f)
            {
                palpitaciones++;

                // Cada 3 palpitaciones
                if (palpitaciones >= 3)
                {
                    palpitaciones = 0;

                    if (vidaJugador != null)
                    {
                        vidaJugador
                        .RecibirDanio(
                        danoPorGolpe
                        );
                    }

                    // Acelerar corazón

                    velocidadActual +=
                        aumentoVelocidad;

                    velocidadActual =
                        Mathf.Min(

                        velocidadActual,

                        velocidadMaxima
                        );
                }
            }

            ultimoValorSeno =
                seno;
        }

        // NO ESTÁ EN OSCURIDAD MÁXIMA
        else
        {
            oscuridadExtrema = false;

            Color c =
                spriteRenderer.color;

            c.a = 1f;

            spriteRenderer.color = c;

            palpitaciones = 0;

            velocidadActual =
                velocidadInicial;
        }
    }
}
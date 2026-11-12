using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;

    public float velocidadCorrer = 8f;

    public float fuerzaSalto = 15f;


    [Header("Suelo")]
    public Transform puntoSuelo;

    public float radioSuelo = 0.5f;

    public LayerMask capaSuelo;

    [Header("Ataque")]
    public float rangoAtaque = 1.5f;
    public int dañoAntorcha = 25;
    public LayerMask enemigos;

    private ControlLuz controlLuz;

    [Header("Arma")]
    public GameObject arma;


    private Rigidbody2D rb;

    private bool enSuelo;

    private float movimiento;


    private ControlCorrupcionJugador
        corrupcion;


    void Start()
    {
        controlLuz = GetComponent<ControlLuz>();
        rb =
            GetComponent<Rigidbody2D>();


        corrupcion =
            GetComponent
            <ControlCorrupcionJugador>();
    }


    void Update()
    {
        movimiento =

            Input.GetAxisRaw(
            "Horizontal"
            );


        // Invertir controles
        if (
            corrupcion != null &&
            corrupcion.corrompido
           )
        {
            movimiento *= -1;
        }


        // Girar personaje
        if (movimiento > 0)
        {
            transform.localScale =
                new Vector3(
                1,
                1,
                1
                );
        }

        else if (movimiento < 0)
        {
            transform.localScale =
                new Vector3(
                -1,
                1,
                1
                );
        }


        // Suelo
        if (puntoSuelo != null)
        {
            enSuelo =

                Physics2D
                .OverlapCircle(

                puntoSuelo.position,

                radioSuelo,

                capaSuelo

                );
        }


        // Salto
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Presionaste espacio. ¿Está en suelo?: " + enSuelo);

            if (enSuelo)
            {
                rb.linearVelocity = new Vector2(
                    rb.linearVelocity.x,
                    0f
                );

                rb.AddForce(
                    Vector2.up * fuerzaSalto,
                    ForceMode2D.Impulse
                );

                Debug.Log("¡SALTANDO!");
            }
        }


        // Ataque
        if (            Input.GetMouseButtonDown(        0           )       )
        {
            Atacar();
            Debug.Log("Ataque");
        }
    }


    void FixedUpdate()
    {
        float velocidadActual =
            velocidad;


        if (
            Input.GetKey(
            KeyCode.LeftShift
            )
           )
        {
            velocidadActual =
                velocidadCorrer;
        }


        rb.linearVelocity =

            new Vector2(

            movimiento *

            velocidadActual,

            rb.linearVelocity.y

            );
    }


    private void OnDrawGizmos()
    {
        if (puntoSuelo != null)
        {
            Gizmos.color =
                Color.red;


            Gizmos.DrawWireSphere(

                puntoSuelo.position,

                radioSuelo

                );
        }
    }
    void Atacar()
    {
        if (controlLuz == null || !controlLuz.encendida)
        {
            Debug.Log("No puedes atacar sin luz");
            return;
        }

        Vector2 posicionAtaque = arma.transform.position;

        Collider2D[] enemigosGolpeados = Physics2D.OverlapCircleAll(
    posicionAtaque,
    rangoAtaque,
    enemigos
);

        Debug.Log(
        "Detectados: " +
        enemigosGolpeados.Length
        );

        foreach (Collider2D col in enemigosGolpeados)
        {
            Debug.Log(
            "Golpeando: " +
            col.name
            );

            EnemigoOscuridad enemigo =
                col.GetComponent<EnemigoOscuridad>();

            if (enemigo != null)
            {
                enemigo.RecibirDaño(
                dañoAntorcha
                );
            }
        }

        Debug.Log("Ataque con antorcha");
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoAtaque);
    }
}
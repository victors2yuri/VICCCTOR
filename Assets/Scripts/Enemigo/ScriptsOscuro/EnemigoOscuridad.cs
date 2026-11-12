using UnityEngine;
using System.Collections;

public class EnemigoOscuridad : MonoBehaviour
{
    private Collider2D col;
    private Rigidbody2D rb;

    [Header("Vida")]
    public int vidaMax = 3;
    private int vidaActual;

    [Header("Patrulla Simple")]
    public float velocidadPatrulla = 1.5f;
    public float tiempoMinPatrulla = 1.5f;
    public float tiempoMaxPatrulla = 3f;

    private float tiempoPatrulla;
    private float contadorPatrulla;
    private int direccionPatrulla = 1; // 1 derecha, -1 izquierda

    [Header("Movimiento")]
    public float velocidadMin = 2f;
    public float velocidadMax = 5f;
    private float velocidad;

    [Header("Modo Luz")]
    public bool hostil = true;
    public float velocidadEscape = 4f;
    public float distanciaSegura = 2.5f;

    [Header("IA Miedo")]
    public float zonaMiedo = 4f;
    public float reaccionSuave = 2f;
    private float miedoActual;

    [Header("Minijuego")]
    public float tiempoMinijuego = 5f;

    [Header("Combustible")]
    public GameObject prefabCombustible;
    [Range(0, 100)]
    public int probabilidad = 45;

    [Header("Vida")]
    public GameObject prefabCorazon;

    [Header("Expulsión")]
    public float distanciaExpulsion = 3f;
    public float alturaExpulsion = 0.8f;
    public float tiempoExpulsion = 0.5f;
    public float velocidadDesvanecer = 0.7f;

    private Transform jugador;
    private Transform cuerpoJugador;

    private bool pegado;

    void Start()
    {
        Debug.Log("EstadoJuego encontrado: " + EstadoJuego.instancia);
        vidaActual = vidaMax;

        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        velocidad = Random.Range(velocidadMin, velocidadMax);

        IniciarPatrulla();
        BuscarJugador();
    }

    void Update()
    {
        if (jugador == null)
        {
            BuscarJugador();
            return;
        }

        // =========================
        // 🌤 MODO NO HOSTIL (HUYE INTELIGENTE)
        // =========================
        if (!hostil)
        {
            float distancia = Vector2.Distance(transform.position, jugador.position);

            miedoActual = Mathf.Lerp(
                miedoActual,
                Mathf.Clamp01(1 - (distancia / zonaMiedo)),
                Time.deltaTime * reaccionSuave
            );

            bool meVe = distancia < distanciaSegura;

            if (meVe)
            {
                // 👇 TU SISTEMA DE HUÍDA ACTUAL (NO TOCADO)
                Vector2 direccion =
                    (transform.position - jugador.position).normalized;

                Vector2 lateral = new Vector2(-direccion.y, direccion.x);

                Vector2 direccionFinal =
                    (direccion + lateral * Random.Range(-0.4f, 0.4f)).normalized;

                transform.position +=
                    (Vector3)(direccionFinal * velocidadEscape * miedoActual * Time.deltaTime);
            }
            else
            {
                // 👇 PATRULLA SIMPLE IZQ ↔ DER
                Patrullar();
            }

            return;
        }
        
        // =========================
        // 🌑 MODO HOSTIL (PERSECUCIÓN)
        // =========================
        if (pegado)
        {
            if (cuerpoJugador != null)
            {
                transform.position =
                    cuerpoJugador.position + new Vector3(0.3f, 0.5f, 0);
            }
            return;
        }

        transform.position = Vector2.MoveTowards(
            transform.position,
            jugador.position,
            velocidad * Time.deltaTime
        );

        transform.localScale = (jugador.position.x > transform.position.x)
            ? new Vector3(1, 1, 1)
            : new Vector3(-1, 1, 1);
    }

    void BuscarJugador()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");

        if (obj != null)
            jugador = obj.transform;
    }

    // =========================
    // COLISIÓN HOSTIL
    // =========================
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hostil) return;
        if (pegado) return;

        GameObject player = other.transform.root.gameObject;

        if (!player.CompareTag("Player")) return;

        pegado = true;
        cuerpoJugador = player.transform;

        StartCoroutine(Invasion(player));
    }

    // =========================
    // INVASIÓN / MINIJUEGO
    // =========================
    IEnumerator Invasion(GameObject player)
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }

        ControlCorrupcionJugador corrupcion =
            player.GetComponent<ControlCorrupcionJugador>();

        if (corrupcion == null)
        {
            Destroy(gameObject);
            yield break;
        }

        if (corrupcion.corrompido)
        {
            yield return new WaitForSeconds(1f);

            corrupcion.RecibirAtaqueDirecto();

            Destroy(gameObject);
            yield break;
        }

        MinijuegoExpulsion mini =
            player.GetComponent<MinijuegoExpulsion>();

        if (mini == null)
        {
            Destroy(gameObject);
            yield break;
        }

        mini.Activar();

        float tiempo = 0;

        while (tiempo < tiempoMinijuego)
        {
            if (!mini.activo)
            {
                yield return StartCoroutine(ExpulsarSombra());
                yield break;
            }

            tiempo += Time.deltaTime;
            yield return null;
        }

        mini.activo = false;

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        if (sprite != null)
            sprite.enabled = false;

        yield return new WaitForSeconds(0.5f);

        corrupcion.Corromper();

        Destroy(gameObject);
    }

    // =========================
    // EXPULSIÓN
    // =========================
    IEnumerator ExpulsarSombra()
    {
        pegado = false;
        cuerpoJugador = null;

        if (col != null)
            col.enabled = false;

        float direccion =
            (transform.position.x > jugador.position.x) ? 1f : -1f;

        Vector3 inicio = transform.position;
        Vector3 destino = transform.position +
            new Vector3(direccion * distanciaExpulsion, alturaExpulsion, 0);

        float t = 0;

        while (t < tiempoExpulsion)
        {
            t += Time.deltaTime;

            transform.position = Vector3.Lerp(
                inicio,
                destino,
                t / tiempoExpulsion
            );

            yield return null;
        }

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        if (sprite != null)
        {
            Color c = sprite.color;

            while (c.a > 0)
            {
                c.a -= Time.deltaTime * velocidadDesvanecer;
                sprite.color = c;
                yield return null;
            }
        }

        Recompensa();
        Destroy(gameObject);
    }

    // =========================
    // RECOMPENSA
    // =========================
    void Recompensa()
    {
        int numero = Random.Range(0, 100);

        if (numero <= probabilidad && prefabCombustible != null)
        {
            Instantiate(prefabCombustible, transform.position, Quaternion.identity);
        }
    }

    // =========================
    // MUERTE POR LUZ
    // =========================
    public void MorirConLuz()
    {
        Debug.Log("ENTRE A MORIRCONLUZ");
        if (prefabCorazon != null)
        {
            Instantiate(prefabCorazon, transform.position, Quaternion.identity);
        }
        if (EstadoJuego.instancia != null)
        {
            EstadoJuego.instancia.enemigosDerrotados++;

            Debug.Log(
                "Enemigos derrotados: "
                + EstadoJuego.instancia.enemigosDerrotados
            );
        }

        Destroy(gameObject);
    }

    // =========================
    // CAMBIO DE ESTADO
    // =========================
    public void SetHostilidad(bool estado)
    {
        hostil = estado;

        if (col != null)
            col.isTrigger = hostil;

        if (!hostil)
        {
            pegado = false;
        }
    }
    void IniciarPatrulla()
    {
        tiempoPatrulla = Random.Range(tiempoMinPatrulla, tiempoMaxPatrulla);
        contadorPatrulla = 0f;

        // dirección inicial aleatoria
        direccionPatrulla = (Random.value > 0.5f) ? 1 : -1;
    }
    void Patrullar()
    {
        contadorPatrulla += Time.deltaTime;

        transform.position += new Vector3(
            direccionPatrulla * velocidadPatrulla * Time.deltaTime,
            0,
            0
        );

        // cuando termina el tiempo → cambia dirección
        if (contadorPatrulla >= tiempoPatrulla)
        {
            IniciarPatrulla();
        }

        // flip visual
        transform.localScale = (direccionPatrulla > 0)
            ? new Vector3(1, 1, 1)
            : new Vector3(-1, 1, 1);
    }
    public void RecibirDaño(int daño)
    {
        Debug.Log("ME PEGARON");

        /*if (!hostil)
        {
            Debug.Log("No recibo daño porque no soy hostil");
            return;
        }*/

        vidaActual -= daño;

        Debug.Log("Vida restante: " + vidaActual);

        StartCoroutine(FeedbackGolpe());

        if (vidaActual <= 0)
        {
            Morir();
        }
    }
    void Morir()
    {
        Debug.Log("ENTRE A MORIR");
        if (vidaActual <= 0)
        {
            MorirConLuz();
        }
    }
    IEnumerator FeedbackGolpe()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (sr == null)
            yield break;

        Color original = sr.color;

        // rojo rápido
        sr.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        // parpadeo
        for (int i = 0; i < 3; i++)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(0.05f);

            sr.enabled = true;
            yield return new WaitForSeconds(0.05f);
        }

        sr.color = original;
    }
}
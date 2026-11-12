using UnityEngine;

public class SpawnEnemigosOscuridad : MonoBehaviour
{
    [Header("Referencias")]
    private OscuridadSombras oscuridad;

    private Camera camara;

    [Header("Prefab")]
    public GameObject prefabSombra;

    [Header("Spawn")]
    public float tiempoEntreSpawns = 8f;

    [Header("Límite")]
    public int maximoEnemigos = 4;

    [Header("Suelo")]
    public LayerMask capaSuelo;

    public float alturaRaycast = 4f;

    private float temporizador;

    void Update()
    {
        // Buscar referencias
        if (oscuridad == null ||
            camara == null)
        {
            BuscarReferencias();

            return;
        }

        // Solo cuando existe oscuridad extrema
        if (!oscuridad.oscuridadExtrema)
        {
            temporizador = 0;

            return;
        }

        // Contar enemigos y sombras
        int cantidad =

            FindObjectsByType
            <EnemigoOscuridad>(

            FindObjectsSortMode.None

            ).Length

            +

            FindObjectsByType
            <SombraEnemigo>(

            FindObjectsSortMode.None

            ).Length;

        // Limitar cantidad
        if (cantidad >=
            maximoEnemigos)
        {
            return;
        }

        temporizador +=
            Time.deltaTime;

        if (temporizador >=
            tiempoEntreSpawns)
        {
            temporizador = 0;

            SpawnSombra();
        }
    }

    void BuscarReferencias()
    {
        GameObject jugador =

            GameObject.FindGameObjectWithTag(
            "Player"
            );

        if (jugador == null)
            return;


        oscuridad =

            jugador.GetComponentInChildren
            <OscuridadSombras>();


        camara =

            jugador.GetComponentInChildren
            <Camera>();
    }

    void SpawnSombra()
    {
        float ancho =

            camara.orthographicSize *

            camara.aspect;


        float x =

            Random.Range(

            camara.transform.position.x
            - ancho + 1f,

            camara.transform.position.x
            + ancho - 1f
            );


        Vector2 origen =

            new Vector2(

            x,

            camara.transform.position.y
            + alturaRaycast
            );


        RaycastHit2D hit =

            Physics2D.Raycast(

            origen,

            Vector2.down,

            100f,

            capaSuelo
            );


        if (hit.collider == null)
            return;


        Instantiate(

            prefabSombra,

            hit.point,

            Quaternion.identity
        );
    }
}
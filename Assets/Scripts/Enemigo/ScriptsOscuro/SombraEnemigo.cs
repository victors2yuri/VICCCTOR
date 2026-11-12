using UnityEngine;
using System.Collections;

public class SombraEnemigo : MonoBehaviour
{
    [Header("Enemigo")]
    public GameObject enemigoReal;

    [Header("Tiempo")]
    public float tiempoSalir = 3f;

    [Header("Movimiento")]
    public float amplitud = 1f;

    public float velocidadMovimiento = 4f;

    [Header("Suelo")]
    public float distanciaDebajo = 0.7f;

    [Header("Salida del enemigo")]
    public float alturaAparicionEnemigo = 0.8f;


    private Transform jugador;

    private float tiempo;

    private float yBloqueada;


    void Start()
    {
        GameObject obj =

            GameObject
            .FindGameObjectWithTag(
            "Player"
            );


        if (obj != null)
        {
            jugador =
                obj.transform;


            // Bloquea la Y una sola vez
            yBloqueada =

                jugador.position.y

                -

                distanciaDebajo;
        }


        StartCoroutine(
            SalirDelSuelo()
        );
    }


    void Update()
    {
        if (jugador == null)
            return;


        tiempo +=
            Time.deltaTime;


        float movimientoX =

            Mathf.Sin(

            tiempo *

            velocidadMovimiento

            )

            *

            amplitud;


        Vector3 posicion =

            transform.position;


        // Sigue al jugador en X

        posicion.x =

            jugador.position.x

            +

            movimientoX;


        // Mantiene la Y fija

        posicion.y =
            yBloqueada;


        transform.position =
            posicion;
    }


    IEnumerator SalirDelSuelo()
    {
        yield return new WaitForSeconds(
            tiempoSalir
        );


        Vector3 posicionSpawn =

            transform.position +

            new Vector3(

            0,

            alturaAparicionEnemigo,

            0

            );


        Instantiate(

            enemigoReal,

            posicionSpawn,

            Quaternion.identity

            );


        Destroy(
            gameObject
        );
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float velocidad;
    public float fuerzaSalto;
    public float saltosMaximos;
    public LayerMask capaSuelo;

    private Rigidbody2D rigidBody;
    private CapsuleCollider2D capsuleCollider;
    private Animator animator; // <-- Nueva variable para el Animator

    private bool mirandoDerecha = true;
    private float saltosRestantes;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>(); // <-- Buscamos el Animator

        saltosRestantes = saltosMaximos;
    }

    void Update()
    {
        ProcesarMovimiento();
        ProcesarSalto();
        ActualizarAnimator(); // <-- Nueva función para controlar las animaciones
    }

    bool EstaEnSuelo()
    {
        RaycastHit2D raycastHit = Physics2D.CapsuleCast(
            capsuleCollider.bounds.center,
            capsuleCollider.size,
            capsuleCollider.direction,
            0f,
            Vector2.down,
            0.2f,
            capaSuelo
        );

        return raycastHit.collider != null;
    }

    void ProcesarSalto()
    {
        if (EstaEnSuelo())
        {
            saltosRestantes = saltosMaximos;
        }

        if (Input.GetKeyDown(KeyCode.Space) && saltosRestantes > 0)
        {
            saltosRestantes--;
            rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, 0f);
            rigidBody.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }
    }

    void ProcesarMovimiento()
    {
        float inputMovimiento = Input.GetAxis("Horizontal");
        rigidBody.linearVelocity = new Vector2(inputMovimiento * velocidad, rigidBody.linearVelocity.y);
        GestionarOrientacion(inputMovimiento);
    }

    void GestionarOrientacion(float inputMovimiento)
    {
        if ((mirandoDerecha == true && inputMovimiento < 0) || (mirandoDerecha == false && inputMovimiento > 0))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    // --- NUEVA FUNCIÓN ---
    void ActualizarAnimator()
    {
        // 1. Enviamos el valor absoluto de la velocidad horizontal (siempre positivo)
        animator.SetFloat("velocidadX", Mathf.Abs(rigidBody.linearVelocity.x));

        // 2. Enviamos la velocidad vertical (positivo al subir, negativo al caer)
        animator.SetFloat("velocidadY", rigidBody.linearVelocity.y);

        // 3. Enviamos si está tocando el suelo o no
        animator.SetBool("enSuelo", EstaEnSuelo());
    }
}
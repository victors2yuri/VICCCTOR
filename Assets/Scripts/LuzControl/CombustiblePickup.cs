using UnityEngine;

public class CombustiblePickup : MonoBehaviour
{
    public float combustibleRecuperado = 25f;

    private void OnTriggerEnter2D(
        Collider2D other)
    {
        ControlLuz luz =
            other.GetComponent<ControlLuz>();

        if (luz != null)
        {
            luz.Recargar(
                combustibleRecuperado
            );

            Destroy(gameObject);
        }
    }
}
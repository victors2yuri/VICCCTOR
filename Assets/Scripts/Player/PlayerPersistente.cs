using UnityEngine;

public class PlayerPersistente : MonoBehaviour
{
    private static PlayerPersistente instancia;

    void Awake()
    {
        if (instancia != null)
        {
            Destroy(gameObject); // destruye el duplicado
            return;
        }

        instancia = this;
        DontDestroyOnLoad(gameObject);
    }
}

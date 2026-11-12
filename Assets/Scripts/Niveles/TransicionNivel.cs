using UnityEngine;

public class TransicionNivel : MonoBehaviour
{
    public static string puntoSpawn = "";

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
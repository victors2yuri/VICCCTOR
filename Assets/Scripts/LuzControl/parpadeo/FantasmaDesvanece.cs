using UnityEngine;

public class FantasmaDesvanece : MonoBehaviour
{
    private SpriteRenderer sr;

    [SerializeField] private float alphaMin = 0.1f;
    [SerializeField] private float alphaMax = 0.8f;
    [SerializeField] private float velocidad = 1.5f;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float alpha = Mathf.Lerp(
            alphaMin,
            alphaMax,
            (Mathf.Sin(Time.time * velocidad) + 1f) / 2f
        );

        Color color = sr.color;
        color.a = alpha;
        sr.color = color;
    }
}
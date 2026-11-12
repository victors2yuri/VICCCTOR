using UnityEngine;
using System.Collections;
using TMPro;

public class Dialogos1 : MonoBehaviour
{
    [SerializeField] private GameObject icono;
    [SerializeField] private GameObject dialogoPanel;
    [SerializeField] private TMP_Text dialogoText;
    [SerializeField, TextArea(4, 6)] private string[] mensaje;

    [SerializeField] private Transform jugador;

    private bool isPlayerRange;
    private bool didDialogoStart;
    private int lineIndex;

    private float typingTime = 0.05f;

    void Update()
    {
        if (isPlayerRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!didDialogoStart)
            {
                StartDialogo();
            }
            else if (dialogoText.text == mensaje[lineIndex])
            {
                NextDialogoLine();
            }
            else
            {
                StopAllCoroutines();
                dialogoText.text = mensaje[lineIndex];
            }
        }
    }

    private void StartDialogo()
    {
        didDialogoStart = true;
        dialogoPanel.SetActive(true);
        icono.SetActive(false);

        lineIndex = 0;

        // Despega la cámara del jugador
        Camera.main.transform.SetParent(null);

        // Mueve la cámara al NPC
        Camera.main.transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            Camera.main.transform.position.z
        );

        Time.timeScale = 0f;

        StartCoroutine(ShowLine());
    }

    private void NextDialogoLine()
    {
        lineIndex++;

        if (lineIndex < mensaje.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogoStart = false;
            dialogoPanel.SetActive(false);
            icono.SetActive(true);

            // La cámara vuelve a ser hija del jugador
            Camera.main.transform.SetParent(jugador);

            // Ajusta esto si tu cámara tiene otra posición local
            Camera.main.transform.localPosition = new Vector3(0, 0, -10);

            Time.timeScale = 1f;
        }
    }

    private IEnumerator ShowLine()
    {
        dialogoText.text = string.Empty;

        foreach (char letra in mensaje[lineIndex])
        {
            dialogoText.text += letra;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerRange = true;
            icono.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerRange = false;
            icono.SetActive(false);
        }
    }
}

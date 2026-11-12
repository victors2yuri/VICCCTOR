using UnityEngine;
using System.Collections;
using TMPro;

public class Dialogos : MonoBehaviour
{
    [ SerializeField ] private GameObject icono;
    [ SerializeField ] private GameObject dialogoPanel;
    [ SerializeField ] private TMP_Text dialogoText;
    [ SerializeField, TextArea(4, 6)] private string[] mensaje;
    private bool isPlayerRange;
    private bool didDialogoStart;
    private int lineIndex;

    private float typingTime = 0.05f;

    void Update()
    {
        if (isPlayerRange && Input.GetKeyDown(KeyCode.E))
        {
            if(!didDialogoStart)
            {
                StartDialogo();
            }
            else if(dialogoText.text == mensaje[lineIndex])
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
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());

    }
    private void NextDialogoLine()
    {
        lineIndex++;
        if(lineIndex < mensaje.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogoStart=false;
            dialogoPanel.SetActive(false); 
            icono.SetActive(true);
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
        isPlayerRange = true;
        icono.SetActive(true); 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerRange = false;
        icono.SetActive(false);
    }
}

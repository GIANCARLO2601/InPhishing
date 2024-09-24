using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    public string[] dialogueLines; // Las líneas de diálogo que el NPC dirá
    public TextMeshProUGUI dialogueText; // Referencia al objeto UI que mostrará el diálogo
    public GameObject interactionIndicator; // El ícono o señal visual de interacción

    private bool isPlayerInRange = false; // Para saber si el jugador está cerca

    private void Start()
    {
        dialogueText.gameObject.SetActive(false); // Esconder el diálogo al iniciar
        interactionIndicator.SetActive(false); // Esconder la señal de interacción al iniciar
    }

    private void Update()
    {
        // Solo mostrar el diálogo si el jugador está dentro del rango y presiona "E"
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ShowDialogue());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // El jugador está dentro del rango del NPC
            interactionIndicator.SetActive(true); // Mostrar la señal visual
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // El jugador salió del rango del NPC
            interactionIndicator.SetActive(false); // Esconder la señal visual
            dialogueText.gameObject.SetActive(false); // Esconder el diálogo si el jugador se aleja
        }
    }

    private IEnumerator ShowDialogue()
    {
        interactionIndicator.SetActive(false); // Esconder la señal mientras el diálogo está activo
        dialogueText.gameObject.SetActive(true); // Mostrar el diálogo

        // Mostrar cada línea de diálogo con una pequeña pausa
        foreach (string line in dialogueLines)
        {
            dialogueText.text = line;
            yield return new WaitForSeconds(3); // Pausa de 3 segundos entre cada línea
        }

        dialogueText.gameObject.SetActive(false); // Esconder el diálogo al final
        interactionIndicator.SetActive(true); // Volver a mostrar la señal después del diálogo
    }
}

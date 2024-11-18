using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BienvenidaManager : MonoBehaviour
{
    [Header("Referencias del UI")]
    public GameObject dialogoPanel; // El panel que contiene el mensaje de bienvenida
    public TextMeshProUGUI mensajeBienvenidaTMP; // Campo de texto para mostrar los mensajes

    [Header("Configuración de Diálogos")]
    public List<string> textosDeBienvenida; // Lista de textos configurables desde el Inspector
    public float velocidadEscritura = 0.05f; // Velocidad de escritura

    private Queue<string> dialogos; // Cola de diálogos para gestionar los textos
    private bool textoEnProgreso = false; // Controlar si se está escribiendo el texto
    private string textoActual = ""; // Texto que se está escribiendo actualmente

    void Start()
    {
        // Inicializar la cola de diálogos con los textos del Inspector
        dialogos = new Queue<string>();
        foreach (string texto in textosDeBienvenida)
        {
            dialogos.Enqueue(texto);
        }

        dialogoPanel.SetActive(true); // Mostrar el panel de bienvenida
        MostrarSiguienteDialogo(); // Iniciar el primer diálogo
    }

    void Update()
    {
        // Avanzar al siguiente diálogo con clic
        if (Input.GetMouseButtonDown(0))
        {
            if (textoEnProgreso) // Si el texto se está escribiendo, terminarlo inmediatamente
            {
                TerminarTextoInstantaneamente();
            }
            else // Si el texto ya está completo, mostrar el siguiente
            {
                MostrarSiguienteDialogo();
            }
        }
    }

    private void MostrarSiguienteDialogo()
    {
        if (dialogos.Count == 0)
        {
            dialogoPanel.SetActive(false); // Ocultar el panel al terminar los diálogos
            return;
        }

        textoActual = dialogos.Dequeue(); // Sacar el siguiente texto de la cola
        StartCoroutine(EscribirTexto(textoActual));
    }

    private IEnumerator EscribirTexto(string texto)
    {
        textoEnProgreso = true; // Bloquear hasta que termine de escribir
        mensajeBienvenidaTMP.text = "";

        foreach (char letra in texto.ToCharArray())
        {
            mensajeBienvenidaTMP.text += letra;
            yield return new WaitForSeconds(velocidadEscritura); // Esperar entre letras
        }

        textoEnProgreso = false; // Permitir avanzar al siguiente diálogo
    }

    private void TerminarTextoInstantaneamente()
    {
        StopAllCoroutines(); // Detener la escritura del texto
        mensajeBienvenidaTMP.text = textoActual; // Completar el texto actual instantáneamente
        textoEnProgreso = false; // Permitir avanzar al siguiente diálogo
    }
}

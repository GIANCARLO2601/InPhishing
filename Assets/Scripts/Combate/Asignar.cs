using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Asignar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI preguntaTMP;
    [SerializeField] private TextMeshProUGUI respuestaATMP;
    [SerializeField] private TextMeshProUGUI respuestaBTMP;
    [SerializeField] private TextMeshProUGUI respuestaCTMP;
    public List<CombatePreguntas> listaDePreguntas; // Lista de todas las preguntas
    private CombatePreguntas preguntaActual;
    [SerializeField] private Batalla batalla;
    private int preguntasCorrectas = 0;
    private int totalPreguntas = 5; // Número de preguntas correctas para ganar

    void Start()
    {
        batalla = GetComponent<Batalla>();
        MostrarNuevaPregunta();
    }

    private void MostrarNuevaPregunta()
    {
        if (preguntasCorrectas < totalPreguntas)
        {
            // Seleccionar una pregunta aleatoria de la lista
            preguntaActual = listaDePreguntas[Random.Range(0, listaDePreguntas.Count)];

            // Mostrar la pregunta y las respuestas en la UI
            preguntaTMP.text = preguntaActual.Pregunta;
            respuestaATMP.text = preguntaActual.respuestaA;
            respuestaBTMP.text = preguntaActual.respuestaB;
            respuestaCTMP.text = preguntaActual.respuestaC;
        }
        else
        {
            // Si se contestaron todas las preguntas correctamente, regresar a la escena principal
            RegresarALaEscenaPrincipal();
        }
    }

    public void ResponderCorrectamente()
    {
        preguntasCorrectas++;
        MostrarNuevaPregunta(); // Mostrar la siguiente pregunta solo si se responde correctamente
    }

    public void ResponderIncorrectamente()
    {
        // No cambia la pregunta, solo muestra un mensaje
        Debug.Log("Respuesta incorrecta. Intenta de nuevo.");
    }

    private void RegresarALaEscenaPrincipal()
    {
        batalla.OtorgarRecompensa();
        Debug.Log("¡Todas las preguntas correctas!");
        batalla.RegresarALaEscenaPrincipal();
    }
}

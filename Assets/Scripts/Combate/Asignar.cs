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
    private int totalPreguntas = 12; // Número de preguntas correctas para ganar

    void Start()
    {
        batalla = GetComponent<Batalla>();
        MostrarNuevaPregunta();
    }

    public void MostrarNuevaPregunta()
    {
        if (preguntasCorrectas < totalPreguntas)
        {
            // Seleccionar una pregunta aleatoria
            preguntaActual = listaDePreguntas[Random.Range(0, listaDePreguntas.Count)];

            // Mostrar la pregunta y las respuestas
            preguntaTMP.text = preguntaActual.Pregunta;
            respuestaATMP.text = preguntaActual.respuestaA;
            respuestaBTMP.text = preguntaActual.respuestaB;
            respuestaCTMP.text = preguntaActual.respuestaC;

            // Asignar listeners a los botones
            batalla.AsignarListeners(
                !preguntaActual.respuestaAIncorrecta,
                !preguntaActual.respuestaBIncorrecta,
                !preguntaActual.respuestaCIncorrecta
            );
        }
        else
        {
            RegresarALaEscenaPrincipal();
        }
    }

    public void ResponderCorrectamente()
    {
        preguntasCorrectas++;
        MostrarNuevaPregunta();
    }

    public void ResponderIncorrectamente()
    {
        Debug.Log("Respuesta incorrecta. Intenta de nuevo.");
    }

    private void RegresarALaEscenaPrincipal()
    {
        batalla.OtorgarRecompensa();
        batalla.RegresarALaEscenaPrincipal();
    }
}

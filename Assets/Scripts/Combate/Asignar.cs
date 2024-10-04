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
    private int totalPreguntas = 5; // Número de preguntas que el jugador debe contestar correctamente
    

    void Start()
    {
        batalla = GetComponent<Batalla>();
        MostrarNuevaPregunta();
    }

    private void MostrarNuevaPregunta()
    {
        if (preguntasCorrectas < totalPreguntas)
        {
            // Seleccionamos una pregunta aleatoria
            preguntaActual = listaDePreguntas[Random.Range(0, listaDePreguntas.Count)];

            // Mostrar los detalles de la pregunta en la UI
            preguntaTMP.text = preguntaActual.Pregunta;
            respuestaATMP.text = preguntaActual.respuestaA;
            respuestaBTMP.text = preguntaActual.respuestaB;
            respuestaCTMP.text = preguntaActual.respuestaC;
        }
        else
        {
            // Todas las preguntas contestadas correctamente, regresamos a la escena principal
            RegresarALaEscenaPrincipal();
        }
    }

    public void ResponderCorrectamente()
    {
        preguntasCorrectas++;
        MostrarNuevaPregunta(); // Mostrar la siguiente pregunta
    }

    public void ResponderIncorrectamente()
    {
        // Mostrar mensaje de error (lo puedes hacer en la UI si deseas)
        Debug.Log("Respuesta incorrecta.");
        MostrarNuevaPregunta(); // Mostrar otra pregunta o la misma
    }

    private void RegresarALaEscenaPrincipal()
    {
        // Aquí puedes otorgar la recompensa y regresar a la escena principal
        Debug.Log("¡Todas las preguntas correctas!");
        // Otorgar la llave y regresar a la escena principal (puedes usar el método del otro script)
        // Batalla.OtorgarRecompensa(); (puedes adaptar esto de acuerdo a tu inventario)
        StartCoroutine(batalla.OtorgarRecompensaYRegresar());
    }
}

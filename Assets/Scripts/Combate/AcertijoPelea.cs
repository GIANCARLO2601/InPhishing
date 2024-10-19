using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcertijoPelea : MonoBehaviour
{
    [Header("Referencias a los botones de respuesta")]
    [SerializeField] private Button botonRespuestaA;
    [SerializeField] private Button botonRespuestaB;
    [SerializeField] private Button botonRespuestaC;
    
    [Header("Items que puede soltar")]
    [SerializeField] private InventarioItem pocionVidaItem;
    [SerializeField] private InventarioItem megaVidaItem;
    [SerializeField] private InventarioItem medallaItem; // Agregar referencia para la medalla
    private PersonajeVida personajeVida;
    public AsignarAcertijo asignarAcertijoScript; // Referencia al script AsignarAcertijo que maneja las preguntas y respuestas

    void Start()
    {
        personajeVida = FindObjectOfType<PersonajeVida>();
        
        // Asignar listeners a los botones
        botonRespuestaA.onClick.AddListener(() => SeleccionarRespuesta("A"));
        botonRespuestaB.onClick.AddListener(() => SeleccionarRespuesta("B"));
        botonRespuestaC.onClick.AddListener(() => SeleccionarRespuesta("C"));
    }

    public void SeleccionarRespuesta(string respuesta)
    {
        // Dependiendo de la respuesta, verifica si es la correcta y avanza a la siguiente pregunta
        if (respuesta == "A" && asignarAcertijoScript.preguntaActual.respuestaACorrecta)
        {
            Debug.Log("¡Respuesta A correcta!");
            asignarAcertijoScript.AsignarPreguntaAleatoria(); // Cambiar a la siguiente pregunta
        }
        else if (respuesta == "B" && asignarAcertijoScript.preguntaActual.respuestaBCorrecta)
        {
            Debug.Log("¡Respuesta B correcta!");
            asignarAcertijoScript.AsignarPreguntaAleatoria(); // Cambiar a la siguiente pregunta
        }
        else if (respuesta == "C" && asignarAcertijoScript.preguntaActual.respuestaCCorrecta)
        {
            Debug.Log("¡Respuesta C correcta!");
            asignarAcertijoScript.AsignarPreguntaAleatoria(); // Cambiar a la siguiente pregunta
        }
        else
        {
            personajeVida.recibirDaño(10);
            Debug.Log("Respuesta incorrecta. Intenta de nuevo.");
        }
    }
}

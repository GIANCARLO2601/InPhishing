using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NuevoCombate", menuName = "Phishing/Combate")]
public class CombatePreguntas : ScriptableObject
{
    [Header("Pregunta")]
    public string Pregunta;

    [Header("Respuestas")]
    public string respuestaA; // Quien envía el correo
    public bool respuestaAIncorrecta;

    public string respuestaB; // Quien envía el correo
    public bool respuestaBIncorrecta;

    public string respuestaC; // Quien envía el correo
    public bool respuestaCIncorrecta;

}

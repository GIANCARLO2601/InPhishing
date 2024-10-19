using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NuevoAcertijo", menuName = "Acertijo")]
public class CombateAcertijo : ScriptableObject
{
    [Header("Acertijo")]
    [TextArea] public string acertijo;

    [Header("Respuestas")]
    public string respuestaA; // Quien envía el correo
    public bool respuestaACorrecta;

    public string respuestaB; // Quien envía el correo
    public bool respuestaBCorrecta;

    public string respuestaC; // Quien envía el correo
    public bool respuestaCCorrecta;

}
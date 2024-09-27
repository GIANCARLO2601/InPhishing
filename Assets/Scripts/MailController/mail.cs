using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevoCorreoPhishing", menuName = "Phishing/CorreoPhishing")]
public class mail : ScriptableObject
{
    [Header("Información del correo")]
    public string Remitente; // Quien envía el correo
    public bool RemitenteEsIncorrecto; // Si el remitente es incorrecto

    public string Asunto;    // Asunto del correo
    public bool AsuntoEsIncorrecto; // Si el asunto es incorrecto

    [TextArea] public string Cuerpo;  // Cuerpo del correo
    public bool CuerpoEsIncorrecto; // Si el cuerpo del correo es incorrecto

    public string EnlaceVisible;  // Enlace que el usuario ve en el correo
    public string EnlaceReal;     // Enlace malicioso real al que redirige
    public bool EnlaceEsIncorrecto; // Si el enlace es incorrecto

    [Header("Adjunto Opcional")]
    public bool TieneAdjunto; // Si el correo tiene un archivo adjunto
    public string NombreArchivoAdjunto; // Nombre del archivo adjunto, si lo hay
}

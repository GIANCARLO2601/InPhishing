using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemigoVida : VidaBaseEnemigo
{
    [SerializeField] private string nombreEnemigo;  // Nombre del enemigo
    private UIManagerEnemigo uiManagerEnemigo;  // Referencia al UIManagerEnemigo

    protected override void Start()
    {
        base.Start();
        Salud = saludMaxima;  // Aseguramos que la salud inicie al máximo
        ObtenerUIManagerEnemigo();  // Intentar obtener el UIManagerEnemigo
        ActualizarUIInicial();  // Actualizar la UI con los valores iniciales
    }

    private void ObtenerUIManagerEnemigo()
    {
        uiManagerEnemigo = FindObjectOfType<UIManagerEnemigo>();

        if (uiManagerEnemigo != null)
        {
            Debug.Log("UIManagerEnemigo encontrado y preparado.");
        }
        else
        {
            Debug.LogError("No se encontró UIManagerEnemigo.");
        }
    }

    private void ActualizarUIInicial()
    {
        if (uiManagerEnemigo != null)
        {
            uiManagerEnemigo.ActualizarVidaEnemigo(Salud, saludMaxima);
            Debug.Log($"Barra de vida inicializada: {Salud} / {saludMaxima}");
        }
    }

    public override void recibirDaño(float cantidad)
    {
        base.recibirDaño(cantidad);  // Llamar a la lógica base

        if (uiManagerEnemigo != null)
        {
            uiManagerEnemigo.ActualizarVidaEnemigo(Salud, saludMaxima);  // Actualizar UI
            Debug.Log($"Vida del enemigo actualizada: {Salud} / {saludMaxima}");
        }
    }
}

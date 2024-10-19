using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemigoVida : VidaBaseEnemigo
{
    [SerializeField] private string nombreEnemigo;  // Nombre del enemigo
    private UIManagerEnemigo uiManagerEnemigo;  // Referencia al UIManagerEnemigo

    protected override void Start()
    {
        base.Start();

        // Intentar obtener el UIManagerEnemigo
        if (SceneManager.GetActiveScene().name == "EscenaCombate")
        {
            ObtenerUIManagerEnemigo();
        }
        else
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "EscenaCombate")
        {
            ObtenerUIManagerEnemigo();
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void ObtenerUIManagerEnemigo()
    {
        uiManagerEnemigo = FindObjectOfType<UIManagerEnemigo>();

        if (uiManagerEnemigo != null)
        {
            uiManagerEnemigo.ActualizarVidaEnemigo(Salud, saludMaxima);  // Inicializar la UI
        }
        else
        {
            Debug.LogError("No se encontró UIManagerEnemigo.");
        }
    }

    public override void recibirDaño(float cantidad)
    {
        base.recibirDaño(cantidad);  // Llamar a la lógica base

        if (uiManagerEnemigo != null)
        {
            uiManagerEnemigo.ActualizarVidaEnemigo(Salud, saludMaxima);  // Actualizar UI
        }
    }
}

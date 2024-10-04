using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class ValidarBotonCorreoPhishing : MonoBehaviour
{
    // Instancia de la vida del personaje
    private PersonajeVida personajeVida;

    [Header("Referencias del UI")]
    public Button botonRemitente;
    public Button botonAsunto;
    public Button botonCuerpo;
    public Button botonEnlace;

    public TextMeshProUGUI textoRemitente;
    public TextMeshProUGUI textoAsunto;
    public TextMeshProUGUI textoCuerpo;
    public TextMeshProUGUI textoEnlace;

    public mail correoPhishing; // Referencia al ScriptableObject
    
    public Color colorCorrecto = Color.green;
    public Color colorIncorrecto = Color.red;

    private bool remitenteValidadoCorrectamente = false;
    private bool asuntoValidadoCorrectamente = false;
    private bool cuerpoValidadoCorrectamente = false;
    private bool enlaceValidadoCorrectamente = false;

    private int totalCamposIncorrectos;
    private int camposIncorrectosSeleccionados;

    private void Start()
    {
        personajeVida = FindObjectOfType<PersonajeVida>(); // Obtén la referencia de la vida del personaje
        if (personajeVida == null)
        {
            Debug.LogError("No se pudo encontrar el componente PersonajeVida.");
        }

        ContarCamposIncorrectos();

        botonRemitente.onClick.AddListener(() => ValidarRemitente());
        botonAsunto.onClick.AddListener(() => ValidarAsunto());
        botonCuerpo.onClick.AddListener(() => ValidarCuerpo());
        botonEnlace.onClick.AddListener(() => ValidarEnlace());
    }

    private void ContarCamposIncorrectos()
    {
        if (correoPhishing.RemitenteEsIncorrecto) totalCamposIncorrectos++;
        if (correoPhishing.AsuntoEsIncorrecto) totalCamposIncorrectos++;
        if (correoPhishing.CuerpoEsIncorrecto) totalCamposIncorrectos++;
        if (correoPhishing.EnlaceEsIncorrecto) totalCamposIncorrectos++;
    }

    public void ValidarRemitente()
    {
        if (correoPhishing.RemitenteEsIncorrecto)
        {
            textoRemitente.color = colorCorrecto;
            remitenteValidadoCorrectamente = true;
            camposIncorrectosSeleccionados++;
        }
        else
        {
            textoRemitente.color = colorIncorrecto;
            personajeVida.recibirDaño(5); // Reduce la vida al personaje
        }
        ComprobarSiTodoEsCorrecto();
    }

    public void ValidarAsunto()
    {
        if (correoPhishing.AsuntoEsIncorrecto)
        {
            textoAsunto.color = colorCorrecto;
            asuntoValidadoCorrectamente = true;
            camposIncorrectosSeleccionados++;
        }
        else
        {
            textoAsunto.color = colorIncorrecto;
            personajeVida.recibirDaño(5);
        }
        ComprobarSiTodoEsCorrecto();
    }

    public void ValidarCuerpo()
    {
        if (correoPhishing.CuerpoEsIncorrecto)
        {
            textoCuerpo.color = colorCorrecto;
            cuerpoValidadoCorrectamente = true;
            camposIncorrectosSeleccionados++;
        }
        else
        {
            textoCuerpo.color = colorIncorrecto;
            personajeVida.recibirDaño(5);
        }
        ComprobarSiTodoEsCorrecto();
    }

    public void ValidarEnlace()
    {
        if (correoPhishing.EnlaceEsIncorrecto)
        {
            textoEnlace.color = colorCorrecto;
            enlaceValidadoCorrectamente = true;
            camposIncorrectosSeleccionados++;
        }
        else
        {
            textoEnlace.color = colorIncorrecto;
            personajeVida.recibirDaño(5);
        }
        ComprobarSiTodoEsCorrecto();
    }

    private void ComprobarSiTodoEsCorrecto()
    {
        if (camposIncorrectosSeleccionados == totalCamposIncorrectos)
        {
            Debug.Log("Todos los elementos incorrectos seleccionados. Volviendo a la escena principal...");

            // Llamar al método Singleton para destruir el objeto en la EscenaPrincipal
            fantasmaMail.Instance.DestruirObjetoYSoltarItem();

            // Reanudar la escena principal
            Time.timeScale = 1;

            // Cerrar la escena de phishing y activar la escena principal
            SceneManager.UnloadSceneAsync("EscenaMailFail");
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("EscenaPrincipal"));
        }
    }
}

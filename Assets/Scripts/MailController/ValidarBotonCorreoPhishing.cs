using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ValidarBotonCorreoPhishing : MonoBehaviour
{
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

    public mail correoPhishing;

    public Color colorCorrecto = Color.green;
    public Color colorIncorrecto = Color.red;

    private int totalCamposIncorrectos = 0;
    private int camposIncorrectosSeleccionados = 0;
    private bool todoValidado = false;

    private void Start()
    {
        personajeVida = FindObjectOfType<PersonajeVida>();
        if (personajeVida == null)
        {
            Debug.LogError("No se pudo encontrar el componente PersonajeVida.");
        }

        ContarCamposIncorrectos();

        botonRemitente.onClick.AddListener(ValidarRemitente);
        botonAsunto.onClick.AddListener(ValidarAsunto);
        botonCuerpo.onClick.AddListener(ValidarCuerpo);
        botonEnlace.onClick.AddListener(ValidarEnlace);
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
            camposIncorrectosSeleccionados++;
        }
        else
        {
            textoRemitente.color = colorIncorrecto;
            personajeVida.recibirDaño(5);
        }
        ComprobarSiTodoEsCorrecto();
    }

    public void ValidarAsunto()
    {
        if (correoPhishing.AsuntoEsIncorrecto)
        {
            textoAsunto.color = colorCorrecto;
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
        if (todoValidado) return;

        if (camposIncorrectosSeleccionados == totalCamposIncorrectos)
        {
            Debug.Log("Todos los elementos incorrectos seleccionados. Volviendo a la escena principal...");

            botonRemitente.interactable = false;
            botonAsunto.interactable = false;
            botonCuerpo.interactable = false;
            botonEnlace.interactable = false;

            todoValidado = true;

            if (fantasmaMail.Instance != null)
            {
                fantasmaMail.Instance.DestruirObjetoYSoltarItem();
            }
            else
            {
                Debug.LogWarning("No se encontró una instancia de 'fantasmaMail'.");
            }

            Time.timeScale = 1;

            SceneManager.UnloadSceneAsync("EscenaMailFail").completed += (asyncOperation) =>
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName("EscenaPrincipal"));
            };
        }
    }
}

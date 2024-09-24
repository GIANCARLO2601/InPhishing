using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class CombateManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    public Image playerHealthBar;
    public Image enemyHealthBar;
    public TextMeshProUGUI playerHealthText;   // Texto que muestra la vida del jugador
    public TextMeshProUGUI enemyHealthText;    // Texto que muestra la vida del enemigo

    private int currentPlayerHealth = 100;
    private int currentEnemyHealth = 100;
    private Pregunta preguntaActual;
    private List<Pregunta> bancoPreguntas;

    private void Start()
    {
        InicializarBancoPreguntas();
        MostrarPregunta();
        ActualizarBarraVida(); // Inicializar la barra y texto de vida
    }

    private void InicializarBancoPreguntas()
    {
        bancoPreguntas = new List<Pregunta>
        {
            new Pregunta("¿Cuál es la capital de Francia?", new string[] { "París", "Londres", "Berlín" }, "París"),
            new Pregunta("¿Cuánto es 5 + 3?", new string[] { "5", "8", "10" }, "8"),
            new Pregunta("¿Cuál es el planeta más grande del sistema solar?", new string[] { "Marte", "Júpiter", "Saturno" }, "Júpiter"),
            new Pregunta("¿Quién escribió 'Don Quijote'?", new string[] { "Cervantes", "Shakespeare", "Dante" }, "Cervantes")
        };
    }

    public void MostrarPregunta()
    {
        // Seleccionar una pregunta aleatoria del banco de preguntas
        preguntaActual = bancoPreguntas[Random.Range(0, bancoPreguntas.Count)];
        questionText.text = preguntaActual.pregunta;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            string respuesta = preguntaActual.respuestas[i];
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = respuesta;
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => VerificarRespuesta(respuesta));
        }
    }

    public void VerificarRespuesta(string respuestaSeleccionada)
    {
        if (respuestaSeleccionada == preguntaActual.respuestaCorrecta)
        {
            // Respuesta correcta: el enemigo pierde vida
            currentEnemyHealth -= 20;
        }
        else
        {
            // Respuesta incorrecta: el jugador pierde vida
            currentPlayerHealth -= 20;
        }

        // Actualizar las barras de vida y el texto de vida
        ActualizarBarraVida();

        // Verificar si el combate ha terminado
        if (currentPlayerHealth <= 0)
        {
            FinDelCombate(false);
        }
        else if (currentEnemyHealth <= 0)
        {
            FinDelCombate(true);
        }
        else
        {
            MostrarPregunta(); // Mostrar la siguiente pregunta
        }
    }

    void ActualizarBarraVida()
    {
        // Actualizar la barra de vida del jugador y del enemigo
        playerHealthBar.fillAmount = Mathf.Clamp01((float)currentPlayerHealth / 100);
        enemyHealthBar.fillAmount = Mathf.Clamp01((float)currentEnemyHealth / 100);

        // Actualizar los textos de vida
        playerHealthText.text = $"{currentPlayerHealth}/100";
        enemyHealthText.text = $"{currentEnemyHealth}/100";
    }

    void FinDelCombate(bool playerWon)
    {
        if (playerWon)
        {
            questionText.text = "¡Has ganado!";
        }
        else
        {
            questionText.text = "Has perdido...";
        }

        // Deshabilitar botones de respuesta
        foreach (Button button in answerButtons)
        {
            button.interactable = false;
        }

    // If combat is over, return to the main scene
    GameManager.Instance.ReturnToMainScene();
    }

}

[System.Serializable]
public class Pregunta
{
    public string pregunta;
    public string[] respuestas;
    public string respuestaCorrecta;

    public Pregunta(string pregunta, string[] respuestas, string respuestaCorrecta)
    {
        this.pregunta = pregunta;
        this.respuestas = respuestas;
        this.respuestaCorrecta = respuestaCorrecta;
    }
}

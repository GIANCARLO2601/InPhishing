using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Batalla : MonoBehaviour
{
    private PersonajeVida personajeVida;  // Referencia a la vida del jugador

    [Header("Referencias del UI")]
    public Button botonOpcion1;
    public Button botonOpcion2;
    public Button botonOpcion3;
    public InventarioItem llaveItem;  // Llave que se otorga tras la victoria
    public TextMeshProUGUI textoPregunta;
    public TextMeshProUGUI textoResultado;  // Mostrar "Te confundiste" o "¡Ganaste!"

    [Header("Vida del Enemigo")]
    [SerializeField] private EnemigoVida enemigoVida;  // Referencia al script EnemigoVida

    private Asignar asignar;  // Controla las preguntas gaaaa

    private void Start()
    {
        personajeVida = FindObjectOfType<PersonajeVida>();
        enemigoVida = FindObjectOfType<EnemigoVida>();
        Time.timeScale = 1;
        if (personajeVida == null)
        {
            Debug.LogError("No se encontró el componente PersonajeVida.");
        }

        asignar = GetComponent<Asignar>();
        if (enemigoVida == null)
        {
            Debug.LogError("No se ha asignado la referencia del EnemigoVida en el Inspector.");
        }
        

        // Asignar las funciones a los botones
        botonOpcion1.onClick.AddListener(() => VerificarRespuesta(true));  // Respuesta correcta
        botonOpcion2.onClick.AddListener(() => VerificarRespuesta(false)); // Respuesta incorrecta
        botonOpcion3.onClick.AddListener(() => VerificarRespuesta(false)); // Respuesta incorrecta
    }

    private void VerificarRespuesta(bool esCorrecta)
    {
        if (esCorrecta)
        {
            textoResultado.text = "¡Respuesta correcta!";
            enemigoVida.recibirDaño(10);  // Reducir vida del enemigo

            if (enemigoVida.Salud <= 0)
            {
                OtorgarRecompensa();
                RegresarALaEscenaPrincipal();
            }
            else
            {
                asignar.ResponderCorrectamente();  // Mostrar siguiente pregunta
            }
        }
        else
        {
            textoResultado.text = "Respuesta incorrecta. Intenta de nuevo.";
            personajeVida.recibirDaño(10);  // Daño al jugador
            asignar.ResponderIncorrectamente();  // Mantener la misma pregunta
        }
    }

    public void OtorgarRecompensa()
    {
        if (llaveItem != null)
        {
            Inventario.Instance.AñadirItem(llaveItem, 1);
            Debug.Log("Llave agregada al inventario.");
        }
        else
        {
            Debug.LogWarning("No se ha asignado ninguna llave.");
        }
    }

    public void RegresarALaEscenaPrincipal()
    {
        SceneManager.UnloadSceneAsync("EscenaCombate");

        Scene mainScene = SceneManager.GetSceneByName("EscenaPrincipal");
        if (!mainScene.isLoaded)
        {
            Debug.LogWarning("Cargando la escena principal...");
            SceneManager.LoadScene("EscenaPrincipal");
        }
        else
        {
            SceneManager.SetActiveScene(mainScene);
        }

        UIManager.Instance.ConfigurarUIParaCombate(false);  // Restaurar UI estándar
        Time.timeScale = 1;  // Asegurar que el tiempo no esté pausado
        Debug.Log("Escena de combate cerrada y regresando a la escena principal.");
    }
}

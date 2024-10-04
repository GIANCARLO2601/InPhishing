using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Batalla : MonoBehaviour
{
    private PersonajeVida personajeVida;
    [Header("Referencias del UI")]
    public Button botonOpcion1;
    public Button botonOpcion2;
    public Button botonOpcion3;
    public InventarioItem llaveItem;  // Llave que se otorga tras la victoria
    public TextMeshProUGUI textoPregunta;
    public TextMeshProUGUI textoResultado;  // Mostrar "Te confundiste" o "¡Ganaste!"

    private Asignar asignar; // Referencia al script Asignar para controlar las preguntas
    private void Start()
    
    {
        personajeVida = FindObjectOfType<PersonajeVida>(); // Obtén la referencia de la vida del personaje
        if (personajeVida == null)
        {
            Debug.LogError("No se pudo encontrar el componente PersonajeVida.");
        }
        asignar = GetComponent<Asignar>();
        
        // Asignar las funciones a los botones
        botonOpcion1.onClick.AddListener(() => VerificarRespuesta(true)); // Si la respuesta es correcta
        botonOpcion2.onClick.AddListener(() => VerificarRespuesta(false)); // Respuesta incorrecta
        botonOpcion3.onClick.AddListener(() => VerificarRespuesta(false)); // Respuesta incorrecta
    }

    // Función para verificar la respuesta
    private void VerificarRespuesta(bool esCorrecta)
    {
        if (esCorrecta)
        {
            textoResultado.text = "¡Ganaste!";
            asignar.ResponderCorrectamente();
        }
        else
        {
            textoResultado.text = "Te confundiste";
            personajeVida.recibirDaño(10);
            asignar.ResponderIncorrectamente();

        }
    }

    // Corutina para otorgar la recompensa después de todas las preguntas correctas
    public IEnumerator OtorgarRecompensaYRegresar()
    {
        // Otorgar la llave al inventario
        if (llaveItem != null)
        {
            Inventario.Instance.AñadirItem(llaveItem, 1);
            Debug.Log("Llave agregada al inventario.");
        }
        else
        {
            Debug.LogWarning("No se ha asignado la llaveItem.");
        }
        yield return new WaitForSeconds(1f); 
        // Esperar un poco antes de cambiar de escena (esto crea una pequeña pausa para la transición)
        

        // Desactivar la escena de combate y activar la escena principal
        SceneManager.UnloadSceneAsync("EscenaCombate");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("EscenaPrincipal"));

        // Asegurarse de que el juego esté corriendo normalmente (no en pausa)
        Time.timeScale = 1;

        Debug.Log("Escena de combate cerrada y regresando a la EscenaPrincipal.");
    }

}

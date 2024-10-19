using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniEstatua : MonoBehaviour
{
    [SerializeField] private GameObject estatua;
    private bool jugadorMini = false; 
    [Header("Objeto a destruir al regresar")] 
    [SerializeField] private GameObject objetoADestruir; 

    private void CambiarEscenaCombate()
    {
        try
        {
            // Cambiar a la escena de combate sin destruir UIManager
            SceneManager.LoadScene("EscenaAcertijo", LoadSceneMode.Additive); // Verifica que el nombre de la escena es correcto
                // Destruir el objeto que se le indique
            if (objetoADestruir != null)
            {
                Destroy(objetoADestruir);
                Debug.Log("El objeto ha sido destruido.");
            }
            else
            {
                Debug.LogWarning("No se ha asignado ningún objeto para destruir.");
            }
            Debug.Log("estatua destruido y objetos soltados.");

            // Verifica que UIManager está inicializado
            if (UIManager.Instance != null)
            {
                UIManager.Instance.ConfigurarUIParaCombate(true);
                Debug.Log("UIManager configurado para combate.");
            }
            else
            {
                Debug.LogError("UIManager.Instance es nulo. Asegúrate de que está inicializado.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error al cargar la escena: " + e.Message);
        }
    }

    // Detecta si el jugador colisiona con la estatua
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Asegúrate de que el jugador tiene la etiqueta "Player"
        {
            jugadorMini = true;
            CambiarEscenaCombate();
            Debug.Log("Jugador colisionó con la estatua.");
        }
    }

    // Detecta si el jugador deja de colisionar con la estatua
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Asegúrate de que el jugador tiene la etiqueta "Player"
        {
            jugadorMini = false;
            Debug.Log("Jugador dejó de colisionar con la estatua.");
        }
    }

    void Start()
    {
        // Inicializar variables u objetos si es necesario
    }

    void Update()
    {
        // Lógica del juego que se actualiza en cada frame
    }
}

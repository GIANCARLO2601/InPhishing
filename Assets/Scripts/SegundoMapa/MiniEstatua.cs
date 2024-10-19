using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniEstatua : MonoBehaviour
{
    [SerializeField] private MisionDos misionDos;  // Referencia al controlador de misión
    [SerializeField] private GameObject objetoADestruir;  // Objeto a destruir al terminar
    private bool completada = false;  // Evitar recompensas múltiples

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !completada)
        {
            Debug.Log("Jugador colisionó con la estatua. Cambiando a escena de acertijo.");
            misionDos.EstablecerEstatuaActual(this);  // Guardar la estatua en la misión
            SceneManager.LoadScene("EscenaAcertijo", LoadSceneMode.Additive);
        }
    }

    public void CompletarEstatua()
    {
        if (!completada)
        {
            completada = true;
            Debug.Log("Estatua completada.");
            misionDos.RegistrarEstatuaCompletada();  // Notificar a la misión

            if (objetoADestruir != null)
            {
                Destroy(objetoADestruir);
                Debug.Log("Estatua destruida.");
            }
        }
    }
}

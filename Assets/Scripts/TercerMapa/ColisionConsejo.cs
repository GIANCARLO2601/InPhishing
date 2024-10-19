using System.Collections;
using System.Collections.Generic;
using TMPro; 
using UnityEngine;

public class ColisionConsejo : MonoBehaviour
{
    [Header("Referencias del UI")]
    [SerializeField] private GameObject panelConsejo; // Panel donde se muestra el consejo
    [SerializeField] private TextMeshProUGUI textConsejo; // Componente TextMeshProUGUI para el texto

    private AsignarConsejo asignarConsejo; // Referencia al script AsignarConsejo
    private bool jugadorEnRango = false; // Controla si el jugador está en rango
    private bool consejoMostrado = false; // Controla si el consejo ya fue mostrado

    void Start()
    {
        panelConsejo.SetActive(false); // Desactiva el panel inicialmente

        // Buscar el script AsignarConsejo en la escena
        asignarConsejo = GameObject.FindObjectOfType<AsignarConsejo>();
        if (asignarConsejo == null)
        {
            Debug.LogError("No se encontró el componente AsignarConsejo en la escena.");
        }
    }

    void Update()
    {

        // Si el jugador está en rango y el consejo no se ha mostrado
        if (jugadorEnRango && !consejoMostrado)
        {
            MostrarPanelConsejo(); // Mostrar el consejo
        }
    }

    private void MostrarPanelConsejo()
    {
        panelConsejo.SetActive(true); // Activar el panel del consejo
        asignarConsejo.AsignarConsejoAleatorio(); // Asignar el consejo

        Debug.Log($"Consejo mostrado: {textConsejo.text}"); // Verificar que se asignó texto
        consejoMostrado = true; // Marcar que el consejo fue mostrado

        StartCoroutine(CerrarConsejoDespuesDeTiempo(2f)); // Cerrar consejo después de 2 segundos
    }

    private IEnumerator CerrarConsejoDespuesDeTiempo(float segundos)
    {
        yield return new WaitForSeconds(segundos); // Esperar 2 segundos
        panelConsejo.SetActive(false); // Ocultar el panel
        Debug.Log("Panel de consejo ocultado.");
    }

    // Detecta si el jugador entra en contacto con el objeto
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Asegúrate de que el jugador tiene la etiqueta "Player"
        {

            jugadorEnRango = true; // El jugador está en rango
            Debug.Log("Jugador colisionó con el objeto.");
            MostrarPanelConsejo();
            Destroy(gameObject); // Destruir el objeto de colisión inmediatamente
        }
    }

    // Detecta si el jugador deja de colisionar
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Asegúrate de que el jugador tiene la etiqueta "Player"
        {
            jugadorEnRango = false; // El jugador ya no está en rango
            StartCoroutine(CerrarConsejoDespuesDeTiempo(2f)); 
            Debug.Log("Jugador dejó de colisionar con el objeto.");
        }
    }
}

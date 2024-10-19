using System.Collections;
using System.Collections.Generic;
using TMPro; 
using UnityEngine;
using UnityEngine.UI;

public class TrampaSegundoMapa : MonoBehaviour
{
    [Header("Referencias del UI")]
    [SerializeField] private GameObject panelOpciones; 
    [SerializeField] private Button botonSeguir;
    [SerializeField] private Button botonNoSeguir;
    [SerializeField] private GameObject panelAviso;
    [SerializeField] private TextMeshProUGUI avisoTexto;

    [Header("NPC específico para activar el panel")] 
    [SerializeField] private GameObject npcEspecifico; // Referencia al NPC específico

    private DialogoManager dialogoManager; 
    private VidaBase vidaBase;
    private PersonajeVida vidapersonaje;
    private bool panelInteractuado = false; // Nueva variable de control
    private bool jugadorEnRango = false; // Verifica si el jugador está en el rango del NPC

    void Start()
    {
        panelOpciones.SetActive(false);
        panelAviso.SetActive(false); 
        avisoTexto.text = ""; 

        dialogoManager = DialogoManager.Instance; 

        botonSeguir.onClick.AddListener(SeguirCamino);
        botonNoSeguir.onClick.AddListener(NoSeguirCamino);
        vidaBase = GameObject.FindObjectOfType<VidaBase>();
        vidapersonaje = GameObject.FindObjectOfType<PersonajeVida>();
    }

    void Update()
    {
        // Verificamos si el jugador está en el rango del NPC y si estamos en la despedida
        if (jugadorEnRango && dialogoManager.despedidaMostrar && !panelInteractuado)
        {
            ActivarPanelOpciones();
        }
        else
        {
            // Si el jugador sale del rango o ya interactuó, desactivamos el panel
            panelOpciones.SetActive(false);
        }
    }

    private void ActivarPanelOpciones()
    {
        panelOpciones.SetActive(true); 
        Debug.Log("Panel de opciones activado para el NPC específico");
    }

    private void SeguirCamino()
    {
        Debug.Log("El jugador ha decidido seguir el camino.");
        dialogoManager.AbrirCerrarPanelDialogo(false);
        panelOpciones.SetActive(false);
        panelInteractuado = true; // Se ha interactuado con el panel
        vidaBase.recibirDaño(10);

        panelAviso.SetActive(true);
        Time.timeScale = 1;
        avisoTexto.text = "-10 vida. Recuerda: En phishing, lo que parece confiable puede ser una trampa.";
        StartCoroutine(CerrarDialogoYReanudarDespuesDeTiempo(2f)); 
    }

    private void NoSeguirCamino()
    {
        Debug.Log("El jugador ha decidido no seguir el camino.");
        panelOpciones.SetActive(false); 
        panelInteractuado = true; // Se ha interactuado con el panel
        vidapersonaje.RestaurarSalud(10);
        dialogoManager.AbrirCerrarPanelDialogo(false);
        panelAviso.SetActive(true);
        avisoTexto.text = "+10 vida. ¡Buen trabajo! Evitaste la trampa.";
        Time.timeScale = 1;
        StartCoroutine(CerrarDialogoYReanudarDespuesDeTiempo(2f));
    }

    // Detecta si el jugador colisiona con el NPC específico
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Asegúrate de que el jugador tiene la etiqueta "Player"
        {
            jugadorEnRango = true;
            Debug.Log("Jugador colisionó con el NPC específico.");
        }
    }

    // Detecta si el jugador deja de colisionar con el NPC específico
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Asegúrate de que el jugador tiene la etiqueta "Player"
        {
            jugadorEnRango = false;
            Debug.Log("Jugador dejó de colisionar con el NPC específico.");
        }
    }

    // Corutina para cerrar el diálogo y reanudar después de unos segundos
    private IEnumerator CerrarDialogoYReanudarDespuesDeTiempo(float segundos)
    {
        yield return new WaitForSeconds(segundos); 
        panelOpciones.SetActive(false); 
        panelAviso.SetActive(false); 
        avisoTexto.text = "";

        Debug.Log("Dialogo finalizado y juego reanudado.");
    }
}

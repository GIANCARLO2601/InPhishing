using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;  // Importante para cambiar de escena

public class BossDialogoManager : Singleton<BossDialogoManager>
{
    [SerializeField] private GameObject panelDialogo;
    [SerializeField] private TextMeshProUGUI npcNombreTMP;
    [SerializeField] private TextMeshProUGUI npcConversacionTMP;

    public BossInteraccion NPCDisponible { get; set; }

    private Queue<string> dialogosSecuencia;
    private bool dialogoAnimado;
    private bool despedidaMostrar;

    public bool DialogoActivo { get; private set; } // Nueva variable para saber si el diálogo está activo

    private void Start()
    {
        dialogosSecuencia = new Queue<string>();
    }

    private void Update()
    {
        if (NPCDisponible == null)
        {
            return;
        }

        // Iniciar el diálogo al presionar "E"
        if (Input.GetKeyDown(KeyCode.E))
        {
            ConfigurarPanel(NPCDisponible.Dialogo);
            PausarJuego(); // Pausamos el juego, menos la UI
        }

        // Continuar el diálogo al presionar "Espacio"
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (despedidaMostrar)
            {
                // Mostrar la despedida y luego permitir reanudar el juego
                AbrirCerrarPanelDialogo(false);
                despedidaMostrar = false;
                //ReanudarJuego(); // Reanudar el juego después de la despedida
                CambiarEscenaCombate();  // Cambiar a la escena de combate
            }
            else if (dialogoAnimado)
            {
                ContinuarDialogo(); // Continuar el diálogo si no se ha llegado a la despedida
            }
        }

        // Permitir salir del diálogo con "Escape" en cualquier momento
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AbrirCerrarPanelDialogo(false);
            ReanudarJuego(); // Reanudar el juego cuando se cierra el diálogo
        }
    }

    public void AbrirCerrarPanelDialogo(bool estado)
    {
        Debug.Log(panelDialogo != null ? "Panel asignado correctamente" : "Panel no asignado");

    if (panelDialogo != null)
    {
        panelDialogo.SetActive(estado);
        DialogoActivo = estado; // Actualizar el estado del diálogo
    }
    }

    private void ConfigurarPanel(NPCDialogo npcDialogo)
    {
        Debug.Log(npcNombreTMP != null ? "Texto de nombre del NPC asignado" : "Texto de nombre no asignado");
    Debug.Log(npcConversacionTMP != null ? "Texto de conversación asignado" : "Texto de conversación no asignado");

    if (npcNombreTMP != null && npcConversacionTMP != null)
    {
        AbrirCerrarPanelDialogo(estado: true);
        CargarDialogosSecuencia(npcDialogo);
        npcNombreTMP.text = $"{npcDialogo.Nombre}:";
        MostrarTextoConAnimacion(npcDialogo.Saludo);
    }
    }

    private void CargarDialogosSecuencia(NPCDialogo npcDialogo)
    {
        dialogosSecuencia.Clear();
        foreach (var oracion in npcDialogo.Conversacion)
        {
            dialogosSecuencia.Enqueue(oracion.Oracion);
        }
    }

    private IEnumerator AnimarTexto(string oracion)
    {
        dialogoAnimado = false;
        npcConversacionTMP.text = "";
        foreach (char letra in oracion.ToCharArray())
        {
            npcConversacionTMP.text += letra;
            yield return new WaitForSecondsRealtime(0.03f); // Usamos WaitForSecondsRealtime para evitar problemas con Time.timeScale
        }
        dialogoAnimado = true;
    }

    private void MostrarTextoConAnimacion(string oracion)
    {
        StartCoroutine(AnimarTexto(oracion));
    }

    private void ContinuarDialogo()
    {
        if (dialogosSecuencia.Count == 0)
        {
            // Mostrar mensaje de despedida desde el ScriptableObject cuando se terminen los diálogos
            MostrarDespedida(NPCDisponible.Dialogo);
            despedidaMostrar = true;
            return;
        }

        // Continuar con el siguiente diálogo
        string siguienteDialogo = dialogosSecuencia.Dequeue();
        MostrarTextoConAnimacion(siguienteDialogo);
    }

    private void MostrarDespedida(NPCDialogo npcDialogo)
    {
        // Mostrar la despedida definida en el NPCDialogo
        MostrarTextoConAnimacion(npcDialogo.Despedida);
    }

    // Método para pausar el juego pero dejar la UI funcionando
    private void PausarJuego()
    {
        Time.timeScale = 0; // Detenemos el tiempo del juego
        Debug.Log("Juego pausado, pero los diálogos siguen activos.");
    }

    // Método para reanudar el juego
    private void ReanudarJuego()
    {
        Time.timeScale = 1; // Reanudamos el tiempo del juego
        Debug.Log("Juego reanudado.");
    }
    
    // Método para cambiar a la escena de combate
    private void CambiarEscenaCombate()
    {
        Debug.Log("Cambiando a la escena de combate...");
        Time.timeScale = 0;
        SceneManager.LoadScene("EscenaCombate", LoadSceneMode.Additive);
         // Cambia a la escena de combate
    }
}

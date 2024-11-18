using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogoManager : Singleton<DialogoManager>
{
    [SerializeField] private GameObject panelDialogo;
    [SerializeField] private TextMeshProUGUI npcNombreTMP;
    [SerializeField] private TextMeshProUGUI npcConversacionTMP;

    public NewBehaviourScript NPCDisponible { get; set; }

    private Queue<string> dialogosSecuencia;
    private bool dialogoAnimado;
    public bool despedidaMostrar;

    public bool DialogoActivo { get; private set; }

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
            if (!DialogoActivo) // Iniciar solo si no está activo
            {
                ConfigurarPanel(NPCDisponible.Dialogo);
                PausarJuego();
            }
        }

        // Continuar el diálogo al presionar "Espacio"
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (despedidaMostrar)
            {
                AbrirCerrarPanelDialogo(false);
                despedidaMostrar = false;
                ReanudarJuego();
            }
            else if (dialogoAnimado)
            {
                ContinuarDialogo();
            }
        }

        // Permitir salir del diálogo con "Escape" en cualquier momento
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AbrirCerrarPanelDialogo(false);
            ReanudarJuego();
        }
    }

    public void AbrirCerrarPanelDialogo(bool estado)
    {
        panelDialogo.SetActive(estado);
        DialogoActivo = estado;
    }

    private void ConfigurarPanel(NPCDialogo npcDialogo)
    {
        AbrirCerrarPanelDialogo(true);
        ReiniciarEstadoDialogo(); // Reiniciamos el estado del diálogo
        CargarDialogosSecuencia(npcDialogo);
        npcNombreTMP.text = $"{npcDialogo.Nombre}:";
        MostrarTextoConAnimacion(npcDialogo.Saludo);
    }

    private void ReiniciarEstadoDialogo()
    {
        despedidaMostrar = false;
        dialogoAnimado = true; // Asegurar que se pueda animar texto correctamente
        dialogosSecuencia.Clear(); // Limpiar cualquier texto anterior
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
            yield return new WaitForSecondsRealtime(0.004f);
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
            MostrarDespedida(NPCDisponible.Dialogo);
            despedidaMostrar = true;
            return;
        }

        string siguienteDialogo = dialogosSecuencia.Dequeue();
        MostrarTextoConAnimacion(siguienteDialogo);
    }

    private void MostrarDespedida(NPCDialogo npcDialogo)
    {
        MostrarTextoConAnimacion(npcDialogo.Despedida);

        NPCInteraccionPrincipal npcInteraccionPrincipal = NPCDisponible.GetComponent<NPCInteraccionPrincipal>();
        MisionDos misionDos = NPCDisponible.GetComponent<MisionDos>();

        if (npcInteraccionPrincipal != null)
        {
            npcInteraccionPrincipal.ActivarNPCFantasmas();
        }
        if (misionDos != null)
        {
            misionDos.ActivarNPC();
        }

        AbrirCerrarPanelDialogo(false);
        ReanudarJuego();
    }

    private void PausarJuego()
    {
        Time.timeScale = 0;
        Debug.Log("Juego pausado, pero los diálogos siguen activos.");
    }

    private void ReanudarJuego()
    {
        Time.timeScale = 1;
        Debug.Log("Juego reanudado.");
    }
}

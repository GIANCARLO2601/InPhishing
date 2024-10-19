using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDialogoManager : Singleton<BossDialogoManager>
{
    [SerializeField] private GameObject panelDialogo;
    [SerializeField] private TextMeshProUGUI npcNombreTMP;
    [SerializeField] private TextMeshProUGUI npcConversacionTMP;

    public BossInteraccion NPCDisponible { get; set; }
    private Sprite spriteBoss;  // Almacena el sprite del Boss

    private Queue<string> dialogosSecuencia;
    private bool dialogoAnimado;
    private bool despedidaMostrar;

    public bool DialogoActivo { get; private set; }

    private void Start()
    {
        dialogosSecuencia = new Queue<string>();
    }

    private void Update()
    {
        if (NPCDisponible == null) return;

        // Iniciar el diálogo al presionar "E"
        if (Input.GetKeyDown(KeyCode.E))
        {
            ConfigurarPanel(NPCDisponible.Dialogo);
            PausarJuego();
        }

        // Continuar el diálogo o cambiar a la escena de combate
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (despedidaMostrar)
            {
                AbrirCerrarPanelDialogo(false);
                despedidaMostrar = false;
                CambiarEscenaCombate();  // Cambiar a la escena de combate
            }
            else if (dialogoAnimado)
            {
                ContinuarDialogo();  // Continuar con el siguiente diálogo
            }
        }

        // Salir del diálogo con "Escape"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AbrirCerrarPanelDialogo(false);
            ReanudarJuego();
        }
    }

    public void AsignarSpriteBoss(Sprite sprite)
    {
        if (sprite != null)
        {
            spriteBoss = sprite;
            Debug.Log("Sprite del Boss almacenado en BossDialogoManager.");
        }
        else
        {
            Debug.LogError("El sprite recibido es nulo.");
        }
    }

    public void AbrirCerrarPanelDialogo(bool estado)
    {
        if (panelDialogo != null)
        {
            panelDialogo.SetActive(estado);
            DialogoActivo = estado;
        }
        else
        {
            Debug.LogError("Panel de diálogo no asignado.");
        }
    }

    private void ConfigurarPanel(NPCDialogo npcDialogo)
    {
        if (npcNombreTMP != null && npcConversacionTMP != null)
        {
            AbrirCerrarPanelDialogo(true);
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
    }

    private void MostrarTextoConAnimacion(string oracion)
    {
        StartCoroutine(AnimarTexto(oracion));
    }

    private IEnumerator AnimarTexto(string oracion)
    {
        dialogoAnimado = false;
        npcConversacionTMP.text = "";
        foreach (char letra in oracion.ToCharArray())
        {
            npcConversacionTMP.text += letra;
            yield return new WaitForSecondsRealtime(0.03f);
        }
        dialogoAnimado = true;
    }

    private void CambiarEscenaCombate()
    {
        StartCoroutine(TransferirSpriteYEscena());
    }

    private IEnumerator TransferirSpriteYEscena()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("EscenaCombate", LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        UIManagerEnemigo uiManagerEnemigo = FindObjectOfType<UIManagerEnemigo>();
        if (uiManagerEnemigo != null)
        {
            uiManagerEnemigo.AsignarSpriteEnemigo(spriteBoss);
            Debug.Log("Sprite del Boss transferido correctamente a la escena de combate.");
        }
        else
        {
            Debug.LogError("No se encontró UIManagerEnemigo en la EscenaCombate.");
        }

        UIManager.Instance.ConfigurarUIParaCombate(true);
    }

    private void PausarJuego()
    {
        Time.timeScale = 0;
    }

    private void ReanudarJuego()
    {
        Time.timeScale = 1;
        UIManager.Instance.ConfigurarUIParaCombate(false);
    }
}

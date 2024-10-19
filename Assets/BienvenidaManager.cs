using System.Collections;
using UnityEngine;
using TMPro;

public class BienvenidaManager : MonoBehaviour
{
    public GameObject dialogoPanel;  // El panel que contiene el mensaje de bienvenida
    public TextMeshProUGUI mensajeBienvenida;
    public float duracionTexto = 5f; // Duración en segundos

    void Start()
    {
        // Mostrar el panel de bienvenida al inicio del juego
        dialogoPanel.SetActive(true);
        StartCoroutine(OcultarBienvenida());
    }

    IEnumerator OcultarBienvenida()
    {
        // Esperar la duración especificada
        yield return new WaitForSeconds(duracionTexto);

        // Ocultar todo el panel después de la duración
        dialogoPanel.SetActive(false);
    }
}
